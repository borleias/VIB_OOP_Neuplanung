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

**Lernziele:**
- Unit Tests mit xUnit in .NET schreiben.
- Die AAA-Regel anwenden.
- Abhängigkeiten durch Mocking isolieren.
- Test-Driven Development (TDD) als Workflow verstehen.

## Die Kosten von unentdeckten Fehlern
Warum testen wir?
Je später ein Fehler (Bug) im Software-Lifecycle gefunden wird, desto teurer wird seine Behebung.
- Gefunden beim Coden: Sekunden (Kosten = ~0€)
- Gefunden beim Build: Minuten
- Gefunden durch Tester: Stunden bis Tage
- Gefunden vom Bürger im Produktivbetrieb (Produktion): Krisenmeetings, Datenverlust, massiver Imageschaden.

## Was ist ein Unit Test?
Ein Unit Test (Einheitentest) ist ein kleines, automatisiertes Code-Snippet, das eine spezifische Funktionalität der Anwendung (meist eine einzige Methode einer Klasse) prüft.
Er stellt sicher, dass sich die Methode exakt so verhält, wie der Entwickler es bei der Erstellung vorgesehen hat.

# Teil 2: Tests schreiben mit xUnit

## xUnit in .NET
xUnit ist das De-facto-Standard-Framework für das Testen von C#-Anwendungen (neben NUnit und MSTest).
Es bietet uns Attribute wie `[Fact]` und `[Theory]`, um dem Compiler mitzuteilen: "Dies ist keine normale Methode, sondern ein Testlauf."

## Die Struktur: Das AAA-Pattern
Jeder gute Unit Test folgt dem AAA-Schema:
1. **Arrange (Vorbereiten):** Testobjekte instanziieren, Dummy-Daten anlegen.
2. **Act (Ausführen):** Genau *eine* Methode am Testobjekt aufrufen.
3. **Assert (Prüfen):** Das Ergebnis der Methode mit dem erwarteten Ergebnis vergleichen.

## Code-Beispiel: Ein einfacher Test (Fact)
Das `[Fact]` Attribut kennzeichnet einen simplen Testfall.

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

## Code-Beispiel: Parametrisierte Tests (Theory)
Mit `[Theory]` und `[InlineData]` können wir denselben Test mit verschiedenen Datensätzen durchführen, um Randfälle (Edge Cases) abzuprüfen.

```csharp
    [Theory] 
    [InlineData("123")] // Zu kurz
    [InlineData("123456789012")] // Zu lang
    [InlineData("ABC45678901")] // Enthält Buchstaben
    public void Validieren_SollteFalseZurueckgeben_WennIdUngueltig(string id)
    {
        var validator = new SteuerIdValidator();
        bool result = validator.Validieren(id);
        Assert.False(result);
    }
```

## Die Wichtigkeit der Namensgebung bei Tests
Testnamen müssen nicht kurz sein. Sie sollten wie eine Dokumentation gelesen werden können.
Namensschema: `[ZuTestendeMethode]_[Szenario]_[ErwartetesErgebnis]`
Beispiel: `BerechneGebuehr_WennFahrzeugElektroIst_GibtRabattZurueck()`
Wenn der Test in der CI-Pipeline fehlschlägt, weiß der Entwickler anhand des Namens sofort, was kaputt ist.

# Teil 3: Mocking von Abhängigkeiten

## Die wichtigste Regel: Isolation
Ein Unit Test muss *isoliert* ablaufen. 
Er darf nicht auf das Dateisystem, nicht auf das Netzwerk und vor allem nicht auf eine echte Datenbank zugreifen!
Warum? Weil der Test dann fehlschlägt, wenn das WLAN ausfällt – obwohl Ihr Code vielleicht völlig korrekt ist!

## Das Problem: Fachlogik mit Infrastruktur
Angenommen, wir testen einen `RegistrierungsService`, der eine echte E-Mail sendet und in die DB speichert.
Jedes Mal, wenn wir die Test-Suite ausführen (hunderte Male am Tag), würden wir E-Mails versenden und Mülldaten in die DB schreiben. Das geht nicht!

## Die Lösung: Mocking (Attrappen)
Wir erinnern uns an Woche 4 (Dependency Injection). Weil wir Interfaces nutzen, können wir für unsere Tests einfach Fake-Klassen (Mocks) erstellen, die das Interface bedienen, aber intern nichts Reales tun.

## Das Service-Beispiel (Code)
Dieser Service soll getestet werden:

```csharp
public class RegistrierungsService
{
    private readonly IDatenbank _db; // Wird via DI injiziert!
    
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
TDD ist ein iterativer Zyklus, der in drei strengen Phasen abläuft:
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

# Teil 5: Refactoring

## Was ist Refactoring?
Refactoring ist der Prozess, die interne Struktur von Code zu verbessern, ohne sein beobachtbares äußeres Verhalten zu verändern.
Es ist das Aufräumen der "Küche", während wir das Menü (Software) zubereiten.

## Wann refaktorisieren wir?
- Wenn Code "schlecht riecht" (Code Smells wie riesige Klassen, Duplicate Code).
- Im TDD-Zyklus (Phase 3).
- **Die goldene Regel:** Niemals refaktorisieren, wenn die Unit-Tests rot sind (oder fehlen!). Ohne Tests ist Refactoring blindes Raten.

# Teil 6: Zusammenfassung

## Wrap-up Woche 5
- **Automatisierte Tests** sind das Rückgrat der nachhaltigen Softwareentwicklung.
- **xUnit & AAA:** Strukturierte Herangehensweise zur Überprüfung einzelner Methoden.
- **Mocking:** Die Fähigkeit, I/O-Abhängigkeiten (DB, Netz) aus dem Test auszuklammern.
- **TDD:** Die Methode, Tests als Design-Treiber (Red-Green-Refactor) einzusetzen.

## Ausblick auf nächste Woche
In der finalen Theorie-Woche (Woche 6) betrachten wir asynchrone Programmierung (`async`/`await`) für moderne, vernetzte Fachverfahren und besprechen die Anforderungen an Ihre Abschlussprojekte!
