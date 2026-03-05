# Übung Woche 4: SOLID & Architektur

## Szenario: Der Bescheid-Manager
In einer Behörde gibt es ein Programm, das Bescheide für Bürger erstellt. Aktuell sieht die Klasse `BescheidService` so aus:

```csharp
public class BescheidService {
    public void ErstelleBescheid(string buergerName, double betrag) {
        // 1. Text generieren
        string text = $"Sehr geehrter Herr/Frau {buergerName}, Sie müssen {betrag} EUR zahlen.";
        
        // 2. In Datei speichern
        File.WriteAllText("bescheid.txt", text);
        
        // 3. E-Mail senden
        Console.WriteLine("Sende E-Mail an buerger@beispiel.de mit Inhalt: " + text);
    }
}
```

## Aufgabe
Dieser Code verletzt das **Single Responsibility Principle (SRP)** und ist schwer zu testen, da er direkt auf das Dateisystem und die Konsole zugreift (Verletzung des **Dependency Inversion Principle (DIP)**).

1.  **Refaktorisierung:** Trenne die Verantwortlichkeiten in eigene Klassen auf.
    -   `BescheidGenerator`: Erzeugt nur den Text.
    -   `FileStore`: Speichert den Text (implementiert ein Interface `IPersistence`).
    -   `EmailNotifier`: Versendet die Benachrichtigung (implementiert ein Interface `INotifier`).
2.  **Dependency Injection:** Nutze Konstruktor-Injektion im `BescheidService`, um die Abhängigkeiten von außen zu übergeben.
3.  **Vorteil:** Überlege, wie du nun einen Unit-Test schreiben könntest, ohne tatsächlich eine Datei auf die Festplatte zu schreiben.
