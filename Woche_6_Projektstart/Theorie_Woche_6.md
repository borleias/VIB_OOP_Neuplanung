# Theorie Woche 6: Asynchrone Programmierung & Projektausblick

## 1. Warum Asynchronität?
In der Verwaltungsinformatik müssen oft externe Dienste (z.B. ein zentrales Personenregister) abgefragt werden. Diese Abfragen dauern Zeit. Wenn das Programm blockiert, "friert" die Oberfläche ein.

## 2. async / await in C#
Mit `async` und `await` können wir Aufgaben im Hintergrund laufen lassen, ohne den Haupt-Thread zu blockieren.

### Beispiel: Bürgerdaten abrufen
```csharp
public async Task<string> HoleBuergerDatenAsync(int id) {
    HttpClient client = new HttpClient();
    // Die Anwendung wartet hier, OHNE zu blockieren
    string daten = await client.GetStringAsync($"https://api.behoerde.de/buerger/{id}");
    return daten;
}
```

## 3. Der Weg zum Abschlussprojekt
Ab heute beginnt die Arbeit an deinem eigenen Projekt. Ein gutes Projekt zeichnet sich aus durch:
-   **Klare Struktur:** Nutzung von Klassen und Interfaces.
-   **Anwendung von Mustern:** Mindestens 2 Design Patterns (z.B. Factory, Strategy).
-   **Testbarkeit:** Ein Kernbereich der Logik sollte mit Unit Tests abgedeckt sein.
-   **Saubere Architektur:** Trennung von Datenhaltung und Logik.
