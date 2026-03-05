---
title: "Objektorientierte Programmierung (Vertiefung)"
subtitle: "Woche 6: Fortgeschrittene Konzepte & Projektstart"
author: "Dr. Peter Bernhardt"
date: "März 2026"
section-titles: true
---

# Teil 1: Asynchrone Programmierung

## Warum Asynchronität?
In der vernetzten Verwaltungswelt müssen wir oft auf externe Ressourcen warten:
- Abfrage im Melderegister (Web-API).
- Datenbankzugriffe (SQL Server).
- Senden von Bescheiden per E-Mail-Dienst.

**Das Problem:** Während wir warten, ist der Thread blockiert. In einer Desktop-App friert die Oberfläche ein, in einem Web-Server verbrauchen wir unnötig Ressourcen.

## Die Lösung: Async / Await
C# bietet mit dem `async`- und `await`-Keyword eine elegante Möglichkeit, nicht-blockierenden Code zu schreiben:
- `Task<T>`: Ein Versprechen, dass ein Ergebnis vom Typ T irgendwann in der Zukunft kommt.
- `await`: "Hör hier kurz auf, mach etwas anderes und komm zurück, wenn das Ergebnis da ist."

**Wichtig:** Nutzen Sie niemals `.Result` oder `.Wait()`, da dies den Thread doch wieder blockiert und zu Deadlocks führen kann.

## Paralleles Abfragen
Mit `Task.WhenAll()` können wir mehrere unabhängige Abfragen gleichzeitig starten:
- Meldeamt abfragen (Task 1).
- Finanzamt abfragen (Task 2).
- Beide laufen parallel -> Wir sparen Zeit!

Dies ist der Schlüssel für performante Fachverfahren, die Daten aus vielen Quellen zusammenführen müssen.

# Teil 2: Professionelles Error Handling

## Exceptions sind keine "Kontrollstrukturen"
Nutzen Sie `try-catch` nur für echte Ausnahmesituationen (z.B. Netzwerk weg, DB voll), nicht um normalen Programmfluss zu steuern (z.B. "Nutzer hat falsches Passwort eingegeben").

**Best Practices:**
- **Spezifische Catch-Blöcke:** Fangen Sie erst `SqlException`, dann `IOException` und ganz am Ende die allgemeine `Exception`.
- **Rethrow richtig machen:** Nutzen Sie `throw;` statt `throw ex;`, um den Stack-Trace (die Info, wo der Fehler genau herkommt) zu behalten.

## Zentrales Logging
In einem großen System müssen Admins wissen, was passiert ist.
Nutzen Sie ein Logging-Framework wie **Serilog**. Ein guter Log-Eintrag enthält:
- Den Zeitpunkt.
- Die Schwere (Information, Warning, Error, Critical).
- Den Kontext (Welcher User? Welcher Antrag?).
- Die Fehlermeldung und den Stack-Trace.

Zeigen Sie dem Nutzer niemals den technischen Stack-Trace ("Error at line 42 in DbConnector.cs"), sondern eine freundliche, allgemeine Nachricht. Die Details gehören in das Log-File.

# Teil 3: Vorbereitung der Abschlussprojekte

## Die Vision
Sie entwickeln ein eigenes **digitales Fachverfahren**. Das Ziel ist nicht die Größe, sondern die **architektonische Qualität**.

**Was wir sehen wollen:**
- **Schichten:** Trennung von UI, Logik und Daten.
- **Interfaces:** Bewusster Einsatz zur Entkopplung.
- **Patterns:** Mindestens zwei der besprochenen Design Patterns (z.B. Strategy, Factory, Observer).
- **Tests:** Mindestens 5-10 sinnvolle Unit Tests für die Kernlogik.

## Der Ablauf
- **Woche 6:** Themenfindung und Grobplanung (heute).
- **Woche 7:** Projektwerkstatt (Coaching & Programmieren).
- **Woche 8:** Abschlusspräsentation und Code-Review.

Dies ist Ihre Chance, alles Gelernte anzuwenden und ein Stück Software zu bauen, auf das Sie stolz sein können.

# Zusammenfassung & Projektstart

## Key Takeaways
- **Async/Await** verhindert blockierte Systeme.
- **Robustes Error Handling** macht Software professionell und wartbar.
- **Die Projektphase** beginnt jetzt!

## Nächste Schritte
Überlegen Sie sich bis zum Ende der heutigen Vorlesung ein Thema für Ihr Fachverfahren.
- Was ist der Kern-Workflow?
- Welche Gesetze/Regeln (Strategies) gibt es?
- Welche Zustände (States) durchläuft ein Antrag?

Viel Erfolg beim Planen und Coden!
