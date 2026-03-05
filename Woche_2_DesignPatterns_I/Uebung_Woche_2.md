# Übungsaufgabe Woche 2: Factory & Adapter in der Verwaltung

## Aufgabe 1: Factory Method - Dokumentenservice
Sie entwickeln ein System zur automatisierten Erstellung von Dokumenten. Es gibt drei Arten von Dokumenten: `Antrag`, `Bescheid` und `Rechnung`.

**Anforderungen:**
1.  Erstellen Sie eine abstrakte Basisklasse `Dokument` mit einer Methode `Drucken()`.
2.  Implementieren Sie die drei konkreten Dokumentklassen.
3.  Erstellen Sie eine Fabrikklasse (Factory), die je nach Parameter (z.B. Enum `DokumentTyp`) das passende Dokumenten-Objekt erzeugt.

## Aufgabe 2: Adapter - Legacy Register
Ihre Anwendung benötigt Bürgerdaten von einem alten Mainframe-System (`LegacySystem`). Das Legacy-System liefert Daten als ein einziges langes `string`-Feld mit Pipe-Symbolen (`|`) als Trenner (z.B. `123|Max|Müller|Berlin`).

**Anforderungen:**
1.  Die Schnittstelle `IBuergerService` erwartet eine Methode `GetBuergerName(int id)`.
2.  Implementieren Sie einen Adapter, der das `LegacySystem` nutzt, um die Daten abzurufen, zu parsen und im gewünschten Format zurückzugeben.

### Tipps:
- Nutzen Sie `string.Split('|')` zum Parsen der Legacy-Daten.
- Trennen Sie die Adapter-Logik sauber von der restlichen Business-Logik.
