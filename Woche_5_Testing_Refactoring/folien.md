---
title: "Objektorientierte Programmierung (Vertiefung)"
subtitle: "Woche 5: Testing & Refactoring"
author: "Dr. Peter Bernhardt"
date: "März 2026"
section-titles: true
---

# Teil 1: Einführung in automatisiertes Testen

## Vertrauen ist gut, Tests sind besser
Bisher haben wir Code geschrieben und gehofft, dass er funktioniert. In professionellen Projekten reicht das nicht aus.
**Warum testen wir?**
- **Sicherheit bei Änderungen:** Wir können Code ändern, ohne Angst zu haben, Bestehendes zu zerstören (Regressions-Tests).
- **Lebendige Dokumentation:** Tests zeigen genau, wie eine Methode verwendet werden soll.
- **Besseres Design:** Code, der schwer zu testen ist, ist meistens auch schlecht designt (zu enge Kopplung).

Automatisierte Tests sind die Lebensversicherung für Ihre Software.

## Das Test-Pyramide
Ein gesundes Projekt hat eine klare Struktur bei den Tests:
1. **Unit Tests (Basis):** Tausende, extrem schnelle Tests für einzelne Methoden.
2. **Integration Tests (Mitte):** Hunderte Tests, die das Zusammenspiel mehrerer Komponenten prüfen (z.B. mit echter Datenbank).
3. **End-to-End Tests (Spitze):** Wenige, langsame Tests, die den kompletten Prozess aus Sicht des Nutzers simulieren.

Heute fokussieren wir uns auf die wichtigste Basis: die **Unit Tests**.

# Teil 2: Unit Testing mit xUnit

## Das AAA-Prinzip
Jeder gute Test folgt diesem Schema:
- **Arrange (Vorbereiten):** Erstellen Sie das Objekt und die Daten (z.B. `new Validator()`).
- **Act (Ausführen):** Rufen Sie die Methode auf, die Sie testen wollen (z.B. `validator.Check(id)`).
- **Assert (Prüfen):** Überprüfen Sie das Ergebnis mit einem `Assert`-Befehl (z.B. `Assert.True(result)`).

Wenn ein Assert fehlschlägt, ist der Test "rot". Wenn alle passen, ist er "grün".

## Facts und Theories
In xUnit gibt es zwei Arten von Tests:
- **[Fact]:** Ein einfacher Test ohne Parameter, der immer das gleiche Ergebnis liefern sollte.
- **[Theory]:** Ein Test, der mit verschiedenen Datensätzen (Inputs) ausgeführt wird.
  - Nutzen Sie `[InlineData(value)]`, um verschiedene Testfälle schnell abzudecken (z.B. verschiedene ungültige Steuer-IDs).

# Teil 3: Isolation und Mocking

## Das Problem der Abhängigkeiten
Wenn wir einen `BuergerService` testen wollen, der Daten aus einer Datenbank liest, testen wir eigentlich zwei Dinge gleichzeitig: Den Service UND die Datenbank. Das ist kein Unit Test mehr!

**Die Lösung: Mocks (Attrappen).**
Wir nutzen Interfaces (`IDatabase`), um die echte Datenbank durch ein "Fake-Objekt" zu ersetzen.
Ein Mock liefert uns vordefinierte Daten zurück, ohne dass wir eine echte Datenbank installieren oder konfigurieren müssen.

## Warum Interfaces so wichtig sind
Ohne Interfaces ist Mocking (fast) unmöglich. Interfaces sind die Voraussetzung für:
- **Testbarkeit:** Wir können jede Komponente isoliert prüfen.
- **Parallelität:** Wir können den Service programmieren, während der Kollege noch an der Datenbank arbeitet.

# Teil 4: TDD – Test-Driven Development

## Erst der Test, dann der Code
TDD dreht den klassischen Prozess um. Es ist ein Zyklus in drei Schritten:

1. **RED:** Schreiben Sie einen Test für eine neue Funktion. Er wird fehlschlagen (da der Code fehlt).
2. **GREEN:** Schreiben Sie den simpelsten Code, der den Test besteht. "Schmutziger" Code ist hier erlaubt.
3. **REFACTOR:** Verbessern Sie den Code (Struktur, Namen, Performance). Die grünen Tests garantieren Ihnen, dass die Funktion erhalten bleibt.

**Vorteil:** Sie schreiben niemals Code, den Sie nicht wirklich brauchen, und haben automatisch eine Testabdeckung von 100%.

# Teil 5: Refactoring

## Den Code aufräumen
Refactoring ist das Ändern der inneren Struktur von Code, ohne dessen äußeres Verhalten zu ändern.

**Wann ist Refactoring nötig? (Code Smells)**
- Duplizierter Code (DRY-Prinzip verletzt).
- Zu lange Methoden oder Klassen.
- Unklare Namen.

Refactoring ohne Tests ist gefährlich! Erst wenn Sie ein Sicherheitsnetz aus grünen Tests haben, dürfen Sie mutig aufräumen.

# Zusammenfassung

## Key Takeaways
- **Unit Tests** sind die Basis für Qualität und Wartbarkeit.
- **xUnit** ist das Werkzeug unserer Wahl in C#.
- **Interfaces und Mocks** erlauben uns, Code isoliert zu testen.
- **TDD** führt zu sauberem, fokussiertem Design.
- **Refactoring** hält das System über Jahre hinweg gesund.

Guter Code ist Code, der sich traut, geändert zu werden – dank automatisierter Tests.
