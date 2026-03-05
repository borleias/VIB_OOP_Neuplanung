---
title: "Objektorientierte Programmierung (Vertiefung)"
subtitle: "Woche 4: SOLID-Prinzipien und Architektur"
author: "Dr. Peter Bernhardt"
date: "März 2026"
section-titles: true
---

# Teil 1: Vom Code zum System

## Willkommen zu Woche 4
In den ersten Wochen haben wir die handwerklichen Details betrachtet: wie formatiere ich eine Methode, wie nutze ich ein Design Pattern.
Heute zoomen wir heraus. Wir betrachten die System-Architektur. Wie bauen wir ein gesamtes "Fachverfahren", das auch nach Jahren noch pflegbar bleibt?

**Lernziele:**
- Die fünf SOLID-Prinzipien verstehen.
- Das Konzept der Dependency Injection (DI) meistern.
- Die klassische Schichtenarchitektur aufbauen können.

## Was ist SOLID?
SOLID ist ein Akronym, das von Robert C. Martin ("Uncle Bob") geprägt wurde. Es fasst fünf essenzielle Prinzipien des objektorientierten Designs zusammen.
Wenn Ihr Code diese Prinzipien verletzt, wird das System im Laufe der Zeit starr, zerbrechlich und immobil ("Spaghetti-Code").
Befolgen Sie SOLID, bauen Sie Software, die flexibel und robust ist.

# Teil 2: Die 5 SOLID-Prinzipien

## S – Single Responsibility Principle (SRP)
*"Eine Klasse sollte nur genau einen Grund haben, sich zu ändern."*
Das bedeutet: Eine Klasse hat nur exakt eine einzige Verantwortlichkeit (Aufgabe) im System.

## SRP: Das Problem der Gott-Klasse
Oft sehen wir in der Verwaltung Klassen wie den `AntragsManager`. 
Was tut er? Er prüft die Fachregeln, er baut einen SQL-String zusammen und ruft die Datenbank auf, er formatiert das PDF für den Ausdruck.
Das ist eine "Gott-Klasse" – sie weiß alles und tut alles. Wenn sich das Datenbankschema ändert, müssen wir den AntragsManager ändern. Wenn sich das Layout des PDFs ändert, müssen wir ihn wieder ändern.

## SRP: Die Lösung
Wir spalten die Gott-Klasse auf:
1. `AntragsValidator`: Prüft nur die Fachregeln (z.B. ist der Bürger alt genug?).
2. `AntragsRepository`: Kapselt nur den reinen Datenbankzugriff.
3. `PdfService`: Kümmert sich ausschließlich um das Generieren des Dokuments.
Jede Klasse hat nun nur noch **einen** Grund, sich zu ändern.

## O – Open-Closed Principle (OCP)
*"Software-Einheiten (Klassen, Module, Funktionen) sollten offen für Erweiterungen, aber geschlossen für Modifikationen sein."*
Das klingt wie ein Paradoxon: Wie erweitere ich ein System, ohne seinen Code zu verändern?

## OCP: In der Praxis
Wir haben dieses Prinzip beim **Strategy-Pattern** (Woche 3) kennengelernt!
Wenn wir eine neue Gebührensatzung für eine neue Stadt einführen wollen, passen wir nicht den bestehenden `GebuehrenRechner` an (geschlossen für Modifikation). Stattdessen schreiben wir eine völlig neue Klasse `SatzungLeipzig`, die ein Interface implementiert (offen für Erweiterung).

## L – Liskov Substitution Principle (LSP)
*"Objekte einer Unterklasse müssen sich so verhalten wie Objekte der Oberklasse, ohne dass das Programm fehlerhaft wird."*
Das bedeutet: Wer Vererbung nutzt, darf das zugesicherte Verhalten der Basisklasse nicht brechen.

## LSP: Das Problem mit Exceptions
Stellen Sie sich vor, Sie haben ein Interface `IDokument` mit der Methode `Drucken()`.
Sie haben `BescheidDokument` und `MeldeRegisterDokument`. Nun fügen Sie ein `DigitalOnlyDokument` hinzu, bei dem Sie die `Drucken()`-Methode mit einer `NotImplementedException` füllen.
Das bricht LSP! Eine Schleife, die über alle Dokumente geht und `Drucken()` aufruft, wird nun abstürzen.

## I – Interface Segregation Principle (ISP)
*"Clients sollten nicht dazu gezwungen werden, von Interfaces abzuhängen, die sie nicht benutzen."*
Es ist besser, viele kleine, spezifische Interfaces zu haben als ein riesiges, allgemeines.

## ISP: Kleine, fokussierte Interfaces
Wenn Sie ein riesiges Interface `IVerwaltungsVorgang` haben, das `Drucke()`, `Speichere()`, `Validiere()`, und `Archiviere()` vorschreibt, zwingen Sie alle Klassen, all dies zu implementieren, auch wenn ein spezieller Vorgang z.B. gar nicht gedruckt werden kann.
Lösung: Trennen Sie in `IPrintable`, `ISavable`, `IValidatable`.

## D – Dependency Inversion Principle (DIP)
*"Abhängigkeiten sollten gegen Abstraktionen gerichtet sein, nicht gegen konkrete Klassen."*
High-Level-Module (Fachlogik) sollten nicht von Low-Level-Modulen (Datenbank-Zugriff) abhängen. Beide sollten von Abstraktionen (Interfaces) abhängen.

# Teil 3: Dependency Injection (DI)

## Dependency Injection: Die Umsetzung des DIP
Das Dependency Inversion Principle führt uns zur Dependency Injection. 
Die Kernidee: "Frag nicht nach deinen Abhängigkeiten, lass sie dir geben." (Hollywood-Prinzip: Don't call us, we call you).

## DI: Enge Kopplung (Das Anti-Pattern)
In diesem Beispiel erstellt die Klasse ihre Abhängigkeit selbst. Sie ist "eng gekoppelt" an die SQL-Datenbank.

```csharp
public class Fachverfahren
{
    // Die Abhängigkeit wird hart verdrahtet
    private SqlDatenbank _db = new SqlDatenbank(); 

    public void Verarbeite(Antrag a) 
    {
        _db.Save(a);
    }
}
```
Wie wollen wir diese Klasse jemals mit einer Mock-Datenbank (für Tests) aufrufen? Es ist unmöglich!

## DI: Lose Kopplung (Die Lösung)
Wir verlangen eine Abstraktion (Interface) über den Konstruktor.

```csharp
public class Fachverfahren
{
    private readonly IDatenbank _db;

    // Konstruktor-Injection: Wir lassen uns die DB geben
    public Fachverfahren(IDatenbank db) 
    {
        _db = db;
    }

    public void Verarbeite(Antrag a) => _db.Save(a);
}
```

## DI-Container: Das Herz moderner Anwendungen
In modernen Frameworks (wie ASP.NET Core) müssen wir nicht hunderte Male `new` aufrufen, um die Klassen zusammenzustecken.
Ein **DI-Container** übernimmt das. Wir konfigurieren ihn einmal beim Programmstart:
*"Wenn jemand eine `IDatenbank` benötigt, erzeuge eine Instanz von `SqlDatenbank` und gib sie ihm."*
Der Container löst den gesamten Abhängigkeitsbaum automatisch auf.

# Teil 4: Schichtenarchitektur

## Layered Architecture (Schichtenarchitektur)
Wenn Fachverfahren wachsen, brauchen sie eine grobe Struktur. Wir teilen den Code in logische Schichten (Layers).
Die absolut wichtigste Regel: **Abhängigkeiten zeigen immer nur in eine Richtung, von oben nach unten.**

## Die drei klassischen Schichten
1. **Presentation Layer (UI):** Das Gesicht der Anwendung.
2. **Business Logic Layer (BLL):** Das Gehirn der Anwendung.
3. **Data Access Layer (DAL):** Das Gedächtnis der Anwendung.

## Presentation Layer (User Interface)
Diese Schicht kümmert sich um die Interaktion mit dem Nutzer.
- Typen: Console-App, WPF Desktop-App, ASP.NET Web API, MVC-Webseite.
- Darf **nur** die Business-Schicht aufrufen.
- Enthält **keine** Geschäftsregeln (z.B. Gebührenberechnung).

## Business Logic Layer (BLL)
Hier sitzt das eigentliche Fachwissen der Behörde.
- Prüft Gesetze, führt Berechnungen aus, orchestriert Workflows.
- Darf **nur** auf die Data Access Schicht zugreifen.
- Ist unabhängig von der UI (kann im Web oder auf der Konsole exakt gleich genutzt werden!).

## Data Access Layer (DAL)
Kümmert sich rein um das Speichern und Laden von Daten.
- Verbindungen zur SQL-Datenbank, Lesen von XML-Dateien, Aufruf von externen REST-APIs.
- Kennt keine andere Schicht. Darf keine Fachlogik enthalten.

## Praxis-Beispiel: Projektstruktur in .NET
So sieht eine saubere Solution in Visual Studio aus:

```text
Solution: BehoerdenFachverfahren
├── Projekt.UI (Console App)
│   └── Referenziert Projekt.Business
│
├── Projekt.Business (Class Library)
│   ├── Interfaces (z.B. IDatenbank, IAntragService)
│   ├── Models (z.B. Antrag, Buerger)
│   ├── Services (Fachlogik)
│   └── Referenziert Projekt.Data
│
└── Projekt.Data (Class Library)
    ├── SqlRepository (Implementiert IDatenbank)
    └── Referenziert keine anderen Projekte!
```

## Das Geheimnis der Abstraktion
Wie kann der Business-Layer die Datenbank aufrufen, wenn die Abhängigkeit nach unten zeigt?
Indem der Business-Layer die **Interfaces** definiert (z.B. `IDatenbank`), und der Data-Layer diese Interfaces **implementiert**.
Das ist die perfekte Anwendung des Dependency Inversion Principles!

# Teil 5: Zusammenfassung

## Wrap-up Woche 4
- **SOLID-Prinzipien** bewahren Code vor dem schleichenden Verfall und ermöglichen nachhaltige Software.
- **Dependency Injection** sorgt für lose Kopplung und ist die Voraussetzung für moderne, testbare Architekturen.
- Die **Schichtenarchitektur** trennt UI, Fachlogik und Datenhaltung strikt voneinander und erlaubt den isolierten Austausch von Komponenten.

## Ausblick auf Woche 5
Wir haben nun gelernt, wie man Architekturen baut, die "testbar" sind.
In der nächsten Woche schauen wir uns an, wie wir dieses Potenzial nutzen: Wir steigen in das **Automatisierte Unit Testing** mit xUnit und in das **Test-Driven Development (TDD)** ein.
