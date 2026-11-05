# Modulbeschreibung: Objektorientierte Programmierung

## Modulprofil

| Merkmal | Beschreibung |
|---|---|
| Modulbezeichnung | Objektorientierte Programmierung |
| Einordnung | Freiwilliges Vertiefungsfach im Studiengang Verwaltungsinformatik |
| Zielgruppe | Studierende im dritten Fachsemester |
| Lehrsprache | Deutsch |
| Lehrumfang | Acht Wochen mit jeweils drei Zeitstunden (insgesamt 24 Zeitstunden Präsenzlehre) |
| Prüfungsform | Praktische Programmieraufgabe mit Präsentation und fachlichem Gespräch |

Angaben zu ECTS-Leistungspunkten, Gesamtarbeitsaufwand, Prüfungsdauer und formalen Zulassungsvoraussetzungen sind in den vorliegenden Kursunterlagen nicht festgelegt und müssen bei Bedarf institutionell ergänzt werden.

## Kurzbeschreibung

Das Modul vertieft die objektorientierte Softwareentwicklung mit C#. Aufbauend auf grundlegenden Kenntnissen der prozeduralen und objektorientierten Programmierung lernen die Studierenden, mittelgroße Programmieraufgaben strukturiert zu analysieren, geeignete Entwurfsentscheidungen zu treffen und diese in eine wartbare Softwarelösung zu überführen. Im Mittelpunkt stehen Clean Code, ausgewählte Entwurfsmuster, SOLID-Prinzipien, Schichtenarchitektur, Dependency Injection, automatisierte Tests und Refactoring.

Die Konzepte werden anhand praxisnaher Beispiele vermittelt und in Übungen angewendet. In einer eigenständigen Programmieraufgabe aus einem verwaltungsnahen Anwendungskontext setzen die Studierenden ausgewählte Konzepte um, sichern zentrale Fachlogik durch Tests ab und erläutern ihre Architektur- und Entwurfsentscheidungen.

## Qualifikationsziele und Lernergebnisse

Nach erfolgreichem Abschluss des Moduls können die Studierenden:

1. objektorientierte C#-Grundlagen auf umfangreichere Aufgabenstellungen übertragen und fachliche Anforderungen in geeignete Klassen und Verantwortlichkeiten überführen;
2. lesbaren und wartbaren Code verfassen sowie grundlegende Refactoring-Techniken anwenden;
3. wiederkehrende Entwurfsprobleme erkennen und geeignete Muster – insbesondere Factory Method, Adapter, Strategy, Observer und State – problembezogen auswählen und einsetzen;
4. die SOLID-Prinzipien erläutern und bei der Gestaltung von Klassen, Schnittstellen und Abhängigkeiten berücksichtigen;
5. eine einfache Schichtenarchitektur mit getrennter Benutzeroberfläche, Fachlogik und Datenzugriff entwerfen;
6. Abhängigkeiten über Abstraktionen gestalten und Dependency Injection zur Entkopplung und besseren Testbarkeit nutzen;
7. Unit Tests für zentrale Fachlogik entwickeln und Testergebnisse zur Überprüfung von Änderungen und Refactorings heranziehen;
8. eine überschaubare Programmieraufgabe selbstständig und nachvollziehbar planen, implementieren und dokumentieren;
9. ihre Lösung, deren Aufbau und wesentliche Entwurfsentscheidungen adressatengerecht präsentieren sowie fachliches Feedback geben und aufnehmen.

## Lehrinhalte

- Wiederholung zentraler C#- und OOP-Konzepte
- Clean Code: aussagekräftige Benennung, überschaubare Methoden und klare Verantwortlichkeiten
- Refactoring als schrittweise Verbesserung bestehenden Codes
- Entwurfsmuster:
  - Erzeugungsmuster, insbesondere Factory Method; Singleton mit kritischer Einordnung
  - Strukturmuster, insbesondere Adapter und Decorator
  - Verhaltensmuster, insbesondere Strategy, Observer und State
- SOLID-Prinzipien und Programmierung gegen Abstraktionen
- Schichtenarchitektur und Trennung von Benutzeroberfläche, Fachlogik und Datenzugriff
- Dependency Injection und lose Kopplung
- Unit Testing, testbare Software und Grundlagen von Test-Driven Development
- Planung, Umsetzung und Präsentation einer verwaltungsnahen Programmieraufgabe

Die Lehrinhalte umfassen die Vertiefung zentraler C#- und OOP-Konzepte sowie Clean Code und Refactoring. Behandelt werden ausgewählte Entwurfsmuster – darunter Factory Method, Adapter, Decorator, Strategy, Observer und State – sowie die SOLID-Prinzipien. Die Studierenden lernen, Software in Schichten zu strukturieren, Abhängigkeiten über Dependency Injection zu entkoppeln und Fachlogik mit Unit Tests abzusichern. Ergänzend bearbeiten und präsentieren sie eine verwaltungsnahe Programmieraufgabe; je nach Projektkontext kommen Grundlagen zu asynchroner Programmierung mit `async`/`await` oder Entity Framework Core hinzu.

## Lehr- und Lernformen

Die Veranstaltung kombiniert kurze Wiederholungs- und Theoriephasen, anschauliche Beispiele beziehungsweise Live-Coding und praktische Übungen. Die Studierenden analysieren und verbessern bestehenden Code, implementieren ausgewählte Entwurfsmuster und üben das Testen zentraler Fachlogik. Ein wesentlicher Teil des Moduls ist die selbstständige Bearbeitung einer überschaubaren Projektaufgabe. In der Projektwerkstatt erhalten die Studierenden individuelle fachliche Rückmeldung zu Funktionalität, Architektur und Präsentation.

## Ablauf und thematische Schwerpunkte

| Woche | Schwerpunkt |
|---|---|
| 1 | Wiederholung der C#-OOP-Grundlagen, Clean Code und erstes Refactoring |
| 2 | Entwurfsmuster I: Erzeugungs- und Strukturmuster |
| 3 | Entwurfsmuster II: Verhaltensmuster |
| 4 | SOLID-Prinzipien, Schichtenarchitektur und Dependency Injection |
| 5 | Unit Testing, Testbarkeit, TDD-Grundlagen und Refactoring |
| 6 | Projektstart, Anforderungs- und Architekturentwurf; ergänzende Grundlagen zu `async`/`await` oder Entity Framework Core |
| 7 | Projektwerkstatt, Umsetzung und individuelles Coaching |
| 8 | Präsentation der Lösungen, fachlicher Austausch und Reflexion der Entwurfsentscheidungen |

## Prüfungsleistung und Bewertung

Die Prüfungsleistung besteht aus der Bearbeitung einer überschaubaren, aber nicht trivialen Programmieraufgabe und der Präsentation der Lösung. Die Aufgabe greift einen verwaltungsnahen Anwendungskontext auf. Bewertet werden sowohl die funktionierende Kernfunktionalität als auch die Qualität und Begründung der Umsetzung.

Die Kursunterlagen sehen folgende Bewertungsschwerpunkte vor:

| Kriterium | Gewichtung |
|---|---:|
| Softwarestruktur, Schichtentrennung und Schnittstellennutzung | 20 % |
| Sinnvoller Einsatz von Entwurfsmustern | 20 % |
| Aussagekräftige Unit Tests für die Fachlogik | 20 % |
| Funktionalität des Kernfeatures | 20 % |
| Projektdokumentation und Erläuterung der Entwurfsentscheidungen | 20 % |

In der Präsentation demonstrieren die Studierenden die wesentliche Funktionalität, erläutern den Aufbau der Software und begründen zentrale Architektur- und Musterentscheidungen. Die Präsentation ist mit einer Fragerunde und konstruktivem Peer-Feedback verbunden.

## Teilnahmevoraussetzungen und empfohlene Vorkenntnisse

Empfohlen werden grundlegende Programmierkenntnisse in C# sowie erste Erfahrungen mit prozeduraler und objektorientierter Programmierung. Dazu gehören insbesondere Klassen, Methoden, Vererbung und Interfaces. Weitere Programmiererfahrung, etwa mit Python oder JavaScript, kann hilfreich sein, wird aber nicht als Voraussetzung beschrieben.

## Literatur und Materialien

Die Lehrmaterialien, Beispiele, Übungen und Projektaufgaben werden im Rahmen der Veranstaltung bereitgestellt. Eine verbindliche Literaturliste ist in den vorliegenden Kursunterlagen nicht ausgewiesen.
