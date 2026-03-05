# Übungsaufgabe Woche 1: Refactoring des Bibliothek-Systems

## Ausgangslage
Das vorliegende Projekt `DirtyLibrarySystem.cs` ist funktional korrekt, aber der Code ist schwer lesbar und schwer erweiterbar. Alle Daten und die gesamte Logik befinden sich direkt in der `Main`-Methode.

## Ihre Aufgabe
Refactoren Sie den Code in eine saubere, objektorientierte Struktur. Berücksichtigen Sie dabei die Clean Code Prinzipien aus der Vorlesung.

### Anforderungen:
1.  **Modellierung von Objekten:** Erstellen Sie separate Klassen für `Buch` und `Benutzer`.
2.  **Kapselung:** Nutzen Sie Properties statt öffentlicher Felder oder Arrays von Strings.
3.  **Typsicherheit:** Ersetzen Sie magische Werte (wie "1" für aktiv) durch Enums.
4.  **Logik-Kapselung:** Erstellen Sie eine Klasse `Bibliothek`, die das Ausleihen und Verwalten von Büchern übernimmt.
5.  **Clean Names:** Verwenden Sie sprechende Variablen- und Methodennamen (z.B. `LeiheBuchAus`, `IstVerfuegbar`).

## Projektstruktur (Ziel)
Ihr Projekt sollte nach dem Refactoring etwa so aussehen:
- `Buch.cs` (oder in einer Datei)
- `Benutzer.cs`
- `Bibliothek.cs`
- `Program.cs` (minimaler Code in der Main)

Viel Erfolg!
