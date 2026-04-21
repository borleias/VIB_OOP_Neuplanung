# Übungsaufgabe Woche 6: Async, Error Handling und Projektstart

## Aufgabe 1: Asynchroner Bürgerdaten-Aggregator

### Szenario

Ein Fachverfahren benötigt Daten aus zwei externen Quellen:

- Melderegister
- Steuerdienst

Beide Aufrufe sind I/O-bound und sollen parallel laufen, damit die Gesamtwartezeit sinkt.

### Gegebene Verträge

```csharp
public record Meldedaten(string Name);
public record Steuerdaten(string Steuerklasse);
public record BuergerDaten(string Name, string Steuerklasse);

public interface IExterneApi
{
    Task<Meldedaten> HoleMeldedatenAsync(string steuerId);
    Task<Steuerdaten> HoleSteuerdatenAsync(string steuerId);
}

public sealed class ExterneServices : IExterneApi
{
    public async Task<Meldedaten> HoleMeldedatenAsync(string steuerId)
    {
        // Simuliert die Berechnung des Anfangsbuchstabens des Namens basierend auf der Steuer-ID;
        string initial = ((char)(steuerId[0] % 26 + 65)).ToString().ToUpper();
        return await Task.FromResult(new Meldedaten($"{initial}. Mustermann"));
    }

    public async Task<Steuerdaten> HoleSteuerdatenAsync(string steuerId)
    {
        // Simuliert die Berechnung der Steuerklasse basierend auf der Steuer-ID
        int sk = steuerId[0] % 5 + 1;
        return await Task.FromResult(new Steuerdaten($"Steuerklasse {sk}"));
    }
}
```

### Aufgabe

- Implementieren Sie eine Klasse `BuergerdatenService` mit der öffentlichen Methode:

  ```csharp
  Task<BuergerDaten> HoleVollstaendigeDatenAsync(string steuerId)
  ```

- Entwerfen Sie mindestens drei Testfälle für diese Methode, Validierung von `steuerId` ist `null`.

**Fachregeln:**

1. `steuerId` darf nicht leer oder nur whitespace sein, sonst `ArgumentException` (Fail Fast).
2. Starten Sie beide API-Aufrufe parallel.
3. Warten Sie mit `await Task.WhenAll(...)` auf beide Ergebnisse.
4. Geben Sie ein `BuergerDaten`-Objekt zurück.
5. Verwenden Sie **kein** `.Wait()` und **kein** `.Result` in einer synchronen Methode.

### Ziel

Sie üben das Prinzip **"Async all the way"**, den sinnvollen Einsatz von `Task.WhenAll` und Fail-Fast-Validierung.
