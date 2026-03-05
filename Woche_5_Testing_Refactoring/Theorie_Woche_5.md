# Woche 5: Qualitätssicherung – Testing und Refactoring

Bisher haben wir Code geschrieben und darauf vertraut, dass er funktioniert ("Manuelle Prüfung"). Aber was passiert, wenn wir in drei Monaten eine kleine Änderung machen? Wie stellen wir sicher, dass wir dabei nichts kaputt gemacht haben, was vorher funktioniert hat? Hier kommt das **automatisierte Testen** ins Spiel.

In dieser Woche lernen wir, wie wir unseren Code mit **xUnit** absichern, wie wir durch **Mocking** Abhängigkeiten isolieren und wie wir den **TDD-Zyklus** (Test-Driven Development) anwenden.

---

## 1. Unit Testing mit xUnit

Ein **Unit Test** (Einheitentest) prüft die kleinste testbare Einheit einer Anwendung – meist eine einzelne Methode einer Klasse – in völliger Isolation.

### Die drei Phasen eines Tests: AAA
1.  **Arrange (Vorbereiten):** Wir erstellen das Testobjekt und die nötigen Testdaten.
2.  **Act (Ausführen):** Wir rufen die zu testende Methode auf.
3.  **Assert (Prüfen):** Wir vergleichen das Ergebnis mit unserer Erwartung.

### Beispiel: Validierung einer Steuer-ID
```csharp
using Xunit;

public class SteuerIdValidatorTests
{
    [Fact] // Markiert eine Testmethode
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

    [Theory] // Markiert einen Test mit mehreren Datensätzen
    [InlineData("123")] // Zu kurz
    [InlineData("123456789012")] // Zu lang
    [InlineData("ABC45678901")] // Buchstaben
    public void Validieren_SollteFalseZurueckgeben_WennIdUngueltigIst(string ungueltigeId)
    {
        var validator = new SteuerIdValidator();
        bool result = validator.Validieren(ungueltigeId);
        Assert.False(result);
    }
}
```

---

## 2. Warum Interfaces für Tests entscheidend sind (Mocking)

Stellen Sie sich vor, Sie wollen den `RegistrierungsService` testen. Dieser Service speichert Daten in einer echten Datenbank und sendet eine echte E-Mail.
*   **Problem:** Wenn die Datenbank down ist, schlägt Ihr Test fehl, obwohl Ihr Code korrekt ist. Der Test ist langsam und hinterlässt "Müll" in der DB.

### Die Lösung: Mocks
Wir nutzen ein **Interface** für die Datenbank und übergeben dem Service im Test eine "Attrappe" (Mock). Ein Mock tut so, als wäre er die Datenbank, schreibt aber nichts und liefert sofort vordefinierte Werte zurück.

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

Im Test können wir nun eine Klasse `DatenbankMock` erstellen, die `IDatenbank` implementiert, aber intern nur zählt, wie oft `Speichern` aufgerufen wurde.

---

## 3. Der TDD-Zyklus (Red - Green - Refactor)

**Test-Driven Development** bedeutet: Wir schreiben den Test, **bevor** wir den eigentlichen Code schreiben.

1.  **Red (Rot):** Schreiben Sie einen Test für eine kleine Funktion, die es noch nicht gibt. Lassen Sie den Test laufen -> Er muss fehlschlagen (da der Code fehlt).
2.  **Green (Grün):** Schreiben Sie gerade so viel Code, dass der Test besteht. Keine "schöne" Lösung, nur eine funktionale.
3.  **Refactor (Aufräumen):** Jetzt machen Sie den Code schön. Verbessern Sie die Benennung, entfernen Sie Duplikate. Die Tests geben Ihnen die Sicherheit, dass dabei nichts kaputt geht.

---

## 4. Test-Lifecycle und Best Practices

*   **Isolation:** Ein Test darf niemals von einem anderen Test abhängen. Die Reihenfolge der Ausführung muss egal sein.
*   **Geschwindigkeit:** Unit Tests müssen in Millisekunden laufen. Wenn sie zu langsam sind, werden sie nicht ausgeführt.
*   **Benennung:** Ein Testname sollte genau sagen, was geprüft wird (z.B. `Methode_Szenario_ErwartetesVerhalten`).
*   **Keine Logik im Test:** Ein Test sollte keine `if`-Abfragen oder Schleifen enthalten. Er sollte so einfach sein, dass er selbst keine Fehler enthalten kann.

---

## 5. Refactoring: Den Code "atmen" lassen

Refactoring ist die Verbesserung der inneren Struktur ohne Änderung des äußeren Verhaltens.
*   **Wann?** Immer wenn Sie den Code im "Green"-Zustand haben.
*   **Warum?** Um technische Schulden abzubauen.
*   **Sicherheit:** Ohne automatisierte Tests ist Refactoring wie Seiltanz ohne Netz – lebensgefährlich für das Projekt.

---

## Zusammenfassung
*   **Unit Tests:** Sichern die Basis ab und dienen als lebendige Dokumentation.
*   **xUnit:** Das Standard-Framework für .NET.
*   **Mocking:** Isoliert den Test von langsamen oder unzuverlässigen externen Systemen (DB, API).
*   **TDD:** Ein Workflow, der zu besserem Design und 100% Testabdeckung führt.
