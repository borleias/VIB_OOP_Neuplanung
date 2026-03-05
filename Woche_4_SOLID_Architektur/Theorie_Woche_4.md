# Woche 4: SOLID-Prinzipien & Architektur

Die SOLID-Prinzipien sind fünf Grundregeln des objektorientierten Designs, die helfen, Software wartbar, erweiterbar und testbar zu machen.

## 1. Die SOLID-Prinzipien

### S - Single Responsibility Principle (SRP)
Eine Klasse sollte nur eine einzige Verantwortlichkeit haben.
*   *Beispiel:* Ein `BescheidGenerator` sollte nur den Text erzeugen, aber nicht entscheiden, wie er gespeichert oder per E-Mail versendet wird.

### O - Open/Closed Principle (OCP)
Klassen sollten offen für Erweiterungen, aber geschlossen für Modifikationen sein.
*   *Beispiel:* Wenn wir eine neue Versandart (z.B. Fax) hinzufügen, sollten wir keinen bestehenden Code im `BescheidService` ändern müssen, sondern eine neue Klasse hinzufügen können.

### L - Liskov Substitution Principle (LSP)
Unterklassen müssen sich so verhalten wie ihre Basisklassen. Ein Programm darf nicht abstürzen, wenn eine Basisklasse durch eine Unterklasse ersetzt wird.

### I - Interface Segregation Principle (ISP)
Clients sollten nicht gezwungen werden, von Interfaces abzuhängen, die sie nicht benutzen. Lieber viele kleine, spezifische Interfaces als ein großes "Monster-Interface".

### D - Dependency Inversion Principle (DIP)
Abhängigkeiten sollten gegen Abstraktionen (Interfaces) gerichtet sein, nicht gegen konkrete Klassen.

---

## 2. Dependency Injection (DI)

Dependency Injection ist die praktische Umsetzung des Dependency Inversion Principle. Statt dass eine Klasse ihre Abhängigkeiten selbst mit `new` erzeugt, bekommt sie diese von außen "injiziert" (meist über den Konstruktor).

**Warum DI?**
1.  **Leichtere Testbarkeit:** Man kann echte Services durch "Mocks" (Test-Double) ersetzen.
2.  **Lose Kopplung:** Die Klassen wissen weniger voneinander.

**Beispiel Bescheid-Generator:**

```csharp
public interface INachrichtenService 
{
    void Sende(string nachricht);
}

public class BescheidService
{
    private readonly INachrichtenService _nachrichtenService;

    // Konstruktor-Injektion
    public BescheidService(INachrichtenService nachrichtenService)
    {
        _nachrichtenService = nachrichtenService;
    }

    public void ErstelleUndSendeBescheid(string inhalt)
    {
        // Logik zur Erstellung...
        _nachrichtenService.Sende(inhalt);
    }
}
```
In modernen Anwendungen (wie ASP.NET Core) übernimmt ein "DI-Container" automatisch die Erzeugung dieser Objekte.
