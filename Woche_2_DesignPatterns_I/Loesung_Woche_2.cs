using System;

namespace Woche_2_Loesung
{
    // --- Teil 1: Simple Factory Pattern ---
    
    public enum DokumentTyp { Antrag, Bescheid, Rechnung }

    public abstract class Dokument
    {
        public string Titel { get; protected set; }
        public abstract void Drucken();
    }

    public class Antrag : Dokument
    {
        public Antrag() => Titel = "Antragsformular";
        public override void Drucken() => Console.WriteLine($"Drucke: {Titel}");
    }

    public class Bescheid : Dokument
    {
        public Bescheid() => Titel = "Offizieller Bescheid";
        public override void Drucken() => Console.WriteLine($"Drucke: {Titel}");
    }

    public class Rechnung : Dokument
    {
        public Rechnung() => Titel = "Gebührenrechnung";
        public override void Drucken() => Console.WriteLine($"Drucke: {Titel}");
    }

    public static class DokumentFabrik
    {
        public static Dokument ErstelleDokument(DokumentTyp typ)
        {
            return typ switch
            {
                DokumentTyp.Antrag => new Antrag(),
                DokumentTyp.Bescheid => new Bescheid(),
                DokumentTyp.Rechnung => new Rechnung(),
                _ => throw new ArgumentException("Ungültiger Dokumenttyp")
            };
        }
    }

    // --- Teil 2: Adapter Pattern ---

    // Legacy-System, das wir nicht ändern können
    public class LegacyRegister
    {
        public string GetDataById(int id)
        {
            // Simuliert Daten vom alten System (ID|Vorname|Nachname|Stadt)
            return id switch
            {
                1 => "1|Max|Mustermann|Berlin",
                2 => "2|Erika|Musterfrau|München",
                _ => "0|Unbekannt|Unbekannt|N/A"
            };
        }
    }

    // Die moderne Schnittstelle, die wir in unserer Anwendung nutzen wollen
    public class Buerger
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Wohnort { get; set; }

        public override string ToString() => $"{Vorname} {Nachname} (ID: {Id}, Wohnort: {Wohnort})";
    }

    public interface IBuergerService
    {
        Buerger GetBuerger(int id);
    }

    // Der Adapter, der das alte System an die neue Schnittstelle anpasst
    public class RegisterAdapter : IBuergerService
    {
        private readonly LegacyRegister _legacyRegister;

        public RegisterAdapter(LegacyRegister legacyRegister)
        {
            _legacyRegister = legacyRegister;
        }

        public Buerger GetBuerger(int id)
        {
            string rawData = _legacyRegister.GetDataById(id);
            string[] parts = rawData.Split('|');

            if (parts.Length < 4)
                throw new FormatException($"Ungültiges Datenformat vom Legacy-System: '{rawData}'");

            return new Buerger
            {
                Id = int.Parse(parts[0]),
                Vorname = parts[1],
                Nachname = parts[2],
                Wohnort = parts[3]
            };
        }
    }

    // --- Teil 4: Decorator Pattern ---

    public interface IWeihnachtsbaum
    {
        string GetBeschreibung();
        double GetKosten();
    }

    public class EinfacherWeihnachtsbaum : IWeihnachtsbaum
    {
        public string GetBeschreibung() => "Weihnachtsbaum";
        public double GetKosten() => 20.00;
    }

    public abstract class BaumDecorator : IWeihnachtsbaum
    {
        protected readonly IWeihnachtsbaum _baum;
        protected BaumDecorator(IWeihnachtsbaum baum) => _baum = baum;

        public virtual string GetBeschreibung() => _baum.GetBeschreibung();
        public virtual double GetKosten() => _baum.GetKosten();
    }

    public class Lichterkette : BaumDecorator
    {
        public Lichterkette(IWeihnachtsbaum baum) : base(baum) { }
        public override string GetBeschreibung() => base.GetBeschreibung() + " + Lichterkette";
        public override double GetKosten() => base.GetKosten() + 15.00;
    }

    public class Weihnachtskugeln : BaumDecorator
    {
        public Weihnachtskugeln(IWeihnachtsbaum baum) : base(baum) { }
        public override string GetBeschreibung() => base.GetBeschreibung() + " + Weihnachtskugeln";
        public override double GetKosten() => base.GetKosten() + 8.00;
    }

    public class Lametta : BaumDecorator
    {
        public Lametta(IWeihnachtsbaum baum) : base(baum) { }
        public override string GetBeschreibung() => base.GetBeschreibung() + " + Lametta";
        public override double GetKosten() => base.GetKosten() + 5.00;
    }

    // --- Main ---
    class Program
    {
        static void Main()
        {
            Console.WriteLine("--- Aufgabe 1: Simple Factory ---");
            Dokument dok1 = DokumentFabrik.ErstelleDokument(DokumentTyp.Bescheid);
            dok1.Drucken();
            Dokument dok2 = DokumentFabrik.ErstelleDokument(DokumentTyp.Rechnung);
            dok2.Drucken();

            Console.WriteLine("\n--- Aufgabe 2: Adapter ---");
            LegacyRegister altesSystem = new LegacyRegister();
            IBuergerService service = new RegisterAdapter(altesSystem);

            Buerger buerger = service.GetBuerger(1);
            Console.WriteLine($"Bürger: {buerger}");

            Console.WriteLine("\n--- Aufgabe 4: Decorator ---");
            IWeihnachtsbaum baum = new EinfacherWeihnachtsbaum();
            baum = new Lichterkette(baum);
            baum = new Weihnachtskugeln(baum);
            baum = new Lametta(baum);

            Console.WriteLine(baum.GetBeschreibung());
            // Ausgabe: Weihnachtsbaum + Lichterkette + Weihnachtskugeln + Lametta
            Console.WriteLine($"Gesamtkosten: {baum.GetKosten():F2} EUR");
            // Ausgabe: 48.00 EUR
        }
    }
}
