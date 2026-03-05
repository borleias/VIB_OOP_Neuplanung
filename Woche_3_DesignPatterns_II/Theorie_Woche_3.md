# Woche 3: Design Patterns II - Verhaltensmuster

In dieser Woche konzentrieren wir uns auf Verhaltensmuster, die die Interaktion zwischen Objekten steuern.

## 1. Strategy Pattern (Strategie-Muster)

Das Problem: Wir haben verschiedene Algorithmen für dieselbe Aufgabe (z.B. Gebührenberechnung), die zur Laufzeit ausgetauscht werden sollen.
Die Lösung: Wir definieren eine Schnittstelle für alle unterstützten Algorithmen und kapseln jeden Algorithmus in einer eigenen Klasse.

**Anwendung in der Verwaltung:** Verschiedene Logiken für die Berechnung von Verwaltungsgebühren (z.B. Standard, Sozialtarif, Befreiung).

```csharp
public interface IGebuehrenStrategie
{
    decimal BerechneGebuehr(decimal basisBetrag);
}

public class StandardGebuehr : IGebuehrenStrategie
{
    public decimal BerechneGebuehr(decimal basisBetrag) => basisBetrag;
}

public class SozialTarif : IGebuehrenStrategie
{
    public decimal BerechneGebuehr(decimal basisBetrag) => basisBetrag * 0.5m; // 50% Rabatt
}

public class GebuehrenRechner
{
    private IGebuehrenStrategie _strategie;
    public void SetStrategie(IGebuehrenStrategie strategie) => _strategie = strategie;
    public decimal Berechne(decimal basis) => _strategie.BerechneGebuehr(basis);
}
```

## 2. Observer Pattern (Beobachter-Muster)

Das Problem: Wenn sich der Zustand eines Objekts (z.B. ein Antrag) ändert, müssen andere Objekte (z.B. E-Mail-Service, Statistik-Service) darüber informiert werden, ohne dass das ursprüngliche Objekt diese im Detail kennen muss.
Die Lösung: Ein "Subjekt" führt eine Liste seiner "Beobachter" und benachrichtigt diese bei Zustandsänderungen.

**Anwendung in der Verwaltung:** Benachrichtigung bei Statusänderung eines Antrags (von "Eingegangen" auf "In Bearbeitung").

```csharp
public interface IBeobachter
{
    void Aktualisiere(string status);
}

public class Antrag
{
    private List<IBeobachter> _beobachter = new List<IBeobachter>();
    public string Status { get; private set; }

    public void Registriere(IBeobachter beobachter) => _beobachter.Add(beobachter);

    public void AendereStatus(string neuerStatus)
    {
        Status = neuerStatus;
        foreach (var b in _beobachter) b.Aktualisiere(neuerStatus);
    }
}
```
In C# wird das Observer Pattern oft eleganter über `events` und `delegates` gelöst, das Prinzip bleibt aber dasselbe.
