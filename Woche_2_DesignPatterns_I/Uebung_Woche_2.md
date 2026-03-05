# Übungsaufgabe Woche 2: Factory, Adapter & State

## Aufgabe 1: Factory Method - Dokumentenservice

Sie entwickeln ein System zur automatisierten Erstellung von Dokumenten. Es gibt drei Arten von Dokumenten: `Antrag`, `Bescheid` und `Rechnung`.

**Anforderungen:**

1. Erstellen Sie eine abstrakte Basisklasse `Dokument` mit einer Methode `void Drucken()`.
2. Implementieren Sie die drei konkreten Dokumentklassen. Beim Drucken soll nur ein passender Text auf dem Bildschirm ausgegeben werden.
3. Erstellen Sie eine Fabrikklasse (*Factory*), die je nach Parameter (z.B. Enum `DokumentTyp`) das passende Dokumenten-Objekt erzeugt.
4. Testen Sie Ihre Implementierung.

## Aufgabe 2: Adapter - Legacy Register

Ihre Anwendung benötigt Bürgerdaten von einem alten Mainframe-System (`LegacySystem`). Das Legacy-System liefert über die Methode `string SuchePersonDatensatz(string personalId)` Daten als ein einziges langes `string`-Feld mit Pipe-Symbolen (`|`) als Trenner (z.B. `123|Max|Müller|Berlin`).

**Anforderungen:**

1. Die Schnittstelle `IBuergerService` erwartet eine Methode `GetBuergerName(int id)`.
2. Implementieren Sie einen Adapter, der das `LegacySystem` nutzt, um die Daten abzurufen, zu parsen und im gewünschten Format zurückzugeben.
3. Testen Sie Ihre Implementierung.

**Tipps:**

- Nutzen Sie `string.Split('|')` zum Parsen der Legacy-Daten.
- Trennen Sie die Adapter-Logik sauber von der restlichen Business-Logik.

## Aufgabe 3: Singleton - Zentraler Konfigurationsmanager

In Ihrer Anwendung müssen verschiedene Module auf gemeinsame Konfigurationsdaten zugreifen (z.B. Datenbankverbindung, Log-Level, API-Endpoints). Es soll genau eine zentrale Instanz des `KonfigurationsManager` existieren, die von allen Teilen der Anwendung genutzt wird.

**Anforderungen:**

1. Implementieren Sie eine Klasse `KonfigurationsManager` als Singleton.
2. Die Klasse soll eine Methode `string GetEinstellung(string schluessel)` bereitstellen.
3. Die Klasse soll eine Methode `void SetzeEinstellung(string schluessel, string wert)` bereitstellen.
4. Intern können Sie ein `Dictionary<string, string>` zur Speicherung verwenden.
5. Stellen Sie sicher, dass der Konstruktor privat ist und nur über eine statische Methode oder Property auf die Instanz zugegriffen werden kann.
6. Testen Sie Ihre Implementierung, indem Sie das nachfolgende Hauptprogramm ausführen.

**Beispiel-Hauptprogramm:**

```csharp
public class Program
{
    static void Main(string[] args)
    {
        // Erste Instanz abrufen und Einstellungen setzen
        var config1 = KonfigurationsManager.Instance;
        config1.SetzeEinstellung("DatabaseConnection", "Server=localhost;Database=VerwaltungsDB");
        config1.SetzeEinstellung("LogLevel", "DEBUG");
        config1.SetzeEinstellung("ApiEndpoint", "https://api.verwaltung.de");
        
        Console.WriteLine("Konfiguration gesetzt über config1");
        
        // Zweite "Instanz" abrufen - sollte dieselbe sein
        var config2 = KonfigurationsManager.Instance;
        
        // Einstellungen über config2 auslesen
        Console.WriteLine($"DatabaseConnection: {config2.GetEinstellung("DatabaseConnection")}");
        Console.WriteLine($"LogLevel: {config2.GetEinstellung("LogLevel")}");
        Console.WriteLine($"ApiEndpoint: {config2.GetEinstellung("ApiEndpoint")}");
        
        // Prüfen, ob config1 und config2 dieselbe Instanz sind
        if (ReferenceEquals(config1, config2))
        {
            Console.WriteLine("\nErfolg: config1 und config2 sind dieselbe Instanz!");
        }
        else
        {
            Console.WriteLine("\nFehler: config1 und config2 sind unterschiedliche Instanzen!");
        }
        
        // Simulation: Verschiedene Module greifen auf die Konfiguration zu
        Console.WriteLine("\n--- Modul A greift zu ---");
        ModulA.AusfuehrenA();
        
        Console.WriteLine("\n--- Modul B greift zu ---");
        ModulB.AusfuehrenB();
    }
}

// Beispiel-Module, die auf die zentrale Konfiguration zugreifen
public class ModulA
{
    public static void AusfuehrenA()
    {
        var config = KonfigurationsManager.Instance;
        Console.WriteLine($"Modul A verwendet: {config.GetEinstellung("DatabaseConnection")}");
    }
}

public class ModulB
{
    public static void AusfuehrenB()
    {
        var config = KonfigurationsManager.Instance;
        Console.WriteLine($"Modul B verwendet LogLevel: {config.GetEinstellung("LogLevel")}");
    }
}
```
