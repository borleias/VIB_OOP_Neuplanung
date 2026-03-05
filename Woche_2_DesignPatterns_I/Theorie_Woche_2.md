# Woche 2: Design Patterns I - Erzeugung & Struktur

In dieser Woche beschäftigen wir uns mit den ersten Entwurfsmustern (Design Patterns). Muster sind bewährte Lösungen für wiederkehrende Probleme in der Softwareentwicklung.

## 1. Factory Method (Fabrikmethode)

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

## 2. Adapter Pattern

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
