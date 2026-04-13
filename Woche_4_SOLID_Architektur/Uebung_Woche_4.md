
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

1. **Refaktorisierung:** Trennen Sie die Verantwortlichkeiten in eigene Klassen auf.
    - `BescheidGenerator`: Erzeugt nur den Text (implementiert ein Interface `IBescheidGenerator`).
    - `FileStore`: Speichert den Text (implementiert ein Interface `IPersistence`).
    - `EmailNotifier`: Versendet die Benachrichtigung (implementiert ein Interface `INotifier`).

2. **Dependency Injection:** Nutzen Sie Konstruktor-Injektion im `BescheidService`, um die Abhängigkeiten von außen zu übergeben. Sie können auf einen DI-Container verzichten und die Abhängigkeiten manuell im Hauptprogramm erstellen.
3. **Testen:** Überlege, wie Sie nun einen Test schreiben könnten, ohne tatsächlich eine Datei auf die Festplatte zu schreiben.

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
    - `Antrag Laden(int antragId)`
3. Implementieren Sie das Interface `IAntragRepository` mit einem einfachen `InMemoryAntragRepository` (z.B. mit `Dictionary<int, Antrag>` zum Speichern der Anträge). Ist bereits ein Antrag mit der gleichen Id vorhanden, soll dieser überschrieben werden. Kann ein Antrag nicht gefunden werden, soll `null` zurueckgegeben werden.
4. Erstellen Sie im Business Layer eine Klasse `AntragService`, die das Repository per Konstruktor-Injektion erhaelt.
5. Implementieren Sie im `AntragService` die Methode `void AntragEinreichen(Antrag antrag)` mit folgender Regel:
    - Wenn eine Fachregel des Antrages verletzt wird, wird eine `ArgumentException` geworfen.
    - Sonst wird der Antrag ueber das Repository gespeichert.
6. Erstellen Sie im Presentation Layer eine Klasse `AntragController` mit Methoden:
    - `void NeuerAntrag(int id, string name, string kategorie)`  
     Diese Methode erstellt einen neuen Antrag und reicht ihn ein. Über das ERgebnis (Erfolg oder Fehler) wird eine Nachricht auf der Konsole ausgegeben.
    - `void ZeigeAntrag(int id)`
        Diese Methode lädt einen Antrag und zeigt die Details auf der Konsole an. Wenn der Antrag nicht gefunden wird, soll eine entsprechende Nachricht ausgegeben werden.
7. Der Controller soll nur mit dem Service arbeiten (Konstruktor-Injektion) und keinen direkten Repository-Zugriff haben.
8. Testen Sie Ihre Implementierung mit einer Konsolenanwendung. Sie können auf Benutzerinteraktion verzichten und die Methoden direkt im `Main`-Programm aufrufen.

### Reflexionsfragen

1. Welche Klasse gehört zu welcher Schicht der **Drei-Schichten-Architektur**?
2. Welche Schicht kennt welche andere Schicht?
3. Warum verbessert diese Struktur Testbarkeit und Wartbarkeit?
4. Wo wuerden Sie Logging oder Validierung zusaetzlich platzieren?
