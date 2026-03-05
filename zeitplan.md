# Zeitplan: Vertiefungsfach Objektorientierte Programmierung (C#)

**Zielgruppe:** Verwaltungsinformatiker (3. Semester)
**Umfang:** 8 Wochen à 3 Zeitstunden
**Voraussetzungen:** Grundkurs C# (Prozedural & Basis-OOP)
**Prüfungsleistung:** Umsetzung einer mittelgroßen Programmieraufgabe + Präsentation

---

## Modulübersicht

| Woche | Thema | Fokus & Übung |
| :--- | :--- | :--- |
| **1** | **Refresher & Clean Code** | Wiederholung C# OOP; Namenskonventionen, Methoden-Design, Kommentare vs. Code-Klarheit. |
| **2** | **Design Patterns I (Erzeugung & Struktur)** | Singleton, Factory Method, Adapter, Decorator. Praxis: Entkopplung von Komponenten. |
| **3** | **Design Patterns II (Verhalten)** | Observer, Strategy, State. Praxis: Dynamisches Programmverhalten ohne Riesen-If-Else. |
| **4** | **SOLID-Prinzipien & Architektur** | Deep Dive SOLID; Schichtenarchitektur vs. Dependency Injection. Praxis: Umbau eines "Spaghetti-Codes". |
| **5** | **Qualitätssicherung & Refactoring** | Unit Testing (xUnit/NUnit), Test Driven Development (TDD) Basics, Refactoring-Techniken. |
| **6** | **Projektstart & Fortgeschrittene Konzepte** | Ausgabe der Abschlussaufgaben; Arbeit mit APIs/Datenbanken (Entity Framework Core Basics) oder Async/Await. |
| **7** | **Projektwerkstatt & Coaching** | Intensive Betreuung der Projekte; Individuelle Problemlösung; Architektur-Review der Studierenden-Lösungen. |
| **8** | **Abschlusspräsentationen** | Vorstellung der Ergebnisse; Code-Review in der Gruppe; Reflexion der angewandten Muster. |

---

## Detaillierte Wochenplanung

### Woche 1: Von "Funktioniert" zu "Lesbar" (Clean Code)
*   **Theorie:** Warum OOP im Großen anders ist. Einführung in Clean Code (Uncle Bob).
*   **Praxis:** Analyse eines bestehenden (schlechten) C#-Programms. Refactoring von Variablennamen und langen Methoden.
*   **Hausaufgabe:** Kleines Refactoring-Szenario.

### Woche 2: Baupläne und Schnittstellen (Creational & Structural Patterns)
*   **Theorie:** Komposition über Vererbung. Dependency Inversion Prinzip (Vorschau). Fokus: Factory und Adapter.
*   **Praxis:** Implementierung eines Logger-Adapters oder einer flexiblen Objekt-Erzeugung für verschiedene Verwaltungsvorgänge.

### Woche 3: Flexibilität im Ablauf (Behavioral Patterns)
*   **Theorie:** Strategie-Muster für austauschbare Algorithmen. Observer für Event-getriebene Programmierung.
*   **Praxis:** Simulation eines Workflow-Systems (z.B. Genehmigungsprozess), bei dem sich das Verhalten je nach Status ändert.

### Woche 4: Das Fundament (SOLID & DI)
*   **Theorie:** Jedes SOLID-Prinzip im Detail mit C#-Beispielen. Einführung in Dependency Injection Container (Microsoft.Extensions.DependencyInjection).
*   **Praxis:** Entkopplung einer Applikation von der Datenquelle (Mocking-Vorbereitung).

### Woche 5: Vertrauen in den Code (Testing & Refactoring)
*   **Theorie:** Die Test-Pyramide. Schreiben von testbarem Code. Warum statische Methoden oft problematisch sind.
*   **Praxis:** Schreiben von Unit Tests für die Logik aus Woche 3.

### Woche 6: Die große Aufgabe (Architektur & Projektstart)
*   **Theorie:** Wie starte ich ein Projekt? Anforderungsanalyse -> Klassendiagramm -> Skelett.
*   **Praxis:** Vorstellung der Themen für die Abschlussprüfung (z.B. Bibliotheksverwaltung, Antragsmanagementsystem, Fuhrparkverwaltung). Start der Modellierung.

### Woche 7: Umsetzung & Feinschliff
*   **Betreuung:** Die 3 Stunden werden als offene Werkstatt genutzt. Der Dozent fungiert als Senior Developer / Architekt für die "Junior Teams".
*   **Fokus:** Anwendung von mindestens zwei Design Patterns pro Projekt.

### Woche 8: Showtime
*   **Präsentation:** 15 Min. Vorstellung + 5 Min. Q&A pro Student.
*   **Inhalt:** Fokus liegt nicht nur auf "Es läuft", sondern auf "Warum habe ich dieses Pattern gewählt?".
