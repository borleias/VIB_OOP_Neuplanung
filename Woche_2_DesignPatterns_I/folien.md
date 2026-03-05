---
title: "Objektorientierte Programmierung (Vertiefung)"
subtitle: "Woche 2: Design Patterns I – Erzeugung & Struktur"
author: "Dr. Peter Bernhardt"
date: "März 2026"
section-titles: true
---

# Teil 1: Einführung in Design Patterns

## Was sind Entwurfsmuster?
Entwurfsmuster sind bewährte Lösungen für häufig auftretende Probleme im Software-Design. Sie repräsentieren Best Practices, die über Jahrzehnte von Software-Entwicklern entwickelt wurden. Ein Muster ist keine fertige Implementierung, sondern eine Vorlage, wie ein Problem gelöst werden kann.

In der Verwaltungsinformatik helfen sie uns, komplexe Fachlogik (wie Gesetze und Verordnungen) in flexiblen und wartbaren Code zu übersetzen. Wir nutzen sie, um das Rad nicht jedes Mal neu erfinden zu müssen.

## Kategorien von Patterns
Die "Gang of Four" (GoF), die Pioniere der Design Patterns, unterteilten sie in drei Hauptgruppen:

1. **Erzeugungsmuster (Creational):** Mechanismen zur Objekterstellung, die die Komplexität der Instanziierung verbergen.
2. **Strukturmuster (Structural):** Erleichtern das Design durch das Identifizieren einfacher Wege, um Beziehungen zwischen Einheiten zu realisieren.
3. **Verhaltensmuster (Behavioral):** Identifizieren allgemeine Kommunikationsmuster zwischen Objekten.

Heute fokussieren wir uns auf die ersten beiden Kategorien.

# Teil 2: Singleton (Erzeugungsmuster)

## Das Singleton-Prinzip
Das Singleton-Muster stellt sicher, dass eine Klasse nur eine einzige Instanz hat und bietet einen globalen Zugriffspunkt auf diese Instanz.

**Warum brauchen wir das?**
In vielen Systemen gibt es Ressourcen, die nur einmal existieren sollten:
- Ein zentraler Konfigurationsmanager.
- Ein Log-Writer, der in eine einzige Datei schreibt.
- Ein Verbindungspool zu einer Datenbank.

Mehrere Instanzen würden hier zu Inkonsistenzen oder unnötigem Ressourcenverbrauch führen.

## Implementierung des Singletons
In C# nutzen wir einen privaten Konstruktor und eine statische Eigenschaft.

```csharp
public sealed class ConfigManager {
    private static ConfigManager _instance;
    private static readonly object _lock = new object();

    private ConfigManager() {} // Verhindert 'new'

    public static ConfigManager Instance {
        get {
            lock(_lock) {
                if (_instance == null) _instance = new ConfigManager();
                return _instance;
            }
        }
    }
}
```
Die `lock`-Anweisung sorgt dafür, dass das Muster auch in Multi-Threading-Umgebungen (z.B. Web-Servern) sicher funktioniert.

# Teil 3: Factory Method (Erzeugungsmuster)

## Das Problem: Starre Objekterstellung
Wenn wir in unserem Code direkt `new BewilligungsBescheid()` schreiben, binden wir uns fest an diese konkrete Klasse. Wenn später ein neuer Bescheid-Typ (z.B. "Vorläufiger Bescheid") hinzukommt, müssen wir den Code an vielen Stellen ändern.

Die Factory Method löst dies, indem sie die Erstellungslogik in eine eigene Methode auslagert.

## Anwendung: Bescheid-Erstellung
Ein Sachbearbeiter-System weiß, *dass* ein Bescheid erstellt werden muss, aber erst zur Laufzeit wird entschieden, *welcher* Typ es ist (basierend auf der Antragsprüfung).

- **Interface:** `IBescheid`
- **Konkrete Klassen:** `Zulassung`, `Ablehnung`
- **Factory:** `BescheidCreator` entscheidet basierend auf Logik, welches Objekt zurückgegeben wird.

Dies fördert die lose Kopplung: Der aufrufende Code arbeitet nur mit dem Interface `IBescheid`.

# Teil 4: Adapter (Strukturmuster)

## Die Brücke zwischen Alt und Neu
In der Verwaltung arbeiten wir oft mit Legacy-Systemen (Großrechner, alte Datenbanken). Diese haben oft Schnittstellen, die nicht zu modernen Standards passen.

Der Adapter fungiert als "Übersetzer":
- Er implementiert ein modernes Interface, das unsere Anwendung erwartet.
- Intern ruft er die kryptischen Funktionen des alten Systems auf.
- Er konvertiert die Datenformate (z.B. von COBOL-Strings zu C#-Objekten).

## Beispiel: Melderegister-Anbindung
Unsere moderne Web-App erwartet ein `IPersonenService`. Das vorhandene Melderegister liefert aber nur Rohdaten über eine veraltete DLL-Schnittstelle.

Der `MelderegisterAdapter` nimmt die Anfrage entgegen, ruft die DLL auf, parst den zurückgegebenen Text-String und gibt ein sauberes `Buerger`-Objekt an die Web-App zurück. Die Web-App "merkt" nicht einmal, dass sie mit einem 30 Jahre alten System spricht.

# Teil 5: Decorator (Strukturmuster)

## Dynamische Erweiterung statt Vererbung
Vererbung führt oft zu einer "Klassen-Explosion". Wenn wir einen `Antrag` haben und Optionen wie `Express`, `Einschreiben` und `International` kombinieren wollen, bräuchten wir Klassen für jede Kombination: `ExpressEinschreibenAntrag`, `InternationalExpressAntrag` etc.

Der Decorator erlaubt es uns, Funktionalität zur Laufzeit "um ein Objekt herumzuwickeln".

## Beispiel: Antrags-Zusatzleistungen
1. Wir starten mit einem `BasisAntrag`.
2. Wir wickeln einen `ExpressDecorator` darum (erhöht Kosten).
3. Wir wickeln einen `VersandDecorator` darum (fügt Versandinfo hinzu).

Jeder Decorator implementiert das gleiche Interface wie das Basis-Objekt und delegiert die Arbeit an das eingewickelte Objekt weiter, wobei er eigenes Verhalten hinzufügt.

# Zusammenfassung

## Key Takeaways
- **Singleton:** Zentrale Instanz für globale Ressourcen (Konfiguration).
- **Factory Method:** Flexibilität bei der Erstellung von Objekten (Bescheide).
- **Adapter:** Kompatibilität mit Altsystemen (Zentralregister).
- **Decorator:** Flexible Erweiterung von Objekten ohne Klassen-Explosion (Zusatzleistungen).

Design Patterns sind Werkzeuge, kein Selbstzweck. Setzen Sie sie dort ein, wo sie die Wartbarkeit erhöhen und Komplexität reduzieren.
