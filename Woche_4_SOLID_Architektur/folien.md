---
title: "Objektorientierte Programmierung (Vertiefung)"
subtitle: "Woche 4: SOLID & Architektur"
author: "Dr. Peter Bernhardt"
date: "März 2026"
section-titles: true
---

# Teil 1: Warum Architektur wichtig ist

## Software verrottet (Software Rot)
Wenn wir Code ohne Plan hinzufügen, passiert Folgendes:
- **Rigidität:** Jede Änderung führt zu massiven Problemen an anderen Stellen.
- **Fragilität:** Das System bricht bei kleinsten Änderungen an unerwarteten Orten zusammen.
- **Immobilität:** Teile der Software können nicht wiederverwendet werden, da sie zu fest mit dem Rest "verklebt" sind.

Gute Architektur ist wie Stadtplanung: Sie sorgt dafür, dass das System wachsen kann, ohne im Chaos zu versinken. Die SOLID-Prinzipien sind unsere Bauvorschriften.

# Teil 2: SOLID – Die 5 Prinzipien

## S: Single Responsibility Principle
"Eine Klasse sollte nur eine einzige Verantwortung haben."

In der Verwaltung bedeutet das:
- Die Klasse `Antrag` speichert nur Daten.
- Die Klasse `AntragsValidator` prüft die Daten.
- Die Klasse `AntragsRepository` schreibt sie in die Datenbank.

**Vorteil:** Wenn wir von SQL auf Oracle-Datenbanken wechseln, müssen wir nur das Repository ändern. Der Validator bleibt völlig unberührt.

## O: Open-Closed Principle
"Offen für Erweiterung, geschlossen für Änderung."

Wir fügen neue Funktionen hinzu, indem wir neuen Code schreiben, nicht indem wir alten, funktionierenden Code "aufbohren".

**Beispiel:** Ein System für verschiedene Steuersätze. Statt in einer riesigen Methode `if (bundesland == ...)` zu prüfen, nutzen wir Interfaces. Jedes Bundesland bekommt eine eigene Klasse. Kommt ein Bundesland hinzu, schreiben wir eine neue Klasse – wir ändern aber keine Zeile im Kernsystem.

## L: Liskov Substitution Principle
"Unterklassen müssen ihre Basisklassen ohne Überraschungen ersetzen können."

Das ist ein Vertrag: Wenn mein Programm ein `Fahrzeug` erwartet, muss es auch mit einem `ElektroAuto` funktionieren, ohne abzustürzen.
In der Verwaltung: Ein `SpezialAntrag` darf die Grundregeln eines `BasisAntrags` nicht verletzen oder Methoden "stilllegen" (z.B. durch `throw new NotImplementedException()`).

## I: Interface Segregation Principle
"Viele spezifische Interfaces sind besser als ein riesiges Universal-Interface."

Stellen Sie sich ein Interface vor, das `Drucken`, `Scannen` und `Faxen` verlangt. Ein einfacher Drucker müsste "Faxen" implementieren, obwohl er es nicht kann.

**Lösung:** Wir trennen es in `IDrucker`, `IScanner` und `IFax`. Jede Komponente implementiert nur das, was sie wirklich beherrscht. Dies reduziert Abhängigkeiten und macht den Code klarer.

## D: Dependency Inversion Principle
"Hänge nicht von konkreten Klassen ab, sondern von Abstraktionen (Interfaces)."

Dies ist der wichtigste Schritt zur losen Kopplung.
- **Schlecht:** Der `BuergerService` erstellt sich selbst eine `SqlDatenbank`.
- **Gut:** Der `BuergerService` sagt: "Ich brauche irgendetwas, das `IDatenbank` erfüllt. Wer mir das gibt, ist mir egal."

Dies erlaubt es uns, zum Testen eine "Fake-Datenbank" zu nutzen, ohne den Service-Code zu ändern.

# Teil 3: Dependency Injection (DI)

## Das Konzept der "Einspritzung"
Dependency Injection ist die praktische Umsetzung von Dependency Inversion.

**Wie es funktioniert:**
Anstatt `new MyDatabase()` im Konstruktor aufzurufen, übergeben wir die Datenbank als Parameter:
`public MyService(IDatabase db) { _db = db; }`

Ein **DI-Container** (in .NET eingebaut) verwaltet diese Abhängigkeiten automatisch. Er weiß, welches Objekt er wo "hineinstecken" muss. Das macht das System extrem flexibel und testbar.

# Teil 4: Schichtenarchitektur

## Trennung der Verantwortlichkeiten
Wir ordnen unsere Klassen in Schichten an:

1. **Presentation Layer (UI):** Nimmt Eingaben an, zeigt Daten an (z.B. Blazor, Console, WPF).
2. **Business Logic Layer (BLL):** Hier "wohnt" die Fachlogik. Gesetze, Berechnungen, Regeln.
3. **Data Access Layer (DAL):** Hier werden Daten geholt und gespeichert (Entity Framework, Filesystem).

**Die Goldene Regel:** Eine Schicht darf immer nur die Schicht direkt unter ihr aufrufen. Die UI darf niemals direkt mit der Datenbank sprechen!

# Zusammenfassung

## Key Takeaways
- **SOLID** verhindert, dass Software über die Zeit "verrottet".
- **Interfaces** sind die Klebestellen, die unser System flexibel halten.
- **Dependency Injection** entkoppelt unsere Komponenten.
- **Schichten** sorgen für eine klare Struktur und Ordnung im Projekt.

Architektur kostet am Anfang etwas Zeit, spart aber am Ende Monate an frustrierender Fehlersuche und mühsamen Anpassungen.
