# Woche 6: Fortgeschrittene Konzepte und Projektstart

Herzlich willkommen zur sechsten Woche. Wir haben nun die wichtigsten Grundlagen der objektorientierten Softwareentwicklung, Design Patterns, SOLID und Testing abgedeckt. In dieser Woche schauen wir uns zwei Themen an, die für moderne, vernetzte Verwaltungssysteme unerlässlich sind: **Asynchrone Programmierung** und professionelles **Error Handling**. Außerdem legen wir den Grundstein für Ihre Abschlussprojekte.

---

## 1. Asynchrone Programmierung mit Async/Await

In der modernen Verwaltungsinformatik kommunizieren Systeme ständig mit anderen Registern oder Diensten (Melderegister, Grundsteuer-API, Post-Versand). Diese Abfragen über das Netzwerk brauchen Zeit.
*   **Synchron:** Das Programm wartet (blockiert), bis die Antwort da ist. Der Nutzer sieht eine "eingefrorene" Oberfläche.
*   **Asynchron:** Das Programm stößt die Abfrage an und macht in der Zwischenzeit etwas anderes. Sobald die Antwort da ist, wird die Verarbeitung fortgesetzt.

### Beispiel: Parallele API-Abfragen für Melderegister
```csharp
public async Task<BuergerDaten> HoleVollstaendigeDatenAsync(string id)
{
    // Wir starten zwei Abfragen gleichzeitig (parallel)
    var meldeTask = _apiClient.GetMelderegisterDatenAsync(id);
    var steuerTask = _apiClient.GetSteuerDatenAsync(id);

    // Hier warten wir auf beide Ergebnisse, ohne den Thread zu blockieren
    await Task.WhenAll(meldeTask, steuerTask);

    return new BuergerDaten {
        Name = meldeTask.Result.Name,
        SteuerKlasse = steuerTask.Result.Klasse
    };
}
```

**Wichtige Regel:** "Async all the way". Wenn eine Methode `async` ist, sollte auch die aufrufende Methode `async` sein. Nutzen Sie niemals `.Result` oder `.Wait()` auf einem Task, da dies zu Deadlocks führen kann!

---

## 2. Robustes Error Handling in großen Systemen

In einem komplexen Fachverfahren reicht ein einfaches `try-catch` oft nicht aus. Wir müssen entscheiden: Wann fangen wir einen Fehler ab? Wann lassen wir ihn nach oben durchreichen?

### Best Practices
1.  **Fail Fast:** Prüfen Sie Eingaben so früh wie möglich (Validation).
2.  **Spezifische Exceptions:** Fangen Sie nicht einfach `Exception` (den Vater aller Fehler), sondern spezifische Fehler wie `HttpRequestException` oder `SqlException`.
3.  **Zentrale Fehlerbehandlung:** Nutzen Sie in Web-Anwendungen eine "Middleware", die alle nicht abgefangenen Fehler zentral loggt und dem Nutzer eine freundliche (aber nicht zu informative!) Fehlermeldung zeigt.
4.  **Logging:** Ein Fehler ohne Log-Eintrag ist für einen Admin wertlos. Nutzen Sie Frameworks wie `Serilog`.

### Beispiel: Globales Logging
```csharp
try 
{
    await _service.VerarbeiteAntrag(antrag);
}
catch (AntragUngueltigException ex)
{
    // Fachlicher Fehler: Nutzer informieren
    _logger.Warning("Ungültiger Antrag von Nutzer {User}: {Message}", antrag.UserId, ex.Message);
    ShowUserError("Bitte prüfen Sie Ihre Eingaben.");
}
catch (Exception ex)
{
    // Technischer Fehler: Systematisches Problem
    _logger.Error(ex, "Kritischer Fehler bei Antragsverarbeitung");
    ShowUserError("Ein technisches Problem ist aufgetreten. Support ist informiert.");
}
```

---

## 3. Vorbereitung der Abschlussprojekte

Ab nächster Woche starten wir in die Projektphase. Ziel ist es, ein kleines, aber architektonisch sauberes **Fachverfahren** zu entwickeln.

### Die Anforderungen
*   **Architektur:** Nutzen Sie eine klare Schichten-Trennung (UI -> Business -> Data).
*   **SOLID:** Wenden Sie mindestens drei der SOLID-Prinzipien bewusst an.
*   **Patterns:** Implementieren Sie mindestens zwei Design Patterns (z.B. Factory für Bescheide und Strategy für Gebühren).
*   **Testing:** Sichern Sie die Kern-Logik mit Unit Tests (xUnit) ab.
*   **Clean Code:** Achten Sie auf Benennung, kleine Methoden und Kommentare (nur wo nötig).

---

## 4. Themenvorschläge für Projekte
1.  **KFZ-Zulassungsstelle:** Verwaltung von Kennzeichen, Haltern und Fahrzeugen inkl. Gebührenberechnung.
2.  **Bauantrags-Workflow:** Status-Management eines Antrags von Einreichung bis Genehmigung.
3.  **Wohngeld-Rechner:** Komplexere Berechnungslogik basierend auf verschiedenen sozialen Faktoren.
4.  **Hundesteuer-Portal:** Anmeldung von Hunden, Rassen-Prüfung und Steuerbescheid-Erstellung.

---

## Zusammenfassung
*   **Async/Await:** Macht Anwendungen reaktionsschnell und effizient beim Warten auf externe Daten.
*   **Error Handling:** Unterscheidet zwischen fachlichen Fehlern (User-Fehler) und technischen Katastrophen (System-Fehler).
*   **Projekte:** Jetzt fließen alle gelernten Konzepte in einer eigenen Anwendung zusammen.

In der nächsten Woche erhalten Sie die detaillierten Aufgabenbeschreibungen und wir starten mit der ersten Code-Zeile Ihrer Projekte!
