# Übungsaufgabe Woche 2: Simple Factory, Adapter, Singleton & Decorator

## Aufgabe 1: Simple Factory - Dokumentenservice

Sie entwickeln ein System zur automatisierten Erstellung von Dokumenten. Es gibt drei Arten von Dokumenten: `Antrag`, `Bescheid` und `Rechnung`.

**Anforderungen:**

1. Erstellen Sie eine abstrakte Basisklasse `Dokument` mit einer Methode `void Drucken()`.
2. Implementieren Sie die drei konkreten Dokumentklassen. Beim Drucken soll jeweils ein passender Text auf dem Bildschirm ausgegeben werden.
3. Definieren Sie ein Enum `DokumentTyp` mit den Werten `Antrag`, `Bescheid`, `Rechnung`.
4. Erstellen Sie eine Fabrikklasse `DokumentFactory`, die je nach `DokumentTyp` das passende Dokumenten-Objekt erzeugt.
5. Testen Sie Ihre Implementierung in einem kurzen Hauptprogramm.

**Hinweis:**  
In dieser Aufgabe nutzen Sie bewusst eine **Simple Factory** (Auswahl über Parameter), nicht das GoF-Factory-Method-Pattern.

---

## Aufgabe 2: Adapter - Legacy Register

Ihre Anwendung benötigt Bürgerdaten von einem alten Mainframe-System (`LegacySystem`). Das Legacy-System liefert über die Methode `string SuchePersonDatensatz(string personalId)` Daten als ein einzelnes `string`-Feld mit Pipe-Symbolen (`|`) als Trenner (z.B. `123|Max|Müller|Berlin`).

**Anforderungen:**

1. Erstellen Sie eine Klasse `Buerger` mit mindestens den Feldern:
   - `Id`
   - `Vorname`
   - `Nachname`
   - `Wohnort`
2. Die Schnittstelle `IBuergerService` erwartet eine Methode `Buerger GetBuerger(int id)`.
3. Implementieren Sie einen Adapter, der das `LegacySystem` nutzt, um die Daten abzurufen, zu parsen und als `Buerger`-Objekt zurückzugeben.
4. Ergänzen Sie eine kurze Fehlerbehandlung: Falls das Datenformat ungültig ist, soll eine sinnvolle Exception geworfen werden.
5. Testen Sie Ihre Implementierung.

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

**Reflexionsfrage:**  
Welche Nachteile hat der Singleton-Ansatz in Bezug auf Testbarkeit und globalen Zustand?

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

---

## Aufgabe 4: Decorator - Weihnachtsbaum

Sie möchten einen Weihnachtsbaum schrittweise mit Schmuck dekorieren.
Jede Dekoration fügt eine Beschreibung und zusätzliche Kosten hinzu.

**Anforderungen:**

1. Erstellen Sie ein Interface `IWeihnachtsbaum` mit den Methoden:
   - `string GetBeschreibung()`
   - `double GetKosten()`
2. Implementieren Sie die Basisklasse `EinfacherWeihnachtsbaum`:
   - Beschreibung: `"Weihnachtsbaum"`
   - Kosten: `20.00`
3. Implementieren Sie eine abstrakte Decorator-Basisklasse `BaumDecorator`, die `IWeihnachtsbaum` implementiert und das zu dekorierende Objekt kapselt.
4. Implementieren Sie mindestens drei konkrete Decorators:
   - `Lichterkette`: +`"+ Lichterkette"`, +`15.00`
   - `Weihnachtskugeln`: +`"+ Weihnachtskugeln"`, +`8.00`
   - `Lametta`: +`"+ Lametta"`, +`5.00`
5. Testen Sie Ihre Implementierung: Erstellen Sie einen Baum und dekorieren Sie ihn mit allen drei Optionen. Geben Sie Beschreibung und Gesamtkosten aus.

**Reflexionsfrage:**  
Was wäre der Unterschied gewesen, hätten Sie statt des Decorator-Patterns einfach Vererbung genutzt?
