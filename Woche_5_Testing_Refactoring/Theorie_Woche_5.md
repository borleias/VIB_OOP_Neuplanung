# Theorie Woche 5: Unit Testing mit xUnit

## 1. Warum testen wir?
- **Sicherheit:** Codeänderungen (Refactoring) führen nicht zu Fehlern in bestehender Logik.
- **Dokumentation:** Tests zeigen, wie der Code verwendet werden soll.
- **Design:** Testbarer Code ist automatisch modularer (DIP).

## 2. Struktur eines Tests (AAA-Pattern)
- **Arrange:** Alles vorbereiten (Objekte erzeugen, Mocks setzen).
- **Act:** Die zu testende Methode aufrufen.
- **Assert:** Das Ergebnis mit dem erwarteten Wert vergleichen.

## 3. xUnit in C#
xUnit ist das Standard-Testing-Framework für moderne .NET-Apps.

### Beispiel:
```csharp
public class CalculatorTests {
    [Fact] // Ein einfacher Test
    public void Add_TwoPositiveNumbers_ReturnsCorrectSum() {
        // Arrange
        var calc = new Calculator();
        // Act
        var result = calc.Add(2, 3);
        // Assert
        Assert.Equal(5, result);
    }

    [Theory] // Parametrisierter Test
    [InlineData(1, 2, 3)]
    [InlineData(-1, 1, 0)]
    public void Add_MultipleInputs_ReturnsCorrectSums(int a, int b, int expected) {
        var calc = new Calculator();
        var result = calc.Add(a, b);
        Assert.Equal(expected, result);
    }
}
```

## 4. Testen von Abhängigkeiten (Mocking)
Da wir in Woche 4 Interfaces eingeführt haben, können wir nun "Fake"-Objekte (Mocks) erstellen, um Klassen isoliert zu testen, ohne echte Dateien zu schreiben oder E-Mails zu senden.
