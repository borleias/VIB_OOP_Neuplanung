# Woche 3: Design Patterns II – Verhalten und Interaktion

In dieser Woche vertiefen wir unser Wissen über Entwurfsmuster und konzentrieren uns auf die **Verhaltensmuster**. Während es in der letzten Woche um die Erstellung und Struktur von Objekten ging, beschäftigen wir uns nun damit, wie Objekte intelligent miteinander kommunizieren und wie wir Algorithmen flexibel austauschbar machen.

Besonders in der öffentlichen Verwaltung, wo sich Gesetze (Algorithmen) und Prozesszustände (Workflows) ständig ändern können, sind diese Muster von unschätzbarem Wert.

---

## 1. Strategy: Flexible Gebührenberechnung

Das **Strategy-Pattern** (Strategie-Muster) definiert eine Familie von Algorithmen, kapselt jeden einzelnen und macht sie austauschbar. Das Muster ermöglicht es, den Algorithmus unabhängig von den Clients zu variieren, die ihn nutzen.

### Anwendung in der Verwaltung
Stellen Sie sich ein System zur Berechnung von Parkgebühren oder Hundesteuern vor. Die Berechnungslogik (Satzung) unterscheidet sich von Kommune zu Kommune oder ändert sich durch einen Gemeinderatsbeschluss zum Jahreswechsel. Statt einer riesigen `if-else`-Struktur nutzen wir Strategien.

### Implementierung in C#
```csharp
// Das Interface für alle Strategien
public interface IGebuehrenStrategie
{
    double Berechne(double basisBetrag);
}

// Konkrete Strategien (Satzungen)
public class SatzungBerlin : IGebuehrenStrategie
{
    public double Berechne(double basis) => basis * 1.05; // 5% Verwaltungskostenzuschlag
}

public class SatzungMuenchen : IGebuehrenStrategie
{
    public double Berechne(double basis) => basis + 15.00; // Pauschalgebühr
}

// Der Kontext
public class GebuehrenRechner
{
    private IGebuehrenStrategie _strategie;

    // Strategie kann zur Laufzeit gesetzt oder getauscht werden
    public void SetStrategie(IGebuehrenStrategie strategie) => _strategie = strategie;

    public double BerechneEndbetrag(double basis)
    {
        if (_strategie == null) throw new InvalidOperationException("Keine Satzung gewählt!");
        return _strategie.Berechne(basis);
    }
}
```

**Vorteil:** Wenn eine neue Stadt hinzukommt, müssen wir den `GebuehrenRechner` nicht anpassen. Wir schreiben einfach eine neue Klasse, die `IGebuehrenStrategie` implementiert (Open-Closed-Prinzip).

---

## 2. Observer: Statusänderungen im Bürgerportal

Das **Observer-Pattern** (Beobachter-Muster) definiert eine 1:n-Abhängigkeit zwischen Objekten, sodass die Änderung des Zustands eines Objekts dazu führt, dass alle abhängigen Objekte benachrichtigt werden.

### Anwendung in der Verwaltung
Ein Bürger hat einen Antrag in einem Portal eingereicht. Sobald ein Sachbearbeiter den Status auf "Genehmigt" setzt, sollen mehrere Dinge passieren:
1.  Das Bürger-Dashboard im Web muss sich aktualisieren.
2.  Eine automatische E-Mail-Benachrichtigung muss raus.
3.  Die Archivierungs-Schnittstelle muss informiert werden.

### Implementierung in C#
```csharp
// Das Interface für die Beobachter
public interface IStatusBeobachter
{
    void Aktualisieren(string neuerStatus);
}

// Das Subjekt (der Antrag), das beobachtet wird
public class Antrag
{
    private readonly List<IStatusBeobachter> _beobachter = new List<IStatusBeobachter>();
    public string Status { get; private set; }

    public void RegistriereBeobachter(IStatusBeobachter b) => _beobachter.Add(b);

    public void SetzeStatus(string neuerStatus)
    {
        Status = neuerStatus;
        BenachrichtigeAlle();
    }

    private void BenachrichtigeAlle()
    {
        foreach (var b in _beobachter) b.Aktualisieren(Status);
    }
}

// Konkreter Beobachter: E-Mail Dienst
public class EmailService : IStatusBeobachter
{
    public void Aktualisieren(string neuerStatus) 
        => Console.WriteLine($"Sende E-Mail: Ihr Antrag ist nun '{neuerStatus}'.");
}
```

---

## 3. State: Der Workflow eines Bauantrags

Das **State-Pattern** (Zustands-Muster) ermöglicht es einem Objekt, sein Verhalten zu ändern, wenn sein interner Zustand sich ändert. Das Objekt sieht dann so aus, als ob es seine Klasse gewechselt hätte.

### Anwendung in der Verwaltung
Ein Bauantrag durchläuft verschiedene Phasen: `Eingegangen`, `InPrüfung`, `Vollständig`, `Genehmigt`. Bestimmte Aktionen (z.B. "Unterlagen nachfordern") sind nur im Zustand `InPrüfung` erlaubt, aber nicht im Zustand `Genehmigt`.

### Implementierung in C#
```csharp
public abstract class BauantragStatus
{
    public abstract void Bearbeiten(Bauantrag kontext);
    public abstract void Genehmigen(Bauantrag kontext);
}

// Konkreter Zustand: Eingereicht
public class EingereichtStatus : BauantragStatus
{
    public override void Bearbeiten(Bauantrag kontext) 
    {
        Console.WriteLine("Prüfung wird gestartet...");
        kontext.SetzeZustand(new InPruefungStatus());
    }
    public override void Genehmigen(Bauantrag kontext) 
        => Console.WriteLine("Fehler: Antrag muss erst geprüft werden!");
}

// Der Kontext
public class Bauantrag
{
    private BauantragStatus _aktuellerStatus;

    public Bauantrag() => _aktuellerStatus = new EingereichtStatus();

    public void SetzeZustand(BauantragStatus status) => _aktuellerStatus = status;

    public void Bearbeiten() => _aktuellerStatus.Bearbeiten(this);
    public void Genehmigen() => _aktuellerStatus.Genehmigen(this);
}
```

**Vorteil:** Wir vermeiden gigantische `switch-case`-Blöcke in einer einzigen Klasse. Die Logik für jeden Zustand ist sauber in einer eigenen Klasse gekapselt.

---

## Zusammenfassung
*   **Strategy:** Kapselt Algorithmen (z.B. verschiedene Steuergesetze) und macht sie zur Laufzeit austauschbar.
*   **Observer:** Informiert mehrere Interessenten automatisch über Zustandsänderungen (z.B. Event-Messaging).
*   **State:** Kapselt zustandsabhängiges Verhalten und steuert komplexe Workflows (z.B. Antrags-Lebenszyklus).

Diese Muster fördern die **Wartbarkeit** enorm: Änderungen an einem Workflow-Schritt (State) oder einer Berechnungslogik (Strategy) betreffen nur eine einzige, isolierte Klasse.
