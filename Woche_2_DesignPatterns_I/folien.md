---
title: "Objektorientierte Programmierung (Vertiefung)"
subtitle: "Woche 2: Design Patterns I – Struktur und Erzeugung"
author: "Dr. Peter Bernhardt"
date: "März 2026"
section-titles: true
---

# Teil 1: Einführung in Design Patterns

## Willkommen zu Woche 2
Nachdem wir uns in der ersten Woche mit Clean Code beschäftigt haben, gehen wir nun einen Schritt weiter: zum Software-Design. 
Wir schauen uns an, wie man architektonische Probleme löst, die immer wieder auftreten.

**Lernziele:**
- Das Konzept der Design Patterns verstehen.
- Erzeugungsmuster (Singleton, Factory Method) anwenden.
- Strukturmuster (Adapter, Decorator) in der Praxis nutzen.

## Was sind Design Patterns?
Design Patterns (Entwurfsmuster) sind bewährte Lösungsschablonen für wiederkehrende Probleme im Software-Design.
- Sie wurden maßgeblich durch die "Gang of Four" (GoF) im Jahr 1994 formalisiert.
- Es sind keine fertigen Code-Schnipsel, sondern abstrakte Konzepte.
- Sie bilden ein gemeinsames Vokabular für Entwickler.

## Warum Design Patterns in der Verwaltung?
In der Verwaltungsinformatik bauen wir Systeme für die Ewigkeit.
- **Gesetzesänderungen:** Algorithmen ändern sich, das System muss flexibel bleiben.
- **Legacy-Systeme:** Wir müssen oft alte mit neuen Systemen verbinden.
- Patterns helfen uns, robuste, erweiterbare und wartbare Architekturen zu schaffen.

## Drei Kategorien von Mustern
Die GoF unterteilt Muster in drei Hauptkategorien:
1. **Erzeugungsmuster (Creational):** Wie werden Objekte flexibel erzeugt?
2. **Strukturmuster (Structural):** Wie werden Klassen und Objekte zu größeren Strukturen zusammengefügt?
3. **Verhaltensmuster (Behavioral):** Wie kommunizieren Objekte effizient miteinander?

## Keine "Copy-Paste" Lösungen
Wichtig: Ein Design Pattern ist keine Bibliothek, die man einbindet.
- Sie müssen das Muster an Ihre spezifische Domäne (z.B. das Meldewesen) anpassen.
- Setzen Sie Muster nur ein, wenn Sie das entsprechende Problem haben (Vermeidung von Over-Engineering!).

# Teil 2: Das Singleton Pattern

## Einführung: Singleton
Das Singleton-Muster gehört zu den Erzeugungsmustern. 
Es stellt sicher, dass von einer Klasse **genau eine einzige Instanz** existiert und bietet einen globalen Zugriffspunkt darauf.

## Problemstellung
Oft gibt es Ressourcen in einem System, die nur einmal vorhanden sein dürfen.
Wenn Sie beispielsweise eine Verbindungskonfiguration laden, wollen Sie nicht, dass 10 verschiedene Module 10 verschiedene Kopien dieser Konfiguration im Speicher halten.

## Anwendung in der Verwaltung (Konfigurationsmanager)
Stellen Sie sich ein Fachverfahren vor. Es gibt zentrale Einstellungen:
- URL des zentralen Melderegisters
- Aktuelle Mehrwertsteuersätze
- Name der ausführenden Behörde

Ein `KonfigurationsManager` als Singleton stellt sicher, dass alle Module exakt denselben Stand dieser Daten nutzen.

## Singleton: Die Eigenschaften
Wie erreicht man, dass eine Klasse nur einmal instanziiert werden kann?
1. Ein **privater Konstruktor** verhindert, dass jemand von außen `new` aufruft.
2. Eine **statische Variable** hält die einzige Instanz.
3. Eine **öffentliche statische Eigenschaft** liefert diese Instanz zurück.

## Singleton: Code (Felder & Konstruktor)
```csharp
public sealed class KonfigurationsManager
{
    private static KonfigurationsManager _instance;
    private static readonly object _lock = new object();

    // Privater Konstruktor verhindert Instanziierung von außen
    private KonfigurationsManager() 
    {
        RegisterUrl = "https://zentralregister.behoerde.de/api";
        BehoerdenName = "Landesamt für Digitalisierung";
    }

    public string RegisterUrl { get; set; }
    public string BehoerdenName { get; set; }
    // ...
```

## Singleton: Code (Thread-Safe Instance)
```csharp
    public static KonfigurationsManager Instance
    {
        get
        {
            // Thread-safe Implementierung (Double-Check Locking)
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new KonfigurationsManager();
                    }
                }
            }
            return _instance;
        }
    }
}
```

## Kritik am Singleton
Das Singleton wird oft als "Anti-Pattern" bezeichnet. Warum?
- Es führt einen **globalen Zustand** (Global State) ein, der von überall geändert werden kann.
- Es verdeckt Abhängigkeiten: Eine Klasse, die das Singleton nutzt, zeigt dies nicht in ihrem Konstruktor.
- Es erschwert das **Unit Testing** (Mocking), da man die feste Instanz nur schwer durch einen Fake ersetzen kann.

## Wann man es (wirklich) nutzen sollte
Nutzen Sie Singleton nur, wenn die Instanzierung wirklich systemweit streng limitiert sein **muss** (z.B. Hardware-Treiber, Logging-Dienste in kleinen Systemen). 
In modernen Web-Anwendungen überlässt man diese Aufgabe oft dem "Dependency Injection Container" (dazu mehr in Woche 4).

# Teil 3: Factory Method Pattern

## Einführung: Factory Method
Das Factory Method Pattern definiert ein Interface zur Erstellung eines Objekts.
Die Entscheidung, **welche** konkrete Klasse instanziiert wird, wird jedoch an die Unterklassen delegiert.

## Problemstellung: Harte Verdrahtung
Wenn Sie überall im Code `new BewilligungsBescheid()` schreiben, koppeln Sie Ihre Logik eng an diese konkrete Klasse.
Was passiert, wenn sich der Prozess zur Erstellung ändert oder neue Bescheid-Arten hinzukommen? Der Code wird unflexibel.

## Anwendung: Erstellung von Bescheid-Typen
Ein Fachverfahren muss verschiedene Arten von Bescheiden erzeugen:
- Bewilligungsbescheid
- Ablehnungsbescheid
- Gebührenbescheid

Der Versandprozess (Drucken, Kuvertieren, Porto) ist bei allen gleich, nur der Inhalt variiert.

## Factory Method: Produkt-Klassen (Code)
Wir definieren ein abstraktes Produkt und konkrete Implementierungen:

```csharp
// Das Produkt-Interface / abstrakte Basisklasse
public abstract class Bescheid
{
    public abstract void GeneriereInhalt();
}

// Konkrete Produkte
public class BewilligungsBescheid : Bescheid
{
    public override void GeneriereInhalt() 
        => Console.WriteLine("Ihr Antrag wurde bewilligt.");
}

public class AblehnungsBescheid : Bescheid
{
    public override void GeneriereInhalt() 
        => Console.WriteLine("Ihr Antrag wurde leider abgelehnt.");
}
```

## Factory Method: Die abstrakte Factory (Code)
Die Kernlogik arbeitet nur mit der Abstraktion, nicht mit den konkreten Typen.

```csharp
// Die Factory (Erzeuger)
public abstract class BescheidErzeuger
{
    // Die eigentliche Factory Method
    public abstract Bescheid ErstelleBescheid();

    // Die Kernlogik (für alle gleich!)
    public void SendeBescheid()
    {
        var bescheid = ErstelleBescheid();
        bescheid.GeneriereInhalt();
        Console.WriteLine("Bescheid wird per Post/E-Mail versendet.");
    }
}
```

## Factory Method: Konkrete Erzeuger (Code)
Die Entscheidung der Instanziierung liegt in diesen kleinen Unterklassen.

```csharp
// Konkrete Erzeuger
public class BewilligungsErzeuger : BescheidErzeuger
{
    public override Bescheid ErstelleBescheid() 
        => new BewilligungsBescheid();
}

public class AblehnungsErzeuger : BescheidErzeuger
{
    public override Bescheid ErstelleBescheid() 
        => new AblehnungsBescheid();
}
```

## Vorteile der Factory Method
- **Single Responsibility:** Die Erzeugung der Objekte ist vom Rest des Codes isoliert.
- **Open-Closed Principle:** Sie können neue Bescheid-Arten (z.B. `AnhoerungsBescheid`) hinzufügen, ohne den bestehenden Versand-Code zu verändern.

# Teil 4: Adapter Pattern

## Einführung: Adapter Pattern
Der Adapter ist ein Strukturmuster. Er erlaubt es Objekten mit inkompatiblen Schnittstellen (Interfaces), zusammenzuarbeiten.
Er verhält sich wie ein Reiseadapter für Steckdosen: Er macht den fremden Stecker passend für die lokale Infrastruktur.

## Das Problem: Legacy-Systeme
In der Verwaltung können Sie Altsysteme nicht einfach abschalten. Sie müssen moderne Portale mit 20 Jahre alten Mainframes verbinden. 
Das moderne Portal erwartet saubere C#-Objekte, das alte System liefert aber z.B. kryptische, semikolon-getrennte Strings.

## Anwendung: Anbindung eines Zentralregisters
Wir haben ein Web-Portal, das eine `Person` benötigt. 
Das Legacy-Zentralregister hat keine moderne REST-API, sondern nur eine Funktion, die einen String wie `"Mustermann;Max;1980-01-01"` zurückgibt.

## Adapter: Das Ziel-Interface (Code)
So möchte unser modernes System arbeiten:

```csharp
public class Person {
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public DateTime Geburtsdatum { get; set; }
}

// Bestehendes, modernes System erwartet dieses Interface
public interface IPersonenQuelle
{
    Person GetPerson(string id);
}
```

## Adapter: Das alte System (Code)
Die inkompatible Klasse, die wir nicht ändern können oder dürfen:

```csharp
// Das alte Legacy-System (inkompatibel)
public class AltesRegisterSystem
{
    public string SuchePersonDatensatz(string personalId) 
    {
        // Simulation eines alten Datenbank-Aufrufs
        // Liefert z.B. "Mustermann;Max;1980-01-01"
        return "Mustermann;Max;1980-01-01";
    }
}
```

## Adapter: Die Implementierung (Code)
Der Adapter übersetzt zwischen den Welten:

```csharp
// Der Adapter schließt die Lücke
public class RegisterAdapter : IPersonenQuelle
{
    private readonly AltesRegisterSystem _legacySystem = new AltesRegisterSystem();

    public Person GetPerson(string id)
    {
        string rohDaten = _legacySystem.SuchePersonDatensatz(id);
        string[] teile = rohDaten.Split(';');
        
        return new Person {
            Nachname = teile[0],
            Vorname = teile[1],
            Geburtsdatum = DateTime.Parse(teile[2])
        };
    }
}
```

## Vorteile des Adapters
- Das moderne System bleibt völlig unabhängig von den Eigenheiten des Legacy-Systems.
- Wenn das alte Register irgendwann durch ein neues ersetzt wird, müssen wir nur einen neuen Adapter schreiben – die Fachlogik bleibt unberührt.

# Teil 5: Decorator Pattern

## Einführung: Decorator Pattern
Der Decorator ist ein Strukturmuster, das es erlaubt, einem Objekt dynamisch zusätzliches Verhalten hinzuzufügen.
Es ist eine flexible Alternative zur klassischen Vererbung.

## Das Problem der Klassenexplosion
Angenommen, Sie haben Basis-Anträge und möchten Zusatzoptionen anbieten.
Wenn Sie Vererbung nutzen, brauchen Sie für jede Kombination eine Klasse:
- `Reisepass`
- `ReisepassMitExpress`
- `ReisepassMit48Seiten`
- `ReisepassMitExpressUnd48Seiten`
Das führt zu Hunderten von Klassen!

## Anwendung: Antrags-Zusatzleistungen
In der Verwaltung können Bürger oft Optionen (Express-Zuschlag, Zustellung per Post) dazubuchen.
Statt die Vererbungshierarchie aufzublähen, "dekorieren" (umhüllen) wir das Basis-Objekt wie eine Zwiebel.

## Decorator: Basis und Komponente (Code)
```csharp
// Das Basis-Interface
public interface IAntrag
{
    double BerechneKosten();
    string GetBeschreibung();
}

// Die konkrete Basis-Komponente
public class ReisepassAntrag : IAntrag
{
    public double BerechneKosten() => 60.00;
    public string GetBeschreibung() => "Reisepass (Standard)";
}
```

## Decorator: Die Basis-Decorator-Klasse (Code)
Diese Klasse leitet die Aufrufe standardmäßig an das umhüllte Objekt (`_antrag`) weiter.

```csharp
// Basis-Decorator
public abstract class AntragDecorator : IAntrag
{
    protected IAntrag _antrag;
    
    public AntragDecorator(IAntrag antrag) => _antrag = antrag;
    
    public virtual double BerechneKosten() => _antrag.BerechneKosten();
    public virtual string GetBeschreibung() => _antrag.GetBeschreibung();
}
```

## Decorator: Der konkrete Decorator (Code)
Hier fügen wir das neue Verhalten hinzu:

```csharp
// Konkreter Decorator: Express Option
public class ExpressOption : AntragDecorator
{
    public ExpressOption(IAntrag antrag) : base(antrag) { }
    
    public override double BerechneKosten() 
        => base.BerechneKosten() + 32.00; // Zuschlag
        
    public override string GetBeschreibung() 
        => base.GetBeschreibung() + " + Express-Zuschlag";
}
```

## Nutzung des Decorators
So setzen wir die Zwiebel zusammen:
```csharp
// 1. Basis erstellen (60 EUR)
IAntrag meinAntrag = new ReisepassAntrag();

// 2. Mit Express dekorieren (+32 EUR)
meinAntrag = new ExpressOption(meinAntrag);

Console.WriteLine(meinAntrag.GetBeschreibung()); 
// Ausgabe: Reisepass (Standard) + Express-Zuschlag
Console.WriteLine(meinAntrag.BerechneKosten());  
// Ausgabe: 92.00
```
Sie können Optionen beliebig ineinander verschachteln!

# Teil 6: Zusammenfassung

## Wrap-up Woche 2
- **Singleton:** Wenn es garantiert nur ein Objekt geben darf (Vorsicht: Anti-Pattern Gefahr!).
- **Factory Method:** Delegation der Objekterzeugung an Unterklassen (für Flexibilität).
- **Adapter:** Der Übersetzer zwischen inkompatiblen Welten (Legacy-Integration).
- **Decorator:** Dynamisches Hinzufügen von Funktionen ohne Klassenexplosion.

## Ausblick auf nächste Woche
Diese Woche haben wir Objekte erzeugt (Creational) und strukturiert (Structural).
Nächste Woche betrachten wir die dritte Kategorie: **Verhaltensmuster (Behavioral Patterns)**, wie das Strategy- und Observer-Pattern.
