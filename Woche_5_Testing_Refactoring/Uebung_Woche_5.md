# Übung Woche 5: Parkgebühren-Tester

## Szenario
Du hast eine Klasse `ParkgebuehrenRechner`, die basierend auf dem Fahrzeugtyp und der Dauer die Gebühren berechnet:
-   Standard: 2,00 EUR / Stunde
-   Elektro-Fahrzeuge: 1,00 EUR / Stunde (Umweltbonus)
-   Anwohner: Kostenlos (0,00 EUR)

## Aufgabe
Erstelle ein xUnit-Testprojekt und schreibe Tests für die folgenden Fälle:

1.  **Standard:** Ein normales Fahrzeug parkt 3 Stunden -> Erwartet: 6,00 EUR.
2.  **Elektro:** Ein E-Auto parkt 2 Stunden -> Erwartet: 2,00 EUR.
3.  **Anwohner:** Ein Anwohner parkt 10 Stunden -> Erwartet: 0,00 EUR.
4.  **Edge-Case:** Ein E-Auto parkt 0 Stunden -> Erwartet: 0,00 EUR.
5.  **Negative Werte:** Was passiert bei -1 Stunden? Überlege dir ein sinnvolles Verhalten (z.B. Exception werfen oder 0 zurückgeben).

## Ziel
Du sollst das **AAA-Pattern** anwenden und sowohl `[Fact]` als auch `[Theory]` für die Tests nutzen.
