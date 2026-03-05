using System;
using System.Collections.Generic;

namespace Woche_3_Loesung
{
    // --- Aufgabe 1: Strategy Pattern (Parkgebühren) ---

    public interface IParkGebuehrStrategie
    {
        decimal BerechneParkGebuehr(int minuten);
    }

    public class StandardParken : IParkGebuehrStrategie
    {
        public decimal BerechneParkGebuehr(int minuten) => minuten * 0.05m;
    }

    public class AnwohnerParken : IParkGebuehrStrategie
    {
        public decimal BerechneParkGebuehr(int minuten) => minuten * 0.01m;
    }

    public class ElektroAutoParken : IParkGebuehrStrategie
    {
        public decimal BerechneParkGebuehr(int minuten)
        {
            int kostenpflichtigeMinuten = Math.Max(0, minuten - 60);
            return kostenpflichtigeMinuten * 0.03m;
        }
    }

    public class ParkscheinAutomat
    {
        private IParkGebuehrStrategie _strategie;

        public void SetStrategie(IParkGebuehrStrategie strategie)
        {
            _strategie = strategie;
        }

        public decimal Berechne(int minuten)
        {
            if (_strategie == null) throw new InvalidOperationException("Keine Strategie gesetzt.");
            return _strategie.BerechneParkGebuehr(minuten);
        }
    }

    // --- Aufgabe 2: Observer Pattern (Antragsbenachrichtigung) ---

    public class Antrag
    {
        public int ID { get; set; }
        public string BuergerName { get; set; }
        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    BenachrichtigeAlle();
                }
            }
        }

        private List<IBenachrichtigungsService> _beobachter = new List<IBenachrichtigungsService>();

        public void Registriere(IBenachrichtigungsService beobachter) => _beobachter.Add(beobachter);
        
        private void BenachrichtigeAlle()
        {
            foreach (var b in _beobachter)
            {
                b.Informiere(this);
            }
        }
    }

    public interface IBenachrichtigungsService
    {
        void Informiere(Antrag antrag);
    }

    public class EmailService : IBenachrichtigungsService
    {
        public void Informiere(Antrag antrag)
        {
            Console.WriteLine($"[EMAIL] Sende Nachricht an {antrag.BuergerName}: Antrag #{antrag.ID} ist nun: {antrag.Status}");
        }
    }

    public class SmsService : IBenachrichtigungsService
    {
        public void Informiere(Antrag antrag)
        {
            Console.WriteLine($"[SMS] Sende Nachricht an {antrag.BuergerName}: Statusänderung bei Antrag #{antrag.ID} -> {antrag.Status}");
        }
    }

    // --- Main ---
    class Program
    {
        static void Main()
        {
            Console.WriteLine("--- Aufgabe 1: Strategy Pattern ---");
            var automat = new ParkscheinAutomat();
            
            automat.SetStrategie(new StandardParken());
            Console.WriteLine($"Standard 120min: {automat.Berechne(120):C}");

            automat.SetStrategie(new ElektroAutoParken());
            Console.WriteLine($"Elektro 120min: {automat.Berechne(120):C}");

            Console.WriteLine("\n--- Aufgabe 2: Observer Pattern ---");
            var antrag = new Antrag { ID = 101, BuergerName = "Max Mustermann", Status = "Eingegangen" };
            
            antrag.Registriere(new EmailService());
            antrag.Registriere(new SmsService());

            Console.WriteLine("Ändere Status...");
            antrag.Status = "In Bearbeitung";

            Console.WriteLine("Ändere Status erneut...");
            antrag.Status = "Abgeschlossen";
        }
    }
}
