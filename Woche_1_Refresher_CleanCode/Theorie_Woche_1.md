# Woche 1: Refresher & Clean Code – Vom Skript zum professionellen System

Herzlich willkommen zum Vertiefungsfach "Objektorientierte Programmierung". In dieser ersten Woche schlagen wir die Brücke zwischen dem bloßen "Schreiben von Code, der funktioniert" und dem "Entwickeln von Software, die wartbar ist".

Gerade in der Verwaltungsinformatik begegnen uns Systeme, die über Jahrzehnte im Einsatz sind. Code, den Sie heute schreiben, wird vielleicht in 15 Jahren von einem Kollegen angepasst werden müssen, der Sie nie kennengelernt hat. **Clean Code** ist kein Luxus, sondern eine professionelle Notwendigkeit.

---

## 1. Der Mindset-Shift: Prozedural vs. Objektorientiert

Bisher haben Sie C# oft wie eine Abfolge von Befehlen genutzt (prozedural). In der OOP denken wir nicht in Schritten, sondern in **Verantwortlichkeiten** und **Objekten**.

*   **Prozedural:** "Erst prüfe ich das Alter, dann berechne ich die Gebühr, dann drucke ich den Bescheid." (Fokus: Algorithmus)
*   **Objektorientiert:** "Ein `Antrag` kennt seinen Status. Ein `Gebührenrechner` weiß, wie er einen `Antrag` bewertet. Ein `Bürger` besitzt Daten." (Fokus: Akteure und deren Interaktion)

### Warum OOP?
1.  **Kapselung:** Daten und die Logik, die diese Daten manipuliert, gehören zusammen.
2.  **Wiederverwendbarkeit:** Ein gut designter `AdressValidator` kann in zehn verschiedenen Programmen genutzt werden.
3.  **Wartbarkeit:** Änderungen an der Logik betreffen nur eine Stelle (die Klasse), nicht das gesamte Programm.

---

## 2. C# Refresher: Die Bausteine

### 2.1 Klassen und Objekte
Eine **Klasse** ist der Bauplan (z.B. "Antrag"), ein **Objekt** ist das konkrete Haus (z.B. "Der Bauantrag von Herrn Müller").

### 2.2 Properties und Sichtbarkeit (Access Modifiers)
Nutzen Sie `public`, `private`, `protected` und `internal` bewusst. **Geheimnisprinzip (Information Hiding):** Geben Sie nur das preis, was unbedingt nötig ist.

```csharp
public class Antrag
{
    // Die ID darf von außen gelesen, aber nur intern gesetzt werden (Sicherheit!)
    public string Id { get; private set; }
    
    // Properties kapseln Felder
    public DateTime Eingangsdatum { get; set; }

    public Antrag(string id)
    {
        Id = id;
        Eingangsdatum = DateTime.Now;
    }
}
```

### 2.3 Konstruktoren
Ein Objekt sollte niemals in einem "ungültigen" Zustand existieren. Der Konstruktor stellt sicher, dass alle notwendigen Daten beim Erstellen vorhanden sind.

---

## 3. Was ist Clean Code?

Der Begriff wurde maßgeblich von Robert C. Martin ("Uncle Bob") geprägt. Clean Code ist Code, der sich wie eine gut geschriebene Erzählung liest.

### 3.1 Die Kunst der Namensgebung
Namen sind die wichtigste Dokumentation im Code.

*   **Vermeiden Sie technisches Kauderwelsch:** `List<string> list1` sagt nichts aus. `List<string> registrierteBuerger` ist klar.
*   **Keine Abkürzungen:** In der Verwaltung gibt es viele Abkürzungen (z.B. "VwVfG"). Im Code sollten wir jedoch ausschreiben: `Verwaltungsverfahrensgesetz`.
*   **Sprechende Namen:** Eine Variable `d` für "Tage seit Eingang" ist ein Rätsel. `tageSeitAntragseingang` ist eine Antwort.

### 3.2 Funktionen (Methoden) – Die "Do One Thing"-Regel
Eine Methode sollte genau **eine** Aufgabe haben.

*   **Länge:** Wenn eine Methode nicht auf einen Bildschirm passt, ist sie zu lang. Ziel: 5 bis 15 Zeilen.
*   **Abstraktionsebene:** Mischen Sie nicht High-Level-Logik (z.B. "Prüfe Anspruch") mit Low-Level-Details (z.B. "Schreibe 'true' in SQL-Datenbank").
*   **Argumente:** Je weniger, desto besser. Drei Argumente sind das Maximum, bevor es unübersichtlich wird. Nutzen Sie stattdessen Objekte als Parameter.

### 3.3 Keine Seiteneffekte (Side Effects)
Eine Methode namens `IstBuergerVolljaehrig(Buerger b)` sollte nur `true` oder `false` zurückgeben. Sie sollte **nicht** heimlich das Geburtsdatum im Objekt ändern oder einen Log-Eintrag in die Datenbank schreiben.

---

## 4. Anti-Patterns und wie man sie vermeidet

### 4.1 Magic Numbers und Strings
Zahlen wie `if (status == 4)` sind "magisch" und gefährlich. Was ist 4? Abgelehnt? In Bearbeitung?
**Lösung:** Nutzen Sie `Enums`.

```csharp
public enum AntragsStatus
{
    Eingereicht,
    InPruefung,
    Abgeschlossen,
    Abgelehnt
}
// Viel besser:
if (meinAntrag.Status == AntragsStatus.Abgelehnt) { ... }
```

### 4.2 Kommentare als "Geruchs-Deo"
Schreiben Sie keine Kommentare, um schlechten Code zu erklären. **Refaktorisieren Sie den Code stattdessen.**
*   *Schlecht:* `// Prüft ob Bürger älter als 18 ist und in Berlin wohnt` -> gefolgt von komplexem If-Statement.
*   *Gut:* `if (BuergerIstWahlberechtigt(buerger))` -> Die Methode erklärt sich selbst.

---

## 5. Refactoring-Workflow: Das "Dirty Library System"

In der Übung dieser Woche nehmen wir ein "klassisches" Anfängerprogramm (Dirty) und überführen es in ein professionelles System (Clean).

**Die Strategie:**
1.  **Datenstrukturen identifizieren:** Aus `string[]` Arrays werden echte Klassen (`Buch`, `Nutzer`).
2.  **Verantwortlichkeiten trennen:** Die `Main`-Methode steuert nur den Ablauf. Die Logik wandert in eine Klasse `BibliotheksVerwaltung`.
3.  **Interfaces nutzen (Vorschau):** Wie könnten wir später eine Datenbank statt einer Liste anbinden?

---

## 6. Zusammenfassung der Goldenen Regeln
1.  **DRY (Don't Repeat Yourself):** Kopieren Sie niemals Code.
2.  **KISS (Keep It Simple, Stupid):** Wählen Sie die einfachste Lösung, nicht die "schlaueste".
3.  **Boy Scout Rule:** Hinterlassen Sie den Code immer ein Stück sauberer, als Sie ihn vorgefunden haben.
