using System;

namespace Woche_2_Loesung
{
    // --- Teil 1: Factory Method Pattern ---
    
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
    public interface IBuergerService
    {
        string GetVollstaendigerName(int id);
    }

    // Der Adapter, der das alte System an die neue Schnittstelle anpasst
    public class RegisterAdapter : IBuergerService
    {
        private readonly LegacyRegister _legacyRegister;

        public RegisterAdapter(LegacyRegister legacyRegister)
        {
            _legacyRegister = legacyRegister;
        }

        public string GetVollstaendigerName(int id)
        {
            string rawData = _legacyRegister.GetDataById(id);
            string[] parts = rawData.Split('|');
            
            if (parts.Length < 3) return "Fehler: Ungültige Daten";

            string vorname = parts[1];
            string nachname = parts[2];

            return $"{vorname} {nachname}";
        }
    }

    // --- Main ---
    class Program
    {
        static void Main()
        {
            Console.WriteLine("--- Aufgabe 1: Factory Method ---");
            Dokument dok1 = DokumentFabrik.ErstelleDokument(DokumentTyp.Bescheid);
            dok1.Drucken();

            Console.WriteLine("\n--- Aufgabe 2: Adapter ---");
            LegacyRegister altesSystem = new LegacyRegister();
            IBuergerService service = new RegisterAdapter(altesSystem);
            
            string name = service.GetVollstaendigerName(1);
            Console.WriteLine($"Bürger aus modernem Service: {name}");
        }
    }
}
