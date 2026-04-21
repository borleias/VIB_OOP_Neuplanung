# Übungsaufgabe Woche 6: Async, Error Handling und Projektstart

## Aufgabe 1: Asynchroner Bürgerdaten-Aggregator

```csharp
namespace externalapi;

public class Program
{
    public async static Task Main(string[] args)
    {
        var api = new ExterneServices();
        var sut = new BuergerdatenService(api);

        string steuerId = "DE123";

        var ergebnis = await sut.HoleVollstaendigeDatenAsync(steuerId);
        Console.WriteLine($"Ergebnis für SteuerId {steuerId} -> Name: {ergebnis.Name}, Steuerklasse: {ergebnis.Steuerklasse}"); 
    }
}

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

public class BuergerdatenService
{
    private readonly IExterneApi _api;

    public BuergerdatenService(IExterneApi api)
    {
        _api = api;
    }

    public async Task<BuergerDaten> HoleVollstaendigeDatenAsync(string steuerId)
    {
        if (string.IsNullOrWhiteSpace(steuerId))
        {
            throw new ArgumentException("SteuerId darf nicht leer sein.", nameof(steuerId));
        }

        var meldedatenTask = _api.HoleMeldedatenAsync(steuerId);
        var steuerdatenTask = _api.HoleSteuerdatenAsync(steuerId);

        await Task.WhenAll(meldedatenTask, steuerdatenTask);

        var meldedaten = await meldedatenTask;
        var steuerdaten = await steuerdatenTask;

        return new BuergerDaten(meldedaten.Name, steuerdaten.Steuerklasse);
    }
}
```

**Beispielhafte Testfälle**

1. Ruft beide API-Methoden bei gültiger ID genau einmal auf.
2. Gibt kombiniertes Ergebnis korrekt zurück.
3. Wirft `ArgumentException` bei `null`, `""` und `"   "`.
