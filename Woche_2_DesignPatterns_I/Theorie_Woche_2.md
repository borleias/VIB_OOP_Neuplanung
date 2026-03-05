# Woche 2: Design Patterns I – Struktur und Erzeugung in der Verwaltung

Herzlich willkommen zur zweiten Woche. Nachdem wir uns in der ersten Woche mit den Grundlagen von Clean Code und dem objektorientierten Mindset beschäftigt haben, gehen wir nun einen Schritt weiter. Wir schauen uns bewährte Lösungsmuster für wiederkehrende Probleme im Software-Design an: die **Design Patterns** (Entwurfsmuster).

In der Verwaltungsinformatik helfen uns Design Patterns dabei, Systeme zu bauen, die flexibel auf Gesetzesänderungen reagieren können und gleichzeitig robust gegenüber technologischen Altlasten (Legacy-Systemen) sind.

---

## 1. Was sind Design Patterns?

Design Patterns sind keine fertigen Code-Bausteine, die man kopiert. Sie sind eher **Schablone für Problemlösungen**. Man kann sie sich wie Standard-Verwaltungsverfahren vorstellen: Es gibt einen bewährten Ablauf, wie ein Antrag zu bearbeiten ist, egal ob es ein Bauantrag oder ein Parkausweis ist.

Wir unterscheiden drei Kategorien:
1.  **Erzeugungsmuster:** Wie erstelle ich Objekte flexibel? (z.B. Singleton, Factory Method)
2.  **Strukturmuster:** Wie setze ich Objekte zu größeren Strukturen zusammen? (z.B. Adapter, Decorator)
3.  **Verhaltensmuster:** Wie kommunizieren Objekte miteinander? (Thema der nächsten Woche)

---

## 2. Singleton: Der Konfigurationsmanager

Das **Singleton** stellt sicher, dass von einer Klasse nur genau **eine einzige Instanz** existiert und bietet einen globalen Zugriffspunkt darauf.

### Anwendung in der Verwaltung
Stellen Sie sich eine Anwendung vor, die auf die zentrale Konfiguration eines Fachverfahrens zugreifen muss (z.B. URL des Melderegisters, Datenbank-Verbindungspfad, aktuelle Mehrwertsteuersätze). Es wäre ineffizient und fehleranfällig, wenn jeder Teil des Programms eine eigene Kopie dieser Daten verwaltet.

### Implementierung in C#
```csharp
public sealed class KonfigurationsManager
{
    private static KonfigurationsManager _instance;
    private static readonly object _lock = new object();

    // Privater Konstruktor verhindert Instanziierung von außen
    private KonfigurationsManager() 
    {
        // Hier würden wir Daten aus einer Web.config oder App.settings laden
        RegisterUrl = "https://zentralregister.behoerde.de/api";
        BehoerdenName = "Landesamt für Digitalisierung";
    }

    public string RegisterUrl { get; set; }
    public string BehoerdenName { get; set; }

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

**Kritik am Singleton:** Es wird oft als "Anti-Pattern" bezeichnet, da es globale Zustände einführt und das Testen (Mocking) erschwert. Nutzen Sie es daher nur für echte, systemweite Ressourcen.

---

## 3. Factory Method: Erzeugung von Bescheid-Typen

Die **Factory Method** definiert ein Interface zur Erstellung eines Objekts, lässt aber die Unterklassen entscheiden, welche Klasse instanziiert wird.

### Anwendung in der Verwaltung
Ein Fachverfahren muss verschiedene Arten von **Bescheiden** erstellen (z.B. Bewilligungsbescheid, Ablehnungsbescheid, Gebührenbescheid). Der Kernprozess (die "Druck-Logik") ist oft gleich, aber der Inhalt und die Berechnung unterscheiden sich fundamental.

### Implementierung in C#
```csharp
// Das Produkt-Interface
public abstract class Bescheid
{
    public abstract void GeneriereInhalt();
}

// Konkrete Produkte
public class BewilligungsBescheid : Bescheid
{
    public override void GeneriereInhalt() => Console.WriteLine("Ihr Antrag wurde bewilligt.");
}

public class AblehnungsBescheid : Bescheid
{
    public override void GeneriereInhalt() => Console.WriteLine("Ihr Antrag wurde leider abgelehnt.");
}

// Die Factory (Erzeuger)
public abstract class BescheidErzeuger
{
    // Die eigentliche Factory Method
    public abstract Bescheid ErstelleBescheid();

    public void SendeBescheid()
    {
        var bescheid = ErstelleBescheid();
        bescheid.GeneriereInhalt();
        Console.WriteLine("Bescheid wird per Post/E-Mail versendet.");
    }
}

// Konkrete Erzeuger
public class BewilligungsErzeuger : BescheidErzeuger
{
    public override Bescheid ErstelleBescheid() => new BewilligungsBescheid();
}
```

**Vorteil:** Der Code, der den Bescheid versendet, muss nicht wissen, ob es ein Bewilligungs- oder Ablehnungsbescheid ist. Er arbeitet nur mit der Abstraktion `Bescheid`.

---

## 4. Adapter: Anbindung eines Legacy-Zentralregisters

Der **Adapter** erlaubt es Klassen zusammenzuarbeiten, die aufgrund inkompatibler Schnittstellen eigentlich nicht zusammenpassen würden.

### Anwendung in der Verwaltung
Ein modernes Web-Portal soll Daten aus einem 20 Jahre alten **Zentralregister** (Legacy-System) abrufen. Das alte System liefert Daten in einem kryptischen Format (z.B. semikolon-getrennte Strings), während unser neues System mit sauberen `Person`-Objekten arbeitet.

### Implementierung in C#
```csharp
// Bestehendes System erwartet dieses Interface
public interface IPersonenQuelle
{
    Person GetPerson(string id);
}

// Das alte Legacy-System (inkompatibel)
public class AltesRegisterSystem
{
    public string SuchePersonDatensatz(string personalId) 
    {
        // Liefert z.B. "Mustermann;Max;1980-01-01"
        return "Mustermann;Max;1980-01-01";
    }
}

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

---

## 5. Decorator: Zusatzleistungen für Anträge

Der **Decorator** ermöglicht es, einem Objekt dynamisch zusätzliches Verhalten hinzuzufügen, ohne die ursprüngliche Klasse zu ändern.

### Anwendung in der Verwaltung
Ein **Basis-Antrag** (z.B. Reisepass) kann verschiedene Zusatzleistungen (Optionen) haben: Express-Bearbeitung, Zustellung per Kurier oder ein zusätzliches 48-Seiten-Heft. Statt für jede Kombination eine eigene Klasse zu erstellen (`ReisepassExpressKurier48Seiten`), "dekorieren" wir das Basis-Objekt.

### Implementierung in C#
```csharp
public interface IAntrag
{
    double BerechneKosten();
    string GetBeschreibung();
}

public class ReisepassAntrag : IAntrag
{
    public double BerechneKosten() => 60.00;
    public string GetBeschreibung() => "Reisepass (Standard)";
}

// Basis-Decorator
public abstract class AntragDecorator : IAntrag
{
    protected IAntrag _antrag;
    public AntragDecorator(IAntrag antrag) => _antrag = antrag;
    public virtual double BerechneKosten() => _antrag.BerechneKosten();
    public virtual string GetBeschreibung() => _antrag.GetBeschreibung();
}

// Konkreter Decorator
public class ExpressOption : AntragDecorator
{
    public ExpressOption(IAntrag antrag) : base(antrag) { }
    public override double BerechneKosten() => base.BerechneKosten() + 32.00;
    public override string GetBeschreibung() => base.GetBeschreibung() + " + Express-Zuschlag";
}
```

**Nutzung:** `IAntrag meinAntrag = new ExpressOption(new ReisepassAntrag());`

---

## Zusammenfassung
*   **Singleton:** Wenn es nur EINEN geben darf (z.B. Konfiguration).
*   **Factory Method:** Wenn die Entscheidung über den Objekttyp beim Unterprogramm liegen soll (z.B. Bescheiderstellung).
*   **Adapter:** Wenn Alt und Neu zusammenarbeiten müssen (z.B. Legacy-Schnittstellen).
*   **Decorator:** Wenn Optionen flexibel kombinierbar sein sollen (z.B. Antrags-Zusatzleistungen).
