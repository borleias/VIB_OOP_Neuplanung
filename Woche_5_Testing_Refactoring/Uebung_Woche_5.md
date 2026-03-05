# Übungsaufgabe Woche 5: Unit Testing & TDD

## Aufgabe 1: Parkgebühren-Tester

### Szenario

Du hast eine Klasse `ParkgebuehrenRechner`, die basierend auf dem Fahrzeugtyp und der Dauer die Gebühren berechnet:

- Standard: 2,00 EUR / Stunde
- Elektro-Fahrzeuge: 1,00 EUR / Stunde (Umweltbonus)
- Anwohner: Kostenlos (0,00 EUR)

### Aufgabe

Erstelle ein xUnit-Testprojekt und schreibe Tests für die folgenden Fälle:

1. **Standard:** Ein normales Fahrzeug parkt 3 Stunden -> Erwartet: 6,00 EUR.

2. **Elektro:** Ein E-Auto parkt 2 Stunden -> Erwartet: 2,00 EUR.
3. **Anwohner:** Ein Anwohner parkt 10 Stunden -> Erwartet: 0,00 EUR.
4. **Edge-Case:** Ein E-Auto parkt 0 Stunden -> Erwartet: 0,00 EUR.
5. **Negative Werte:** Was passiert bei -1 Stunden? Überlege dir ein sinnvolles Verhalten (z.B. Exception werfen oder 0 zurückgeben).

### Ziel

Du sollst das **AAA-Pattern** (Arrange, Act, Assert) anwenden und sowohl `[Fact]` als auch `[Theory]` für die Tests nutzen.

## Aufgabe 2: TDD - Warteschlangen-Manager fuer das Buergeramt

### Szenario

Im Buergeramt wird eine einfache Warteschlange benoetigt. Implementiere eine Klasse `WarteschlangenManager` streng nach **TDD**.

Die Klasse soll das folgende Interface `IWarteschlangenManager` implementieren.

```csharp
public interface IWarteschlangenManager
{
   public void TicketZiehen(string name);
   public string NaechstenAufrufen();
   public int AnzahlWartende();
}
```

### Fachregeln

1. Beim Ziehen eines Tickets darf `name` nicht leer oder nur aus Leerzeichen bestehen.
2. `NaechstenAufrufen()` gibt den Namen der Person zurueck, die am laengsten wartet (FIFO).
3. Wird `NaechstenAufrufen()` bei leerer Warteschlange aufgerufen, soll eine `InvalidOperationException` geworfen werden.
4. `AnzahlWartende()` liefert die aktuelle Anzahl wartender Personen.

### Aufgabe

Arbeite in kleinen Schritten nach **Red -> Green -> Refactor**:

1. Schreibe zuerst einen fehlschlagenden Test fuer einen einzigen Happy-Path.
2. Implementiere nur so viel Code, dass der Test gruen wird.
3. Erweitere um den naechsten Testfall (z.B. FIFO-Verhalten).
4. Erweitere danach um Edge-Cases und Fehlerfaelle.
5. Refaktoriere den Code, ohne das Verhalten zu aendern.

### Mindest-Testfaelle

1. Ein Name wird hinzugefuegt, `AnzahlWartende()` steigt auf 1.
2. Zwei Namen werden hinzugefuegt, Aufrufreihenfolge ist FIFO.
3. `NaechstenAufrufen()` auf leerer Warteschlange wirft `InvalidOperationException`.
4. Leerer oder whitespace-Name bei `TicketZiehen` wirft `ArgumentException`.

### Ziel

Der Fokus liegt auf dem TDD-Prozess, nicht auf einer grossen Menge an Produktivcode.
Dokumentiere pro Test kurz, in welcher Phase du warst (`Red`, `Green` oder `Refactor`).

### Muster-Lösung

```csharp
using System;
using System.Collections.Generic;
using Xunit;

// Minimale Beispiel-Implementierung, damit die Tests nachvollziehbar sind.
public class WarteschlangenManager
{
   private readonly Queue<string> _wartende = new();

   public void TicketZiehen(string name)
   {
      if (string.IsNullOrWhiteSpace(name))
      {
         throw new ArgumentException("Name darf nicht leer sein.", nameof(name));
      }
      _wartende.Enqueue(name);
   }

   public string NaechstenAufrufen()
   {
      if (_wartende.Count == 0)
      {
         throw new InvalidOperationException("Keine wartenden Personen vorhanden.");
      }

      return _wartende.Dequeue();
   }

   public int AnzahlWartende()
   {
      return _wartende.Count;
   }
}
```

### Schuelerfassung (ohne Muster-Implementierung)

Verwende diese Variante, wenn die Lernenden den Produktivcode vollstaendig selbst im TDD-Zyklus entwickeln sollen.

```csharp
using System;
using Xunit;

public class WarteschlangenManagerTddTests
{
   [Fact]
   public void Red_01_TicketZiehen_Erhoeht_AnzahlWartende()
   {
      var sut = new WarteschlangenManager();
      sut.TicketZiehen("Anna");
      Assert.Equal(1, sut.AnzahlWartende());
   }

   [Fact]
   public void Red_02_NaechstenAufrufen_Beachtet_FIFO()
   {
      var sut = new WarteschlangenManager();
      sut.TicketZiehen("Anna");
      sut.TicketZiehen("Ben");
      Assert.Equal("Anna", sut.NaechstenAufrufen());
      Assert.Equal("Ben", sut.NaechstenAufrufen());
   }

   [Fact]
   public void Red_03_NaechstenAufrufen_Leer_WirftInvalidOperationException()
   {
      var sut = new WarteschlangenManager();
      Assert.Throws<InvalidOperationException>(() => sut.NaechstenAufrufen());
   }

   [Theory]
   [InlineData("")]
   [InlineData("   ")]
   public void Red_04_TicketZiehen_LeererName_WirftArgumentException(string name)
   {
      var sut = new WarteschlangenManager();
      Assert.Throws<ArgumentException>(() => sut.TicketZiehen(name));
   }
}
```

Hinweis fuer die Durchfuehrung:

1. Starte mit genau einem Test und lasse ihn fehlschlagen (`Red`).
2. Implementiere minimalen Produktivcode bis der Test laeuft (`Green`).
3. Bereinige Namen/Struktur ohne Verhaltensaenderung (`Refactor`).
4. Erst dann den naechsten Test aktivieren.
