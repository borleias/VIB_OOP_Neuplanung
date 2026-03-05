# Woche 1: Refresher & Clean Code

In dieser Woche frischen wir Ihre C#-Kenntnisse auf und führen das Konzept des "Clean Code" ein. In der Verwaltungsinformatik ist Code oft langlebig und muss über Jahrzehnte von verschiedenen Personen gewartet werden. Daher ist die Lesbarkeit entscheidend.

## 1. C# Refresher: Die Basics im Verwaltungskontext

Ein typisches Objekt in der Verwaltung ist ein `Buerger` oder ein `Antrag`.

```csharp
public class Buerger
{
    // Properties mit klaren Namen
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public DateTime Geburtsdatum { get; set; }
    public string SteuerId { get; private set; } // Nur intern setzbar

    public Buerger(string vorname, string nachname, string steuerId)
    {
        Vorname = vorname;
        Nachname = nachname;
        SteuerId = steuerId;
    }

    public int BerechneAlter()
    {
        var heute = DateTime.Today;
        var alter = heute.Year - Geburtsdatum.Year;
        if (Geburtsdatum.Date > heute.AddYears(-alter)) alter--;
        return alter;
    }
}
```

## 2. Clean Code Prinzipien

## Warum Clean Code?
- Code wird öfter gelesen als geschrieben.
- "The only way to go fast, is to go well." (Robert C. Martin)
- Reduktion technischer Schulden.

### Aussagekräftige Namen
Vermeiden Sie Abkürzungen wie `a` oder `temp`. Nutzen Sie Fachbegriffe aus der Domäne (Verwaltung).

- **Variablen:** `int d;` (Schlecht) vs. `int antragsEingangsDatum;` (Gut).
- **Methoden:** `void DoIt();` (Schlecht) vs. `void SaveApplicationToDatabase();` (Gut).
- **Klassen:** Nomen verwenden, keine Verben. `AccountProcessor` statt `ProcessAccount`.

## 3. Funktionen (Methoden)
- **Kurz halten:** Eine Methode sollte idealerweise nicht mehr als 5-10 Zeilen haben.
- **Eines tun (Do One Thing):** Eine Methode sollte eine einzige, klar definierte Aufgabe erfüllen.
- **Keine Seiteneffekte:** Eine Methode `CheckPassword` sollte nicht gleichzeitig das Passwort in der Datenbank ändern.

## 4. Single Responsibility (auf Methodenebene)
Eine Methode sollte genau eine Sache tun. Wenn eine Methode `SpeichereUndSendeEmail` heißt, tut sie bereits zu viel.

## 5. DRY - Don't Repeat Yourself
Wiederholen Sie keine Logik. Wenn Sie die Berechnung einer Gebühr an drei Stellen im Code haben, extrahieren Sie diese in eine eigene Methode.

## 6. Magic Numbers und Strings vermeiden
Nutzen Sie Konstanten oder Enums statt "magischer" Werte.

*   **Schlecht:** `if (status == 1) { ... } // Was bedeutet 1?`
*   **Gut:** `if (status == AntragsStatus.Eingegangen) { ... }`

## 7. Kommentare
- **Guter Code erklärt sich selbst:** Kommentare sollten nur dort eingesetzt werden, wo die Absicht des Codes nicht offensichtlich ist (z.B. komplexe Algorithmen oder externe API-Besonderheiten).
- **Vermeiden:** Offensichtliche Kommentare wie `i++; // Erhöhe i um 1`.
- **Vermeiden:** Auskommentierter Code. (Nutze Git!)

## 8. Formatierung
- Konsistente Einrückung.
- Zusammengehörige Codeabschnitte optisch gruppieren.
- Befolgung der C# Coding Conventions (Microsoft).


## 9. Refactoring: Von Dirty zu Clean

Refactoring ist der Prozess, die Struktur von Code zu verbessern, ohne sein äußeres Verhalten zu ändern. 

**Typische Schritte:**
1.  Klassen für Datenobjekte erstellen.
2.  Logik aus der `Main`-Methode in spezialisierte Klassen auslagern.
3.  Magische Werte durch Enums ersetzen.
4.  Namen sprechend gestalten.
