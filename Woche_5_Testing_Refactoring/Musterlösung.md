# Übungsaufgabe Woche 5: Musterlösung

## Aufgabe 1: Parkgebühren-Tester

```csharp
using System;
using Xunit;

public enum Fahrzeugtyp
{
   Standard,
   Elektro,
   Anwohner
}

public class ParkgebuehrenRechner
{
   public decimal BerechneGebuehr(Fahrzeugtyp fahrzeugtyp, int stunden)
   {
      if (stunden < 0)
      {
         throw new ArgumentOutOfRangeException(nameof(stunden), "Stunden duerfen nicht negativ sein.");
      }

      decimal preisProStunde = fahrzeugtyp switch
      {
         Fahrzeugtyp.Standard => 2.00m,
         Fahrzeugtyp.Elektro => 1.00m,
         Fahrzeugtyp.Anwohner => 0.00m,
         _ => throw new ArgumentOutOfRangeException(nameof(fahrzeugtyp))
      };

      return preisProStunde * stunden;
   }
}

public class ParkgebuehrenRechnerTests
{
   [Theory]
   [InlineData(Fahrzeugtyp.Standard, 3, 6.00)]
   [InlineData(Fahrzeugtyp.Elektro, 2, 2.00)]
   [InlineData(Fahrzeugtyp.Anwohner, 10, 0.00)]
   [InlineData(Fahrzeugtyp.Elektro, 0, 0.00)]
   public void BerechneGebuehr_GibtErwartetenBetragZurueck_WennEingabenGueltigSind(
      Fahrzeugtyp fahrzeugtyp,
      int stunden,
      decimal erwartet)
   {
      // Arrange
      var sut = new ParkgebuehrenRechner();

      // Act
      decimal gebuehr = sut.BerechneGebuehr(fahrzeugtyp, stunden);

      // Assert
      Assert.Equal(erwartet, gebuehr);
   }

   [Fact]
   public void BerechneGebuehr_WirftArgumentOutOfRangeException_WennStundenNegativSind()
   {
      // Arrange
      var sut = new ParkgebuehrenRechner();

      // Act + Assert
      Assert.Throws<ArgumentOutOfRangeException>(() => sut.BerechneGebuehr(Fahrzeugtyp.Standard, -1));
   }
}
```

## Aufgabe 2: TDD - Warteschlangen-Manager fuer das Buergeramt

### Muster-Lösung

```csharp
using System;
using System.Collections.Generic;
using Xunit;

// Minimale Beispiel-Implementierung, damit die Tests nachvollziehbar sind.
public class WarteschlangenManager
{
   private readonly Queue<string> _wartende = new();

   public int TicketZiehen(string name)
   {
      if (string.IsNullOrWhiteSpace(name))
      {
         throw new ArgumentException("Name darf nicht leer sein.", nameof(name));
      }
      _wartende.Enqueue(name);
      return _wartende.Count;
   }

   public string NaechstenAufrufen()
   {
      if (_wartende.Count == 0)
      {
         return "niemand";
      }

      return _wartende.Dequeue();
   }

   public int AnzahlWartende()
   {
      return _wartende.Count;
   }
}
```

### Testklasse

```csharp
using System;
using Xunit;

public class WarteschlangenManagerTddTests
{
   [Fact]
   public void TicketZiehen_Gibt1Zurueck_WennErsterGueltigerNameHinzugefuegtWird()
   {
      var sut = new WarteschlangenManager();
      var ticketNumber = sut.TicketZiehen("Anna");
      Assert.Equal(1, ticketNumber);
   }

   [Fact]
   public void AnzahlWartende_Gibt1MehrZurueck_WennEinGueltigerNameHinzugefuegtWird()
   {
      var sut = new WarteschlangenManager();
      int vorher = sut.AnzahlWartende();
      sut.TicketZiehen("Anna");
      Assert.Equal(vorher + 1, sut.AnzahlWartende());
   }

   [Fact]
   public void NaechstenAufrufen_GibtNamenInFifoReihenfolgeZurueck_WennZweiNamenWarten()
   {
      var sut = new WarteschlangenManager();
      sut.TicketZiehen("Anna");
      sut.TicketZiehen("Ben");
      Assert.Equal("Anna", sut.NaechstenAufrufen());
      Assert.Equal("Ben", sut.NaechstenAufrufen());
   }

   [Fact]
   public void NaechstenAufrufen_GibtNiemandZurueck_WennWarteschlangeLeerIst()
   {
      var sut = new WarteschlangenManager();
      Assert.Equal("niemand", sut.NaechstenAufrufen());
   }

   [Theory]
   [InlineData("")]
   [InlineData("   ")]
   public void TicketZiehen_WirftArgumentException_WennNameLeerOderWhitespaceIst(string name)
   {
      var sut = new WarteschlangenManager();
      Assert.Throws<ArgumentException>(() => sut.TicketZiehen(name));
   }
}
```
