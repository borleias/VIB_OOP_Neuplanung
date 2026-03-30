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

// --- Aufgabe 4: State Pattern (Dokumentenworkflow) ---

public interface IDokumentZustand
{
    string ZustandName { get; }
    void Bearbeiten(DokumentWorkflow kontext);
    void Einreichen(DokumentWorkflow kontext);
    void Genehmigen(DokumentWorkflow kontext);
    void Ablehnen(DokumentWorkflow kontext);
}

public class DokumentWorkflow
{
    public string Titel { get; }
    public IDokumentZustand AktuellerZustand { get; private set; }

    public DokumentWorkflow(string titel)
    {
        Titel = titel;
        AktuellerZustand = new EntwurfZustand();
    }

    public void SetzeZustand(IDokumentZustand zustand)
    {
        AktuellerZustand = zustand;
        Console.WriteLine($"Neuer Zustand: {AktuellerZustand.ZustandName}");
    }

    public void Bearbeiten() => AktuellerZustand.Bearbeiten(this);
    public void Einreichen() => AktuellerZustand.Einreichen(this);
    public void Genehmigen() => AktuellerZustand.Genehmigen(this);
    public void Ablehnen() => AktuellerZustand.Ablehnen(this);
}

public class EntwurfZustand : IDokumentZustand
{
    public string ZustandName => "Entwurf";

    public void Bearbeiten(DokumentWorkflow kontext)
    {
        Console.WriteLine($"'{kontext.Titel}' wird bearbeitet.");
    }

    public void Einreichen(DokumentWorkflow kontext)
    {
        Console.WriteLine($"'{kontext.Titel}' wurde eingereicht.");
        kontext.SetzeZustand(new InPruefungZustand());
    }

    public void Genehmigen(DokumentWorkflow kontext)
    {
        Console.WriteLine($"Aktion nicht moeglich im Zustand {ZustandName}.");
    }

    public void Ablehnen(DokumentWorkflow kontext)
    {
        Console.WriteLine($"Aktion nicht moeglich im Zustand {ZustandName}.");
    }
}

public class InPruefungZustand : IDokumentZustand
{
    public string ZustandName => "InPruefung";

    public void Bearbeiten(DokumentWorkflow kontext)
    {
        Console.WriteLine($"Aktion nicht moeglich im Zustand {ZustandName}.");
    }

    public void Einreichen(DokumentWorkflow kontext)
    {
        Console.WriteLine($"Aktion nicht moeglich im Zustand {ZustandName}.");
    }

    public void Genehmigen(DokumentWorkflow kontext)
    {
        Console.WriteLine($"'{kontext.Titel}' wurde genehmigt.");
        kontext.SetzeZustand(new FreigegebenZustand());
    }

    public void Ablehnen(DokumentWorkflow kontext)
    {
        Console.WriteLine($"'{kontext.Titel}' wurde abgelehnt.");
        kontext.SetzeZustand(new AbgelehntZustand());
    }
}

public class FreigegebenZustand : IDokumentZustand
{
    public string ZustandName => "Freigegeben";

    public void Bearbeiten(DokumentWorkflow kontext)
    {
        Console.WriteLine($"Aktion nicht moeglich im Zustand {ZustandName}.");
    }

    public void Einreichen(DokumentWorkflow kontext)
    {
        Console.WriteLine($"Aktion nicht moeglich im Zustand {ZustandName}.");
    }

    public void Genehmigen(DokumentWorkflow kontext)
    {
        Console.WriteLine($"Aktion nicht moeglich im Zustand {ZustandName}.");
    }

    public void Ablehnen(DokumentWorkflow kontext)
    {
        Console.WriteLine($"Aktion nicht moeglich im Zustand {ZustandName}.");
    }
}

public class AbgelehntZustand : IDokumentZustand
{
    public string ZustandName => "Abgelehnt";

    public void Bearbeiten(DokumentWorkflow kontext)
    {
        Console.WriteLine($"'{kontext.Titel}' wird ueberarbeitet und geht zurueck in den Entwurf.");
        kontext.SetzeZustand(new EntwurfZustand());
    }

    public void Einreichen(DokumentWorkflow kontext)
    {
        Console.WriteLine($"Aktion nicht moeglich im Zustand {ZustandName}.");
    }

    public void Genehmigen(DokumentWorkflow kontext)
    {
        Console.WriteLine($"Aktion nicht moeglich im Zustand {ZustandName}.");
    }

    public void Ablehnen(DokumentWorkflow kontext)
    {
        Console.WriteLine($"Aktion nicht moeglich im Zustand {ZustandName}.");
    }
}

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
