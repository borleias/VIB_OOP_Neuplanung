static void MainManuell(string[] args) {
    // Manuelles Zusammenbauen der Abhängigkeiten
    IFachregelPruefer fachregelPruefer = new FachregelPruefer();
    IDatenbankService datenbankService = new DatenbankService();
    IPdfErsteller pdfErsteller = new PdfErsteller();

    AntragManager antragManager = new AntragManager(fachregelPruefer, datenbankService, pdfErsteller);

    Antrag antrag = new Antrag(); // Beispielantrag
    antragManager.BearbeiteAntrag(antrag);
}

static void Main(string[] args) {
    // Zusammenbauen der Abhängigkeiten mittels DI-Container
    var services = new ServiceCollection();
    services.AddTransient<IFachregelPruefer, FachregelPruefer>();
    services.AddTransient<IDatenbankService, DatenbankService>();
    services.AddTransient<IPdfErsteller, PdfErsteller>();
    services.AddTransient<AntragManager>();
    var serviceProvider = services.BuildServiceProvider();

    AntragManager antragManager = serviceProvider.GetRequiredService<AntragManager>();

    Antrag antrag = new Antrag(); // Beispielantrag
    antragManager.BearbeiteAntrag(antrag);
}

// Die *Gott-Klasse*: Eine Klasse mit einer Methode, die sowohl für die Prüfung 
// der Fachregeln, das Zusammenbauen eines SQL-Strings und den Datenbankzugriff sowie das 
// Erstellen des PDFs für den Ausdruck verantwortlich ist.
public class AntragManager {
    public void BearbeiteAntrag(Antrag antrag) {
        // 1. Fachregeln prüfen
        bool fachregelnErfuellt = [...] // Logik zur Prüfung der Fachregeln
        if (!fachregelnErfuellt) {
            throw new Exception("Antrag erfüllt nicht die Fachregeln.");
        }

        // 2. SQL-String zusammenbauen
        string sql = "INSERT INTO Antraege ..."; // Logik zum Zusammenbauen des SQL-Strings

        // 3. Datenbankzugriff
        Datenbank.Execute(sql);

        // 4. PDF erstellen und drucken
        string pdfPfad = [...] // Logik zum Erstellen des PDFs;
        Drucker.Drucke(pdfPfad);
    }
}

// Schlechtes SRP-Beispiel: Eine Klasse mit mehreren Methoden, die sowohl für die Prüfung 
// der Fachregeln, das Zusammenbauen eines SQL-Strings und den Datenbankzugriff sowie 
// das Erstellen des PDFs für den Ausdruck verantwortlich ist.
public class AntragManager {
    public void BearbeiteAntrag(Antrag antrag) {
        // 1. Fachregeln prüfen
        if (!PruefeFachregeln(antrag)) {
            throw new Exception("Antrag erfüllt nicht die Fachregeln.");
        }

        // 2. SQL-String zusammenbauen
        string sql = BaueSqlString(antrag);

        // 3. Datenbankzugriff
        Datenbank.Execute(sql);

        // 4. PDF erstellen und drucken
        string pdfContent = ErstellePdf(antrag);
        Drucker.Drucke(pdfContent);
    }

    private bool PruefeFachregeln(Antrag antrag) {
        // Logik zur Prüfung der Fachregeln
        return true; // Placeholder
    }

    private string BaueSqlString(Antrag antrag) {
        // Logik zum Zusammenbauen des SQL-Strings
        return "INSERT INTO Antraege ..."; // Placeholder
    }

    private string ErstellePdf(Antrag antrag) {
        // Logik zum Erstellen des PDFs
        return "PDF-Inhalt"; // Placeholder
    }
}

// Gutes SRP-Beispiel: Jede Klasse hat nur eine Verantwortlichkeit, 
// z.B. eine Klasse für die Prüfung der Fachregeln, eine Klasse für den Datenbankzugriff 
//und eine Klasse für die PDF-Erstellung.
public class AntragManager {
    private readonly FachregelPruefer _fachregelPruefer = new FachregelPruefer();
    private readonly DatenbankService _datenbankService = new DatenbankService();
    private readonly PdfErsteller _pdfErsteller = new PdfErsteller();

    public void BearbeiteAntrag(Antrag antrag) {
        // 1. Fachregeln prüfen
        if (!_fachregelPruefer.Pruefe(antrag)) {
            throw new Exception("Antrag erfüllt nicht die Fachregeln.");
        }

        // 2. Datenbankzugriff
        _datenbankService.SpeichereAntrag(antrag);

        // 3. PDF erstellen und drucken
        string pdfContent = _pdfErsteller.ErstellePdf(antrag);
        Drucker.Drucke(pdfContent);
    }
}

public class FachregelPruefer {
    public bool Pruefe(Antrag antrag) {
        // Logik zur Prüfung der Fachregeln
        return true; // Placeholder
    }
}

public class DatenbankService {
    public void SpeichereAntrag(Antrag antrag) {
        // Logik zum Speichern des Antrags in der Datenbank
    }
}

public class PdfErsteller {
    public string ErstellePdf(Antrag antrag) {
        // Logik zum Erstellen des PDFs
        return "PDF-Inhalt"; // Placeholder
    }
}

// Noch besseres SRP-Beispiel mit Dependency Injection: Jede Klasse hat nur eine Verantwortlichkeit.
// Durch die Verwendung von Dependency Injection kennt die `AntragManager`-Klasse nicht mehr die 
// konkreten Implementierungen der anderen Klassen, sondern arbeitet nur mit deren Schnittstellen. 
public class AntragManager {
    private readonly IFachregelPruefer _fachregelPruefer;
    private readonly IDatenbankService _datenbankService;
    private readonly IPdfErsteller _pdfErsteller;

    public AntragManager(IFachregelPruefer fachregelPruefer, IDatenbankService datenbankService, IPdfErsteller pdfErsteller) {
        _fachregelPruefer = fachregelPruefer;
        _datenbankService = datenbankService;
        _pdfErsteller = pdfErsteller;
    }

    public void BearbeiteAntrag(Antrag antrag) {
        // 1. Fachregeln prüfen
        if (!_fachregelPruefer.Pruefe(antrag)) {
            throw new Exception("Antrag erfüllt nicht die Fachregeln.");
        }

        // 2. Datenbankzugriff
        _datenbankService.SpeichereAntrag(antrag);

        // 3. PDF erstellen und drucken
        string pdfContent = _pdfErsteller.ErstellePdf(antrag);
        Drucker.Drucke(pdfContent);
    }
}

public interface IFachregelPruefer {
    bool Pruefe(Antrag antrag);
}

public interface IDatenbankService {
    void SpeichereAntrag(Antrag antrag);
}

public interface IPdfErsteller {
    string ErstellePdf(Antrag antrag);
}

public class FachregelPruefer : IFachregelPruefer {
    public bool Pruefe(Antrag antrag) {
        // Logik zur Prüfung der Fachregeln
        return true; // Placeholder
    }
}

public class DatenbankService : IDatenbankService {
    public void SpeichereAntrag(Antrag antrag) {
        // Logik zum Speichern des Antrags in der Datenbank
    }
}

public class PdfErsteller : IPdfErsteller {
    public string ErstellePdf(Antrag antrag) {
        // Logik zum Erstellen des PDFs
        return "PDF-Inhalt"; // Placeholder
    }
}

public interface IDokument {
    void Speichern();
    void Drucken();
}

public class BescheidDokument : IDokument {
    public void Speichern() { [...] }
    public void Drucken() { [...] }
}

public class DigitalOnlyDokument : IDokument {
    public void Speichern() { [...] }
    public void Drucken() {
        throw new NotImplementedException("Dieses Dokument kann nicht gedruckt werden.");
    }
}

static void Main() {
    List<IDokument> dokumente = new List<IDokument> {
        new BescheidDokument(),
        new DigitalOnlyDokument()
    };

    foreach (var doc in dokumente) {
        doc.Drucken(); // Hier wird die NotImplementedException ausgelöst!
    }
}
    
public interface ISpeicherbar {
    void Speichern();
}

public interface IDruckbar {
    void Drucken();
}

// 2. Klassen implementieren nur, was sie wirklich können
public class BescheidDokument : ISpeicherbar, IDruckbar {
    public void Speichern() { [...] }
    public void Drucken()   { [...] }
}

public class DigitalOnlyDokument : ISpeicherbar {
    public void Speichern() { [...] }
    // Kein Drucken – der Vertrag wird gar nicht erst versprochen
}

static void Main() {
    List<IDruckbar> druckbare = [ new BescheidDokument() ];
    foreach (var dok in druckbare)
        dok.Drucken(); // ✅ Kein Absturz möglich
}

public interface IDokument {
    void Speichern();
    void Drucken();
    void Validieren();
}

public interface ISpeicherbar {
    void Speichern();
}

public interface IDruckbar {
    void Drucken();
}

public interface IValidierbar {
    void Validieren();
}


public class Fachverfahren
{
    // Die Abhängigkeit wird hart verdrahtet
    private SqlDatenbank _db = new SqlDatenbank(); 

    public void Verarbeite(Antrag a) 
    {
        _db.Save(a);
    }
}

static void Main() {
    Fachverfahren fv = new Fachverfahren();
    Antrag antrag = new Antrag();
    fv.Verarbeite(antrag);
}

public class Fachverfahren
{
    private IDatenbank _db;

    // Die Abhängigkeit wird über den Konstruktor injiziert
    public Fachverfahren(IDatenbank db) 
    {
        _db = db;
    }

    public void Verarbeite(Antrag a) 
    {
        _db.Save(a);
    }
}

static void Main() {
    IDatenbank datenbank = new SqlDatenbank();
    Fachverfahren fv = new Fachverfahren(datenbank);
    Antrag antrag = new Antrag();
    fv.Verarbeite(antrag);
}