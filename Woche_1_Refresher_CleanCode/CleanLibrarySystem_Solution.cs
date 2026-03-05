using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanLibrarySystem
{
    // Musterlösung Woche 1: Refactoring des DirtyLibrarySystem
    
    public enum BenutzerStatus
    {
        Inaktiv,
        Aktiv
    }

    public class Benutzer
    {
        public string Name { get; set; }
        public BenutzerStatus Status { get; set; }

        public Benutzer(string name, BenutzerStatus status)
        {
            Name = name;
            Status = status;
        }
    }

    public class Buch
    {
        public string Titel { get; set; }
        public bool IstAusgeliehen { get; set; }

        public Buch(string titel, bool istAusgeliehen = false)
        {
            Titel = titel;
            IstAusgeliehen = istAusgeliehen;
        }
    }

    public class Bibliothek
    {
        private List<Benutzer> _benutzer = new List<Benutzer>();
        private List<Buch> _buecher = new List<Buch>();

        public void RegistriereBenutzer(Benutzer benutzer) => _benutzer.Add(benutzer);
        public void FuegeBuchHinzu(Buch buch) => _buecher.Add(buch);

        public void LeiheBuchAus(string benutzerName, string buchTitel)
        {
            var benutzer = _benutzer.FirstOrDefault(b => b.Name == benutzerName);
            var buch = _buecher.FirstOrDefault(b => b.Titel == buchTitel);

            if (benutzer == null)
            {
                Console.WriteLine($"Benutzer '{benutzerName}' wurde nicht gefunden.");
                return;
            }

            if (benutzer.Status == BenutzerStatus.Inaktiv)
            {
                Console.WriteLine($"Ausleihe abgelehnt: Benutzer '{benutzerName}' ist inaktiv.");
                return;
            }

            if (buch == null)
            {
                Console.WriteLine($"Buch '{buchTitel}' wurde nicht gefunden.");
                return;
            }

            if (buch.IstAusgeliehen)
            {
                Console.WriteLine($"Das Buch '{buchTitel}' ist bereits ausgeliehen.");
            }
            else
            {
                buch.IstAusgeliehen = true;
                Console.WriteLine($"Erfolg: '{buchTitel}' wurde an '{benutzerName}' ausgeliehen.");
            }
        }

        public void ZeigeStatus()
        {
            Console.WriteLine("\n--- Bibliotheksstatus ---");
            foreach (var buch in _buecher)
            {
                string statusText = buch.IstAusgeliehen ? "Ausgeliehen" : "Verfügbar";
                Console.WriteLine($"{buch.Titel}: {statusText}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var bibliothek = new Bibliothek();

            // Initialisierung
            bibliothek.RegistriereBenutzer(new Benutzer("Peter", BenutzerStatus.Aktiv));
            bibliothek.RegistriereBenutzer(new Benutzer("Hans", BenutzerStatus.Inaktiv));

            bibliothek.FuegeBuchHinzu(new Buch("C# Profi"));
            bibliothek.FuegeBuchHinzu(new Buch("Python Skripte", istAusgeliehen: true));

            // Aktion: Ausleihe
            bibliothek.LeiheBuchAus("Peter", "C# Profi");
            bibliothek.LeiheBuchAus("Hans", "C# Profi"); // Sollte fehlschlagen

            // Statusanzeige
            bibliothek.ZeigeStatus();
        }
    }
}
