# Übungsaufgabe Woche 3: Strategy, Observer & State

## Aufgabe 1: Strategy - Variable Gebührenberechnung

Implementieren Sie ein System zur Berechnung von Parkgebühren in einer Stadtverwaltung.

**Anforderungen:**

1. Erstellen Sie ein Interface `IParkGebuehrStrategie` mit einer Methode `double BerechneParkGebuehr(int minuten)`.
2. Implementieren Sie drei Strategien:
    - `StandardParken`: 0.05€ pro Minute.
    - `AnwohnerParken`: 0.01€ pro Minute.
    - `ElektroAutoParken`: Die ersten 60 Minuten sind kostenlos, danach 0.03€ pro Minute.
3. Erstellen Sie eine Klasse `ParkscheinAutomat`, die eine Strategie entgegennimmt und die Gebühr berechnet.
4. Testen Sie Ihre Implementierung.

## Aufgabe 2: Observer - Antragsstatus-Benachrichtigung

Implementieren Sie ein System, das Bürger automatisch benachrichtigt, wenn sich der Status ihres Antrags ändert.

**Anforderungen:**

1. Ein `Antrag` hat eine `ID`, einen `BuergerNamen` und einen `Status`.
2. Erstellen Sie ein Interface `IBenachrichtigungsService` mit einer Methode `void Informiere(Antrag antrag)`.
3. Implementieren Sie zwei Benachrichtigungs-Services:
    - `EmailService`: Gibt eine Konsolenmeldung aus ("E-Mail gesendet: Ihr Antrag #ID ist jetzt auf Status STATUS").
    - `SmsService`: Gibt eine Konsolenmeldung aus ("SMS gesendet: Statusänderung bei Antrag #ID").
4. Der `Antrag` sollte eine Liste von `IBenachrichtigungsService`-Objekten verwalten und alle benachrichtigen, sobald sich der Status ändert.
5. Testen Sie Ihre Implementierung.

## Aufgabe 3 (Optional): Events

Nutzen Sie C# `events` und `delegates` anstelle des "klassischen" Observer Patterns für Aufgabe 2.

## Aufgabe 4: State - Dokumentenfreigabe-Workflow

Implementieren Sie einen Workflow für die Freigabe von Dokumenten in einer Behörde. Ein Dokument durchläuft verschiedene Zustände: `Entwurf`, `InPruefung`, `Freigegeben` und `Abgelehnt`. Die verfügbaren Aktionen hängen vom aktuellen Zustand ab.

**Anforderungen:**

1. Erstellen Sie ein Interface `IDokumentZustand` mit folgenden Methoden:
    - `void Bearbeiten(DokumentWorkflow kontext)`
    - `void Einreichen(DokumentWorkflow kontext)`
    - `void Genehmigen(DokumentWorkflow kontext)`
    - `void Ablehnen(DokumentWorkflow kontext)`
    - `string ZustandName { get; }`

2. Implementieren Sie vier konkrete Zustandsklassen, die das Interface implementieren:
    - `EntwurfZustand`: Erlaubt Bearbeiten und Einreichen. Beim Bearbeiten bleibt der Zustand unverändert. Beim Einreichen wechselt der Zustand zu `InPruefung`.
    - `InPruefungZustand`: Erlaubt Genehmigen und Ablehnen. Genehmigen wechselt zu `Freigegeben`, Ablehnen zu `Abgelehnt`.
    - `FreigegebenZustand`: Endzustand - keine Aktionen mehr möglich.
    - `AbgelehntZustand`: Erlaubt Bearbeiten (zurück zu `Entwurf`).

3. Erstellen Sie eine Klasse `DokumentWorkflow` mit:
    - Property `IDokumentZustand AktuellerZustand`
    - Methode `void SetzeZustand(IDokumentZustand zustand)`
    - Methoden `void Bearbeiten()`, `void Einreichen()`, `void Genehmigen()`, `void Ablehnen()`, die an den aktuellen Zustand delegieren.
    - Eine Property `string Titel` für den Dokumenttitel.

4. Wenn eine Aktion im aktuellen Zustand nicht erlaubt ist, geben Sie eine entsprechende Meldung aus (z.B. "Aktion nicht möglich im Zustand X").

5. Testen Sie Ihre Implementierung mit dem unten stehenden Beispielprogramm.

**Tipps:**

- Jeder Zustand sollte nur die für ihn relevanten Aktionen implementieren und bei unerlaubten Aktionen eine Meldung ausgeben.
- Der Kontext (`DokumentWorkflow`) hält eine Referenz auf den aktuellen Zustand und delegiert alle Aufrufe an diesen.
- Nutzen Sie die `SetzeZustand`-Methode, um Zustandsübergänge durchzuführen.

**Beispiel-Nutzung:**

```csharp
public class Program
{
    public static void Main()
    {
        var dokument = new DokumentWorkflow("Bauantrag Musterstrasse 123");
        Console.WriteLine($"Startzustand: {dokument.AktuellerZustand.ZustandName}");

        dokument.Genehmigen(); // Nicht erlaubt im Entwurf
        dokument.Bearbeiten(); // Erlaubt
        dokument.Einreichen(); // Entwurf -> InPruefung
        dokument.Ablehnen();   // InPruefung -> Abgelehnt
        dokument.Einreichen(); // Nicht erlaubt in Abgelehnt
        dokument.Bearbeiten(); // Abgelehnt -> Entwurf
        dokument.Einreichen(); // Entwurf -> InPruefung
        dokument.Genehmigen(); // InPruefung -> Freigegeben
        dokument.Bearbeiten(); // Nicht erlaubt in Freigegeben
    }
}
```
