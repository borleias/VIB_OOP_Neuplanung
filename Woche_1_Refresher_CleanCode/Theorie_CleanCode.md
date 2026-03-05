# Theorie: Clean Code Prinzipien

Dieses Dokument dient als Begleitmaterial zum Vortrag.

## 1. Warum Clean Code?
- Code wird öfter gelesen als geschrieben.
- "The only way to go fast, is to go well." (Robert C. Martin)
- Reduktion technischer Schulden.

## 2. Aussagekräftige Namen
- **Variablen:** `int d;` (Schlecht) vs. `int elapsedDaysSinceLastUpdate;` (Gut).
- **Methoden:** `void DoIt();` (Schlecht) vs. `void SaveApplicationToDatabase();` (Gut).
- **Klassen:** Nomen verwenden, keine Verben. `AccountProcessor` statt `ProcessAccount`.

## 3. Funktionen (Methoden)
- **Kurz halten:** Eine Methode sollte idealerweise nicht mehr als 5-10 Zeilen haben.
- **Eines tun (Do One Thing):** Eine Methode sollte eine einzige, klar definierte Aufgabe erfüllen.
- **Keine Seiteneffekte:** Eine Methode `CheckPassword` sollte nicht gleichzeitig das Passwort in der Datenbank ändern.

## 4. Kommentare
- **Guter Code erklärt sich selbst:** Kommentare sollten nur dort eingesetzt werden, wo die Absicht des Codes nicht offensichtlich ist (z.B. komplexe Algorithmen oder externe API-Besonderheiten).
- **Vermeiden:** Offensichtliche Kommentare wie `i++; // Erhöhe i um 1`.
- **Vermeiden:** Auskommentierter Code. (Nutze Git!)

## 5. Formatierung
- Konsistente Einrückung.
- Zusammengehörige Codeabschnitte optisch gruppieren.
- Befolgung der C# Coding Conventions (Microsoft).
