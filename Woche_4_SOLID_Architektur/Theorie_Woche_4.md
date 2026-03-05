# Woche 4: SOLID-Prinzipien und Schichtenarchitektur

In den ersten Wochen haben wir gelernt, wie man sauberen Code schreibt und bewährte Muster (Design Patterns) anwendet. Diese Woche heben wir unseren Blickwinkel auf die Ebene des **System-Designs**. Wir beschäftigen uns mit den **SOLID-Prinzipien** – den fünf Grundpfeilern objektorientierter Softwareentwicklung – und schauen uns an, wie man eine Anwendung sinnvoll strukturiert (**Schichtenarchitektur**).

---

## 1. Die SOLID-Prinzipien am Beispiel eines "Fachverfahrens"

SOLID ist ein Akronym, das für fünf Prinzipien steht, die Software wartbarer, flexibler und verständlicher machen sollen.

### S – Single Responsibility Principle (Einzelverantwortung)
*Eine Klasse sollte nur genau einen Grund haben, sich zu ändern.*

*   **Schlecht:** Eine Klasse `AntragsManager`, die den Antrag validiert, ihn in die SQL-Datenbank speichert und am Ende ein PDF-Dokument erzeugt.
*   **Gut:** Drei getrennte Klassen: `AntragsValidator`, `AntragsRepository` (Datenbank) und `PdfService`. Wenn sich das Datenbankformat ändert, muss der `PdfService` nicht angefasst werden.

### O – Open-Closed Principle (Offen-Geschlossen)
*Software-Einheiten sollten offen für Erweiterungen, aber geschlossen für Modifikationen sein.*

*   **Beispiel:** Sie haben ein System zur Berechnung von Wohngeld. Wenn eine neue gesetzliche Regelung hinzukommt, sollten Sie nicht den bestehenden Code umschreiben (Modifikation), sondern eine neue Strategie hinzufügen (Erweiterung). Dies haben wir bereits beim *Strategy Pattern* gesehen.

### L – Liskov Substitution Principle (Ersetzbarkeit)
*Objekte einer Unterklasse müssen sich so verhalten wie Objekte der Oberklasse, ohne dass das Programm fehlerhaft wird.*

*   **Beispiel:** Wenn Sie eine Basisklasse `Bescheid` haben, muss jede Unterklasse (z.B. `Ablehnung`) alle Methoden der Basisklasse sinnvoll unterstützen. Eine Unterklasse sollte niemals eine Methode mit `NotImplementedException` überschreiben, nur weil sie diese nicht braucht.

### I – Interface Segregation Principle (Trennung von Schnittstellen)
*Clients sollten nicht dazu gezwungen werden, von Interfaces abzuhängen, die sie nicht benutzen.*

*   **Schlecht:** Ein riesiges Interface `IVerwaltungsAktion` mit Methoden für `Drucken()`, `Speichern()`, `Validieren()` und `Archivieren()`.
*   **Gut:** Mehrere kleine Interfaces: `IPrintable`, `ISavable`, `IValidatable`. Ein Sachbearbeiter-Modul implementiert nur das, was es wirklich braucht.

### D – Dependency Inversion Principle (Abhängigkeitsumkehr)
*Abhängigkeiten sollten gegen Abstraktionen (Interfaces) gerichtet sein, nicht gegen konkrete Klassen.*

---

## 2. Dependency Injection (DI)

Das Dependency Inversion Principle führt uns direkt zur **Dependency Injection**. Statt dass eine Klasse sich ihre Abhängigkeiten selbst erstellt (mit `new`), bekommt sie diese "injiziert" (meist über den Konstruktor).

### Das Problem (enge Kopplung)
```csharp
public class Fachverfahren
{
    private SqlDatenbank _db = new SqlDatenbank(); // Fest verdrahtet!

    public void Verarbeite(Antrag a) => _db.Save(a);
}
```

### Die Lösung (lose Kopplung mit DI)
```csharp
public class Fachverfahren
{
    private readonly IDatenbank _db;

    // Konstruktor-Injection: Wir verlangen ein Interface
    public Fachverfahren(IDatenbank db) => _db = db;

    public void Verarbeite(Antrag a) => _db.Save(a);
}
```

**DI-Container:** In modernen C#-Anwendungen (wie ASP.NET Core) übernimmt ein "Container" die Arbeit. Wir sagen ihm einmal: "Wann immer jemand `IDatenbank` verlangt, gib ihm eine Instanz von `SqlDatenbank`."

---

## 3. Schichtenarchitektur (Layered Architecture)

Damit eine Anwendung nicht im Chaos versinkt, teilen wir sie in logische Schichten auf. Jede Schicht hat eine klare Aufgabe und darf nur mit der Schicht direkt unter ihr kommunizieren.

### Die drei klassischen Schichten:

1.  **Presentation Layer (UI):**
    *   Verantwortung: Interaktion mit dem Nutzer (Webseite, Desktop-App, Console).
    *   Kennt: Nur den Business Layer.
2.  **Business Logic Layer (BLL):**
    *   Verantwortung: Die "Fachlogik". Hier werden Gesetze geprüft, Berechnungen durchgeführt und Validierungen gemacht. Das Herzstück der Anwendung.
    *   Kennt: Nur den Data Access Layer.
3.  **Data Access Layer (DAL):**
    *   Verantwortung: Speichern und Laden von Daten (Datenbank, Dateisystem, externe APIs).
    *   Kennt: Niemanden (außer der Datenbank).

### Warum Schichten?
*   **Austauschbarkeit:** Sie können die Web-UI durch eine Mobile-App ersetzen, ohne die Fachlogik zu ändern.
*   **Testbarkeit:** Sie können die Fachlogik testen, ohne eine echte Datenbank zu benutzen (Mocking).
*   **Wartbarkeit:** Fehler lassen sich schneller eingrenzen.

---

## 4. Praxis-Beispiel: Struktur eines Fachverfahrens

```text
Projekt.UI (Console App)
  └── Referenziert Projekt.Business

Projekt.Business (Class Library)
  ├── Interfaces (IDatenbank, IAntragService)
  ├── Models (Antrag, Buerger)
  └── Services (BerechnungsService, ValidierungsService)
  └── Referenziert Projekt.Data

Projekt.Data (Class Library)
  ├── SqlRepository
  └── CsvRepository
```

---

## Zusammenfassung
*   **SOLID:** Die "Verfassung" für guten Code. Wer sich daran hält, baut Systeme, die nicht "verrotten".
*   **Dependency Injection:** "Frag nicht nach Daten, lass sie dir geben." Ermöglicht lose Kopplung.
*   **Schichtenarchitektur:** Trennung von Darstellung, Logik und Datenhaltung. Sorgt für Ordnung im System.
