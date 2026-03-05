# Woche 2: Design Patterns I - Erzeugung & Struktur

In dieser Woche beschäftigen wir uns mit den ersten Entwurfsmustern (Design Patterns). Muster sind bewährte Lösungen für wiederkehrende Probleme in der Softwareentwicklung.

## 1. Was sind Design Patterns?

- Von der "Gang of Four" (GoF) formuliert.
- Erprobte Lösungen für Standardprobleme in der OOP.
- Kategorien: **Erzeugungsmuster** (Creational), **Strukturmuster** (Structural), **Verhaltensmuster** (Behavioral).

## 2. Singleton (Erzeugung)

- **Problem:** Es darf nur eine Instanz einer Klasse im gesamten System geben (z.B. Konfigurationsmanager).
- **Lösung:** Privater Konstruktor und statische `Instance`-Eigenschaft.
- **Kritik:** Erschwert Unit Testing und kann globalen State einführen (Anti-Pattern-Gefahr!).

## 3. Factory Method (Erzeugung)

- **Problem:** Die genaue Klasse eines zu erzeugenden Objekts soll nicht im Client-Code festgeschrieben sein.
- **Lösung:** Auslagerung der Erzeugung in eine spezialisierte Methode oder Klasse.
- **Vorteil:** Leichte Erweiterbarkeit durch neue Typen, ohne den Client zu ändern (Open-Closed Principle).

Das Problem: Eine Klasse weiß im Voraus nicht, welche konkreten Unterklassen sie erzeugen muss.
Die Lösung: Wir definieren eine Schnittstelle zur Erzeugung eines Objekts, lassen aber die Unterklassen entscheiden, welche Klasse instanziiert wird.

**Anwendung in der Verwaltung:** Erzeugung verschiedener Dokumenttypen (Bescheid, Anschreiben, Bestätigung).

```csharp
public abstract class Dokument
{
    public abstract void Drucken();
}

public class Bescheid : Dokument
{
    public override void Drucken() => Console.WriteLine("Drucke einen rechtssicheren Bescheid.");
}

public abstract class DokumentCreator
{
    public abstract Dokument ErstelleDokument();
}

public class BescheidCreator : DokumentCreator
{
    public override Dokument ErstelleDokument() => new Bescheid();
}
```

## 4. Adapter (Struktur)

- **Problem:** Zwei Klassen haben inkompatible Schnittstellen, müssen aber zusammenarbeiten (z.B. Einbindung einer Drittanbieter-Bibliothek).
- **Lösung:** Eine Zwischenklasse (der Adapter), welche die Aufrufe "übersetzt".
- **Prinzip:** "Wrapper" um das inkompatible Objekt.

Das Problem: Zwei Klassen haben inkompatible Schnittstellen, müssen aber zusammenarbeiten.
Die Lösung: Ein Adapter fungiert als "Übersetzer" zwischen der neuen Anwendung und einer alten (Legacy) Komponente.

**Anwendung in der Verwaltung:** Anbindung eines alten Einwohnermelderegisters (Legacy System), das Daten in einem veralteten Format liefert.

```csharp
// Das alte System (Legacy)
public class AltesRegister
{
    public string HoleDatenAlt() => "Name: Müller; Vorname: Max; ID: 123";
}

// Die Schnittstelle, die wir erwarten
public interface IModerneBuergerQuelle
{
    string GetBuergerInfo();
}

// Der Adapter
public class RegisterAdapter : IModerneBuergerQuelle
{
    private readonly AltesRegister _altesRegister;
    public RegisterAdapter(AltesRegister altesRegister) => _altesRegister = altesRegister;

    public string GetBuergerInfo()
    {
        string alt = _altesRegister.HoleDatenAlt();
        // Transformation der Daten...
        return alt.Replace(";", ",");
    }
}
```

## 5. Decorator (Struktur)

- **Problem:** Verhalten eines Objekts soll zur Laufzeit erweitert werden, ohne die Vererbungshierarchie aufzublähen.
- **Lösung:** Das Objekt wird in ein anderes Objekt "eingepackt", das dieselbe Schnittstelle hat.
