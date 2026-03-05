---
title: "Objektorientierte Programmierung (Vertiefung)"
subtitle: "Woche 5: Qualitätssicherung – Testing und Refactoring"
author: "Dr. Peter Bernhardt"
date: "März 2026"
section-titles: true
---

# Teil 1: Einführung in Automatisierte Tests

## Willkommen zu Woche 5
Software, die ungetestet ist, ist Software, der man nicht vertrauen kann. Bisher haben wir unseren Code manuell geprüft, indem wir das Programm gestartet und Eingaben gemacht haben. Das ist auf Dauer nicht tragbar.
Heute lernen wir, wie Code automatisch seinen eigenen Code testet.

## Die Kosten von unentdeckten Fehlern
Warum testen wir?
Je später ein Fehler (Bug) im Software-Lifecycle gefunden wird, desto teurer wird seine Behebung.
- Gefunden beim Coden: Sekunden (Kosten = ~0€)
- Gefunden beim Build: Minuten
- Gefunden durch Tester: Stunden bis Tage
- Gefunden in Produktion: Krisenmeetings, Datenverlust, massiver Imageschaden.

## Was ist ein Unit Test?
Ein Unit Test (Einheitentest) ist ein kleines, automatisiertes Code-Snippet, das eine spezifische Funktionalität der Anwendung (meist eine einzige Methode einer Klasse) prüft.
Er stellt sicher, dass sich die Methode exakt so verhält, wie der Entwickler es bei der Erstellung vorgesehen hat.

## Ziele des Unit Testings
1. **Korrektheit:** Beweisen, dass der Code tut, was er soll.
2. **Regressionsschutz:** Sicherstellen, dass neue Änderungen alte Funktionen nicht brechen.
3. **Dokumentation:** Tests zeigen anderen Entwicklern, wie eine Klasse benutzt werden soll.
4. **Design-Check:** Code, der schwer zu testen ist, ist meist auch schlecht entworfen (enge Kopplung).

# Teil 2: Tests schreiben mit xUnit

## xUnit in .NET
xUnit ist das De-facto-Standard-Framework für das Testen von C#-Anwendungen.
Es bietet uns Attribute wie `[Fact]` und `[Theory]`, um dem Compiler mitzuteilen: "Dies ist keine normale Methode, sondern ein Testlauf."

## Die Struktur: Das AAA-Pattern
Jeder gute Unit Test folgt dem AAA-Schema:
1. **Arrange (Vorbereiten):** Testobjekte instanziieren, Dummy-Daten anlegen.
2. **Act (Ausführen):** Genau *eine* Methode am Testobjekt aufrufen.
3. **Assert (Prüfen):** Das Ergebnis der Methode mit dem erwarteten Ergebnis vergleichen.

## Der Unit Testing Lifecycle
Ein Testlauf besteht aus:
1. **Setup:** (Optional) Vorbereiten der Umgebung.
2. **Execution:** Ausführen des Tests (AAA).
3. **Teardown:** (Optional) Aufräumen (z.B. Dateien löschen).
In xUnit wird für jeden Test eine neue Instanz der Testklasse erstellt, was für maximale Isolation sorgt.

## xUnit Attribute: [Fact]
Das `[Fact]` Attribut kennzeichnet einen invarianten Testfall – also eine Prüfung, die immer das gleiche Ergebnis liefern sollte, egal welche Daten vorliegen (innerhalb des Szenarios).

```csharp
using Xunit;

public class SteuerIdValidatorTests
{
    [Fact] 
    public void Validieren_SollteTrueZurueckgeben_WennId11StelligIst()
    {
        // Arrange
        var validator = new SteuerIdValidator();
        string korrekteId = "12345678901";

        // Act
        bool result = validator.Validieren(korrekteId);

        // Assert
        Assert.True(result);
    }
}
```

## xUnit Attribute: [Theory]
Mit `[Theory]` kennzeichnen wir parametrisierte Tests. Wir sagen: "Diese Logik gilt für eine ganze Reihe von Daten."

```csharp
    [Theory] 
    [InlineData("123")] // Zu kurz
    [InlineData("123456789012")] // Zu lang
    [InlineData("ABC45678901")] // Buchstaben
    public void Validieren_SollteFalseZurueckgeben_WennIdUngueltigIst(string ungueltigeId)
    {
        var validator = new SteuerIdValidator();
        bool result = validator.Validieren(ungueltigeId);
        Assert.False(result);
    }
```

## InlineData und alternative Quellen
`[InlineData]` ist der einfachste Weg, Testdaten direkt im Code zu definieren. Für größere Datenmengen bietet xUnit auch `[MemberData]` oder `[ClassData]`, um Daten aus anderen Methoden oder Klassen zu laden.

## Assertions: Mehr als nur True/False
Die `Assert`-Klasse bietet viele hilfreiche Methoden:
- `Assert.Equal(expected, actual)`
- `Assert.Contains("Teiltext", resultString)`
- `Assert.Throws<ArgumentException>(() => method())`
- `Assert.Empty(collection)`

## Die Wichtigkeit der Namensgebung bei Tests
Testnamen müssen nicht kurz sein. Sie sollten wie eine Dokumentation gelesen werden können.
Namensschema: `[ZuTestendeMethode]_[Szenario]_[ErwartetesErgebnis]`
Beispiel: `BerechneGebuehr_WennFahrzeugElektroIst_GibtRabattZurueck()`
Wenn der Test in der CI-Pipeline fehlschlägt, weiß der Entwickler anhand des Namens sofort, was kaputt ist.

# Teil 3: Mocking von Abhängigkeiten

## Die wichtigste Regel: Isolation
Ein Unit Test muss *isoliert* ablaufen. 
Er darf nicht auf das Dateisystem, nicht auf das Netzwerk und vor allem nicht auf eine echte Datenbank zugreifen! Warum? Weil der Test sonst unzuverlässig (flaky) wird.

## Das Problem: Infrastruktur-Abhängigkeiten
Stellen Sie sich vor, Sie wollen den `RegistrierungsService` testen. Dieser Service speichert Daten in einer echten Datenbank und sendet eine echte E-Mail.
Problem: Wenn die Datenbank down ist, schlägt Ihr Test fehl, obwohl Ihr Code korrekt ist. Der Test ist langsam und hinterlässt "Müll" in der DB.

## Die Lösung: Warum Interfaces?
Weil wir gegen Interfaces programmieren, können wir die "echte" Datenbank im Test durch eine Attrappe (Mock) ersetzen. Ein Mock implementiert das Interface, tut aber nichts Kritisches.

## Der zu testende Service (Code)
Dieser Service soll getestet werden. Er hängt von `IDatenbank` ab:

```csharp
public class RegistrierungsService
{
    private readonly IDatenbank _db;
    public RegistrierungsService(IDatenbank db) => _db = db;

    public void Registriere(Buerger b)
    {
        if (b.Alter < 18) throw new ArgumentException("Zu jung");
        _db.Speichern(b);
    }
}
```

## Der Mock-Einsatz im Test
Im Test-Projekt bauen wir uns eine Attrappe, die so tut, als wäre sie die Datenbank.

```csharp
// Unsere Attrappe
public class DatenbankMock : IDatenbank
{
    public int SpeichernAufrufe { get; private set; } = 0;
    
    public void Speichern(Buerger b) 
    {
        SpeichernAufrufe++; // Wir zählen nur mit!
    }
}

// Im Arrange-Teil des Tests nutzen wir den Mock:
var dbMock = new DatenbankMock();
var service = new RegistrierungsService(dbMock);
```
So testen wir **nur** die Logik des Services, ohne die Datenbank zu berühren.

# Teil 4: Test-Driven Development (TDD)

## Was ist Test-Driven Development?
TDD dreht den klassischen Workflow um. Wir schreiben den Test *nicht* am Ende, wenn das Feature fertig ist.
Wir schreiben den Test **zuerst**, bevor überhaupt eine einzige Zeile Fachcode existiert!

## Der Rote-Grün-Refactor Zyklus
TDD ist ein iterativer Zyklus, der in drei Phasen abläuft:
1. **Red (Rot):** Einen fehlschlagenden Test schreiben.
2. **Green (Grün):** Den minimal nötigen Code schreiben, um den Test grün zu machen.
3. **Refactor (Aufräumen):** Den entstandenen Code elegant und sauber strukturieren.

## Phase 1: RED
Sie überlegen sich, was die Methode tun soll. Sie schreiben den Test. Da die Klasse oder Methode noch gar nicht existiert, kompiliert es vielleicht nicht mal. Sie führen den Test aus: Er ist ROT.
*Warum ist das wichtig?* So stellen Sie sicher, dass Ihr Test überhaupt in der Lage ist, fehzuschlagen (und nicht durch einen Designfehler immer "grün" anzeigt).

## Phase 2: GREEN
Sie schreiben den Produktiv-Code. Wichtig: Schreiben Sie keine komplexen Architekturen. Das Ziel ist einzig und allein, den Test so schnell wie möglich grün (erfolgreich) zu bekommen. Wenn Sie "hart" einen String zurückgeben müssen, tun Sie es. 

## Phase 3: REFACTOR
Der Test ist grün. Sie haben ein Sicherheitsnetz! Jetzt schauen Sie sich den Produktiv-Code an.
Gibt es Magic Numbers? Ist die Methode zu lang? Sind die Variablennamen schlecht?
Sie passen den Code an. Nach jeder kleinen Änderung lassen Sie die Tests laufen. Solange sie grün bleiben, haben Sie nichts kaputt gemacht.

## Warum TDD so mächtig ist
- **100% Testabdeckung:** Da kein Code ohne vorherigen Test geschrieben wird, ist das gesamte System automatisch getestet.
- **Lebende Dokumentation:** Die Tests beschreiben das Systemverhalten besser als jedes Word-Dokument.
- **Besseres Design:** Wer TDD anwendet, baut automatisch entkoppelte, gut strukturierte Klassen (da sie sonst gar nicht vorab testbar wären).

# Teil 5: Refactoring-Techniken

## Was ist Refactoring?
Refactoring ist die Verbesserung der inneren Struktur ohne Änderung des äußeren Verhaltens. Es ist das Aufräumen des Codes, um ihn lesbar und wartbar zu halten.

## Häufige Refactoring-Techniken
1. **Extract Method:** Eine zu lange Methode in mehrere kleine aufteilen.
2. **Rename Variable:** Unklare Namen (z.B. `x`) durch sprechende Namen (z.B. `antragsId`) ersetzen.
3. **Move Method:** Eine Methode in die Klasse verschieben, zu der sie logisch gehört.
4. **Remove Duplication:** Den "DRY" (Don't Repeat Yourself) Grundsatz anwenden.

## Wann refaktorisieren?
- Immer wenn der Code im "Green"-Zustand ist.
- Wenn Sie eine neue Funktion hinzufügen wollen, aber der bestehende Code zu starr ist (Refactor first!).
- Wenn Sie "Code Smells" entdecken.

## Die goldene Regel des Refactorings
**Niemals refaktorisieren, wenn die Unit-Tests rot sind!**
Ohne ein grünes Sicherheitsnetz ist Refactoring extrem riskant und führt oft zu neuen Fehlern, die erst spät bemerkt werden.

# Teil 6: Zusammenfassung

## Wrap-up Woche 5
- **Unit Tests:** Sichern die Basis ab und dienen als lebendige Dokumentation.
- **xUnit & AAA:** Ein strukturierter Standard für alle .NET Entwickler.
- **Mocking:** Isoliert Logik von unzuverlässiger Infrastruktur.
- **TDD:** Ein Workflow, der zu besserem Design und hoher Qualität führt.

## Ausblick auf nächste Woche
In der finalen Theorie-Woche (Woche 6) betrachten wir asynchrone Programmierung (`async`/`await`) und professionelles Error Handling. Zudem besprechen wir die Details zu Ihren Abschlussprojekten!
