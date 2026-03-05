---
title: "Objektorientierte Programmierung (Vertiefung)"
subtitle: "Woche 6: Fortgeschrittene Konzepte und Projektstart"
author: "Dr. Peter Bernhardt"
date: "März 2026"
section-titles: true
---

# Teil 1: Einführung & Das Warten auf Daten

## Willkommen zu Woche 6
Wir haben den theoretischen Gipfel dieses Kurses erreicht. Architekturen, Patterns, SOLID und Testing liegen hinter uns.
Heute betrachten wir die Brücke zur realen Welt: Wie interagieren unsere sauberen Systeme effizient und ausfallsicher mit der Außenwelt (APIs, Datenbanken)?
Zudem geben wir heute den Startschuss für Ihre praktischen Abschlussprojekte.

**Lernziele:**
- Asynchrone Programmierung mit `async` / `await` verstehen.
- Fehler in komplexen Systemen robust behandeln (Error Handling).
- Das Abschlussprojekt planen und aufsetzen.

## Die vernetzte Verwaltung
Kein modernes Fachverfahren arbeitet mehr isoliert.
Wir müssen das zentrale Melderegister abfragen, PDF-Generatoren in der Cloud ansprechen oder Geodaten bei einem Karten-Dienst laden.
Diese Abfragen über das Netzwerk oder die Festplatte benötigen Zeit. Manchmal Millisekunden, manchmal Sekunden.

## Das Problem der Synchronität
Wenn wir traditionell (synchron) programmieren, blockiert der aktuelle Thread unseres Programms, bis die externe Antwort da ist.
In einer Desktop-Anwendung führt das dazu, dass die Oberfläche "einfriert" (Keine Rückmeldung).
In einer Web-Anwendung blockieren wir den Webserver für andere Bürger, weil unser Server däumchendrehend auf die Datenbank wartet.

# Teil 2: Asynchrone Programmierung (Async / Await)

## Die Lösung: Asynchrone Ausführung
Asynchronität bedeutet: Das Programm schickt die Anfrage an das Netzwerk ab und sagt dem Thread: "Du bist frei, mach in der Zwischenzeit andere Arbeit (z.B. andere Bürger bedienen). Sag mir Bescheid, wenn die Daten da sind, dann rechne ich hier weiter."

## C# und die Task Parallel Library (TPL)
C# hat asynchrone Programmierung revolutioniert. Anstatt komplexe "Callbacks" zu schreiben, bietet C# zwei magische Schlüsselwörter:
`async` und `await`.
Zusammen mit der Klasse `Task<T>` ermöglichen sie uns, asynchronen Code zu schreiben, der sich fast genau so liest wie synchroner Code.

## Beispiel: Asynchrone Abfrage (Code)
Wir kennzeichnen die Methode mit `async Task<T>` und warten mit `await` auf das Netzwerk.

```csharp
using System.Net.Http;
using System.Threading.Tasks;

public class MeldeService
{
    // Die Methode gibt keinen string zurück, sondern ein "Versprechen" (Task) auf einen String
    public async Task<string> HoleBuergerDatenAsync(string id) 
    {
        HttpClient client = new HttpClient();
        
        // await "pausiert" diese Methode hier, ohne den Thread zu blockieren!
        string daten = await client.GetStringAsync($"https://api.behoerde.de/{id}");
        
        // Erst wenn die Daten da sind, geht es hier weiter
        return daten;
    }
}
```

## Parallelität: Der Turbo-Boost
Noch mächtiger wird es, wenn wir Daten aus *mehreren* Quellen brauchen. 
Anstatt Quelle A abzufragen (warten), dann Quelle B (warten), können wir beide Anfragen gleichzeitig auf die Reise schicken.

## Parallele Ausführung (Code)
```csharp
public async Task<BuergerDaten> HoleVollstaendigeDatenAsync(string id)
{
    // Wir rufen die Methoden auf, OHNE 'await' davor. 
    // Das startet die Anfragen parallel im Hintergrund.
    var meldeTask = _apiClient.GetMelderegisterAsync(id);
    var steuerTask = _apiClient.GetSteuerDatenAsync(id);

    // Jetzt sagen wir: Wir können erst weitermachen, 
    // wenn BEIDE Aufgaben fertig sind.
    await Task.WhenAll(meldeTask, steuerTask);

    // Hier sind garantiert beide Ergebnisse verfügbar
    return new BuergerDaten {
        Name = meldeTask.Result.Name,
        SteuerKlasse = steuerTask.Result.Klasse
    };
}
```

## Die goldene Regel: "Async all the way"
Wenn Sie im Keller Ihrer Architektur (Data Access Layer) asynchron werden, müssen Sie die Asynchronität bis ganz nach oben (Presentation Layer) durchziehen.
- `async` ist wie ein Virus, das sich im positiven Sinne durch den Call-Stack frisst.
- Eine synchrone Methode, die eine asynchrone Methode aufruft, ist ein Anti-Pattern.

## Die Deadlock-Gefahr
Nutzen Sie **niemals** `.Wait()` oder `.Result` auf einem `Task` innerhalb einer normalen (synchronen) Methode, um Asynchronität zu erzwingen.
In UI-Frameworks (WPF) oder Web-Kontexten führt dies fast unweigerlich zu **Deadlocks** (das Programm verhakt sich in sich selbst und steht still).
**Regel:** Wo ein `Task` ist, muss ein `await` sein.

# Teil 3: Robustes Error Handling

## Warum try-catch oft nicht reicht
In Anfänger-Skripten fängt man oft einfach alles ab, um Abstürze zu verhindern:
```csharp
try { MacheEtwas(); } 
catch { /* Ignorieren */ }
```
In großen Fachverfahren ist dieses "Verschlucken" von Fehlern fatal. Der Bürger denkt, der Antrag ging durch, aber er liegt nie in der Datenbank.

## Das "Fail Fast" Prinzip
Prüfen Sie Rahmenbedingungen so früh wie möglich an der Außengrenze Ihres Systems (z.B. im Controller).
Wenn die `SteuerId` des Antrags leer ist, macht es keinen Sinn, den Service aufzurufen, die DB zu öffnen etc.
Werfen Sie sofort eine Exception (z.B. `ArgumentException`) und brechen Sie ab.

## Spezifische Exceptions fangen
Fangen Sie nur Fehler, die Sie auch *sinnvoll behandeln* können.
Wenn die Festplatte brennt (`OutOfMemoryException`), kann Ihr Code das nicht beheben. Lassen Sie das Programm kontrolliert abbrechen!
Fangen Sie spezifische Dinge wie `SqlException` (um z.B. einen Verbindungsversuch 3 Sekunden später nochmal zu wiederholen).

## Fachliche vs. Technische Fehler
- **Fachlich (User-Fehler):** Der Bürger ist zu jung für den Führerschein. Das Programm funktioniert einwandfrei. Wir zeigen dem Nutzer eine nette Meldung: "Sie erfüllen die Voraussetzungen nicht."
- **Technisch (System-Fehler):** Die Datenbank antwortet nicht. Wir zeigen dem Nutzer: "Systemfehler, Support informiert", loggen aber für uns den exakten Stacktrace.

## Die Wichtigkeit von Logging
"Ein Fehler, der nicht geloggt wurde, hat nie stattgefunden."
Ohne Logdateien sind Sie als Administrator blind. Wenn um 3 Uhr nachts ein Fehler auftritt, brauchen Sie den Stacktrace, die Uhrzeit und idealerweise die ID des betroffenen Antrags.
Nutzen Sie in C# moderne Bibliotheken wie `Serilog` oder `NLog`.

## Beispiel: Globales Fehlerhandling (Code)
Oft fängt man Fehler ganz oben (in der UI) global ab, um dem Nutzer immer eine saubere Antwort zu geben.

```csharp
try 
{
    await _service.VerarbeiteAntragAsync(antrag);
    ZeigeErfolgsMeldung();
}
catch (AntragUngueltigException ex)
{
    // Fachlicher Fehler (z.B. Nutzer hat was falsches eingegeben)
    _logger.Warning("Ungültiger Antrag von User {U}: {Msg}", antrag.UserId, ex.Message);
    ZeigeFehlerMeldung("Bitte prüfen Sie Ihre Eingaben.");
}
catch (Exception ex)
{
    // Technischer Fehler (z.B. Datenbank weg)
    _logger.Error(ex, "Kritischer technischer Fehler");
    ZeigeFehlerMeldung("Ein technisches Problem ist aufgetreten. Versuch später erneut.");
}
```

# Teil 4: Projektstart

## Das Abschlussprojekt
Ab heute beginnt die Praxis-Phase. Sie haben nun den restlichen Kurszeitraum Zeit, ein eigenes Fachverfahren zu entwickeln.
Es geht nicht darum, ein riesiges System mit hunderten Funktionen zu bauen. Es geht darum, ein **kleines System architektonisch perfekt** zu konstruieren.

## Die Architektur-Anforderungen
Ihr Code muss die Konzepte der letzten Wochen widerspiegeln:
1. **Schichtenarchitektur:** Trennen Sie klar zwischen UI (z.B. Console), Business-Logik und Datenhaltung.
2. **SOLID:** Wenden Sie die Prinzipien an (insbesondere Dependency Injection).
3. **Design Patterns:** Nutzen Sie mindestens zwei Muster (z.B. Factory für die Dokumentenerzeugung, Strategy für Regeln).
4. **Testing:** Sichern Sie die komplexeste Klasse Ihres Business-Layers mit xUnit ab.

## Themenvorschläge (Auswahl)
Wählen Sie ein Projekt aus der Liste in `Projektaufgaben.md` oder schlagen Sie ein eigenes vor.
Einige Highlights:
- **KFZ-Zulassungsstelle:** Verwaltung von Fahrzeugen, Haltern und Kennzeichen-Reservierungen.
- **Bauantrags-Workflow:** State-Machine für komplexe Antragsprozesse.
- **Wohngeld-Rechner:** Hochkomplexe, testbare Regelwerke (Strategy-Pattern!).
- **Digitales Fundbüro:** Matching-Algorithmen und Observer-Pattern für Benachrichtigungen.

## Der Workflow für die nächsten Wochen
- Überlegen Sie sich Ihr Thema und definieren Sie das Kern-Feature.
- Skizzieren Sie die Klassenstruktur (Welche Schichten? Welche Patterns?).
- Starten Sie mit der Fachlogik und schreiben Sie Tests (TDD!).
- Bauen Sie die UI und die Datenbank-Anbindung erst ganz am Schluss um Ihre Logik herum.

## Zusammenfassung & Startschuss
- Asynchronität und Fehlerbehandlung sind die Kür für den Produktivbetrieb.
- Jetzt liegt es an Ihnen, das Theoriewissen in praktischen, langlebigen Code zu gießen.
- Viel Erfolg bei der Konzeption und Programmierung Ihrer Abschlussprojekte! Die "Projektwerkstatt" ist hiermit eröffnet.
