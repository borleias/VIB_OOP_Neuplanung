using System;
using Xunit;

namespace ParkSystem
{
    // Die zu testende Klasse
    public class ParkgebuehrenRechner
    {
        public double Berechne(string typ, double stunden)
        {
            if (stunden < 0) return 0; // Einfache Fehlerbehandlung

            return typ switch
            {
                "Standard" => stunden * 2.0,
                "Elektro" => stunden * 1.0,
                "Anwohner" => 0.0,
                _ => throw new ArgumentException("Unbekannter Fahrzeugtyp")
            };
        }
    }

    // Die Test-Klasse
    public class ParkgebuehrenTests
    {
        [Fact]
        public void Berechne_StandardFahrzeug_ReturnsCorrectFee()
        {
            // Arrange
            var rechner = new ParkgebuehrenRechner();
            // Act
            var fee = rechner.Berechne("Standard", 3);
            // Assert
            Assert.Equal(6.0, fee);
        }

        [Theory]
        [InlineData("Elektro", 2, 2.0)]
        [InlineData("Anwohner", 10, 0.0)]
        [InlineData("Elektro", 0, 0.0)]
        public void Berechne_MultipleTypes_ReturnExpectedFees(string typ, double stunden, double expected)
        {
            var rechner = new ParkgebuehrenRechner();
            var fee = rechner.Berechne(typ, stunden);
            Assert.Equal(expected, fee);
        }

        [Fact]
        public void Berechne_NegativeStunden_ReturnsZero()
        {
            var rechner = new ParkgebuehrenRechner();
            var fee = rechner.Berechne("Standard", -5);
            Assert.Equal(0, fee);
        }
    }
}
