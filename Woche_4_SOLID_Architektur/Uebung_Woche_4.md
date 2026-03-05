
# Übungsaufgabe Woche 4: SOLID & Architektur

## Aufgabe 1: SOLID - Der Bescheid-Manager

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

Dieser Code verletzt das **Single Responsibility Principle (SRP)** und ist schwer zu testen, da er direkt auf das Dateisystem und die Konsole zugreift (Verletzung des **Dependency Inversion Principle (DIP)**).

1. **Refaktorisierung:** Trenne die Verantwortlichkeiten in eigene Klassen auf.
    - `BescheidGenerator`: Erzeugt nur den Text.
    - `FileStore`: Speichert den Text (implementiert ein Interface `IPersistence`).
    - `EmailNotifier`: Versendet die Benachrichtigung (implementiert ein Interface `INotifier`).
2. **Dependency Injection:** Nutze Konstruktor-Injektion im `BescheidService`, um die Abhängigkeiten von außen zu übergeben.
3. **Vorteil:** Überlege, wie du nun einen Test schreiben könntest, ohne tatsächlich eine Datei auf die Festplatte zu schreiben.

## Aufgabe 2: Drei-Schichten-Architektur - Antragsverwaltung

Implementieren Sie eine einfache Antragsverwaltung nach dem Prinzip der **Drei-Schichten-Architektur**:

- **Presentation Layer**: Nimmt Eingaben entgegen und gibt Ergebnisse aus.
- **Business Layer**: Enthält die Fachlogik.
- **Data Access Layer**: Kapselt den Datenzugriff.

### Szenario

Ein neuer Antrag wird mit `AntragId`, `BuergerName` und `Kategorie` erfasst.
Die Fachregeln lauten:

- Die Nummer des Antrages muss eine natürliche Zahl größer Null sein.
- Der Name eines Bürgers muss nicht leer sein.
- Die Kategorie eines Antrages muss entweder `Express` oder `Normal` sein.

### Anforderungen

1. Erstellen Sie ein Modell `Antrag` mit den Properties `AntragId`, `BuergerName`, `Kategorie`.
2. Definieren Sie im Data Access Layer ein Interface `IAntragRepository` mit:
    - `void Speichern(Antrag antrag)`
    - `Antrag? Laden(int antragId)`
3. Implementieren Sie ein einfaches `InMemoryAntragRepository` (z.B. mit `Dictionary<int, Antrag>`). Kann ein Antrag nicht gefunden werden, soll `null` zurueckgegeben werden.
4. Erstellen Sie im Business Layer eine Klasse `AntragService`, die das Repository per Konstruktor-Injektion erhaelt.
5. Implementieren Sie im `AntragService` die Methode `void AntragEinreichen(Antrag antrag)` mit folgender Regel:
    - Wenn eine Fachregel des Antrages verletzt wird, wird eine `ArgumentException` geworfen.
    - Sonst wird der Antrag ueber das Repository gespeichert.
6. Erstellen Sie im Presentation Layer eine Klasse `AntragController` mit Methoden:
    - `void NeuerAntrag(int id, string name, string kategorie)`
    - `void ZeigeAntrag(int id)`
7. Der Controller soll nur mit dem Service arbeiten (Konstruktor-Injektion) und keinen direkten Repository-Zugriff haben.
8. Testen Sie Ihre Implementierung.

### Reflexionsfragen

1. Welche Klasse gehört zu welcher Schicht der **Drei-Schichten-Architektur**?
2. Welche Schicht kennt welche andere Schicht?
3. Warum verbessert diese Struktur Testbarkeit und Wartbarkeit?
4. Wo wuerden Sie Logging oder Validierung zusaetzlich platzieren?
