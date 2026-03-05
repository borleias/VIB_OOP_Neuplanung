---
title: "Objektorientierte Programmierung (Vertiefung)"
subtitle: "Woche 3: Design Patterns II – Verhaltensmuster"
author: "Dr. Peter Bernhardt"
date: "März 2026"
section-titles: true
---

# Teil 1: Verhaltensmuster in der Verwaltung

## Fokus: Interaktion zwischen Objekten
Nachdem wir in der letzten Woche gelernt haben, wie man Objekte erstellt (Creational Patterns) und strukturiert (Structural Patterns), betrachten wir heute die Dynamik.

Verhaltensmuster (Behavioral Patterns) konzentrieren sich auf:
- Den Informationsfluss zwischen Objekten.
- Die Verteilung von Verantwortlichkeiten.
- Die Flexibilität von Algorithmen zur Laufzeit.

In der Verwaltungsinformatik ist dies besonders wichtig, da Prozesse (Workflows) und Regeln (Gesetze) sich häufig ändern, die Grundstruktur der Anwendung aber stabil bleiben soll.

# Teil 2: Strategy Pattern (Strategie)

## Problem: Die "If-Else"-Hölle
Stellen Sie sich eine Methode zur Gebührenberechnung vor, die alle kommunalen Satzungen Deutschlands enthalten muss. Der Code wäre unlesbar und müsste bei jeder kleinen Gesetzesänderung in einer einzigen, riesigen Datei angepasst werden.

**Die Lösung:** Das Strategy Pattern.
Wir definieren ein Interface `IGebuehrenSatzung`. Jede konkrete Satzung (z.B. `SatzungBuxtehude`) kommt in eine eigene Klasse.

## Vorteile des Strategie-Musters
- **Open-Closed Prinzip:** Wir können neue Berechnungsregeln hinzufügen, ohne bestehenden Code zu ändern.
- **Laufzeit-Flexibilität:** Das Programm kann während der Ausführung entscheiden, welche Strategie genutzt wird (z.B. basierend auf der Postleitzahl des Bürgers).
- **Testbarkeit:** Jede Satzung kann isoliert und einfach getestet werden, ohne den Rest des Systems zu beeinflussen.

# Teil 3: Observer Pattern (Beobachter)

## Problem: Polling vermeiden
Wie erfährt ein Modul, dass in einem anderen Modul etwas passiert ist?
- **Schlecht:** Modul B fragt jede Sekunde bei Modul A nach: "Bist du fertig?" (Polling).
- **Gut:** Modul A informiert alle Interessenten aktiv, sobald ein Ereignis eintritt.

Dies ist das Prinzip von "Publish-Subscribe".

## Anwendung: Das Bürgerportal
Ein Antrag durchläuft das Fachverfahren. Sobald der Status auf "Bescheid versendet" springt:
1. Das **Portal** zeigt den neuen Status an.
2. Der **Benachrichtigungsdienst** sendet eine SMS/E-Mail.
3. Das **Monitoring-System** aktualisiert die Bearbeitungsstatistik.

Alle diese Dienste sind "Beobachter", die sich beim "Subjekt" (dem Antrag) angemeldet haben. Sie sind lose gekoppelt: Der Antrag muss nicht wissen, *wer* ihn beobachtet, sondern nur *dass* er seine Beobachter informieren muss.

# Teil 4: State Pattern (Zustand)

## Problem: Komplexe Workflows
Ein Bauantrag hat viele Zustände: `Eingereicht`, `InPruefung`, `Nachforderung`, `Genehmigt`, `Abgelehnt`. Bestimmte Aktionen sind nur in bestimmten Zuständen erlaubt.

Statt hunderter If-Abfragen wie `if (status == Status.InPruefung && aktion == Aktion.Genehmigen)`...

**Die Lösung:** Jeder Zustand ist eine eigene Klasse. Das Verhalten der Methode `Genehmigen()` ändert sich automatisch, je nachdem, welches Zustand-Objekt gerade aktiv ist.

## Zustandsübergänge steuern
Das State Pattern kapselt die Logik für Übergänge. Wenn im Zustand `Eingereicht` die Methode `Pruefen()` aufgerufen wird, wechselt das Objekt intern in den Zustand `InPruefung`.

Dies macht den Workflow-Code:
- **Übersichtlich:** Logik für "Nachforderung" steht nur in der Klasse `NachforderungStatus`.
- **Sicher:** Es können keine ungültigen Übergänge (z.B. von `Eingereicht` direkt zu `Genehmigt`) programmiert werden, da die Klassen dies verhindern.

# Zusammenfassung

## Key Takeaways
- **Strategy:** Tauscht Algorithmen aus (z.B. Satzungen).
- **Observer:** Reagiert auf Ereignisse (z.B. Statusänderungen).
- **State:** Verwaltet komplexe Prozessabläufe (z.B. Antrags-Workflows).

Diese Muster sind das Rückgrat moderner Business-Software. Sie ermöglichen es uns, Software zu bauen, die "atmen" kann – also flexibel genug ist, um mit den sich ständig ändernden Anforderungen der Verwaltung Schritt zu halten.
