% OOP Vertiefung: Woche 1
% Refresher & Clean Code
% Dr. Peter Bernhardt

# Einleitung & Motivation

## Willkommen zum Vertiefungsfach!
- Ziel: Vom "Coder" zum "Software-Ingenieur"
- Fokus: Qualität, Wartbarkeit, Architektur
- Warum C#? Industriestandard in der Verwaltung (E-Government)

## Warum sind wir hier?
- "It works" reicht nicht mehr aus
- Software in der Verwaltung hält Jahrzehnte
- Wer wartet Ihren Code in 10 Jahren?
- Kostentreiber Nr. 1: Wartung (Technical Debt)

## Agenda für heute
1. OOP-Refresher: Klassen, Objekte, Kapselung
2. Clean Code: Die Kunst der Lesbarkeit
3. Die Macht der Namensgebung
4. Funktionen & Methoden richtig designen
5. Refactoring in der Praxis

# Der Mindset-Shift

## Prozedural vs. Objektorientiert
- **Prozedural (C, Python-Skripte):**
    - Abfolge von Befehlen (Rezepte)
    - Daten und Logik sind getrennt
- **Objektorientiert (C#, Java):**
    - Akteure interagieren (Objekte)
    - Daten und Logik sind vereint (Kapselung)

## Beispiel: Prozeduraler Bescheid
```csharp
string bName = "Müller";
double betrag = 100.0;
PrintBescheid(bName, betrag);
```
- Problem: Daten fließen lose durch das System.

## Beispiel: Objektorientierter Bescheid
```csharp
var bescheid = new Bescheid("Müller", 100.0);
bescheid.Drucken();
```
- Vorteil: Der `Bescheid` weiß selbst, wie er gedruckt wird.

# C# Refresher

## Klassen & Objekte
- **Klasse:** Der Bauplan (Blueprint)
    - `public class Buerger { ... }`
- **Objekt:** Die Instanz (Konkretisierung)
    - `var hans = new Buerger();`

## Properties (Eigenschaften)
- Nutzen Sie `get; set;` statt öffentlicher Felder
- Schützen Sie Ihre Daten!
```csharp
public string SteuerId { get; private set; } 
```
- Nur die Klasse selbst darf die ID ändern.

## Kapselung (Information Hiding)
- "Need to know" Prinzip
- Nutzen Sie Access Modifiers:
    - `public`: Überall sichtbar
    - `private`: Nur in der eigenen Klasse
    - `protected`: Für Erben sichtbar
    - `internal`: Im eigenen Assembly

## Konstruktoren
- Garantieren einen gültigen Zustand
- Beispiel: Ein Bürger ohne Namen darf nicht existieren
```csharp
public Buerger(string name) {
    if(string.IsNullOrEmpty(name)) throw new Exception();
    Name = name;
}
```

# Clean Code Grundlagen

## Was ist Clean Code?
- Begriff von Robert C. Martin ("Uncle Bob")
- Code, der sich wie eine Erzählung liest
- Minimierung von "WTFs per minute" beim Code-Review

## Das "Broken Window" Prinzip
- Ein schlecht geschriebener Teil lädt dazu ein, mehr schlechten Code hinzuzufügen
- Clean Code hält die Moral im Team hoch!

# Die Macht der Namen

## Variablen: Präzision vor Kürze
- Schlecht: `int d;` // Tage? Dauer?
- Gut: `int tageSeitAntragseingang;`
- In der Verwaltung: Keine Angst vor langen Fachbegriffen!

## Kontext ist alles
- Schlecht: `var addr = "Berlin";`
- Gut: `var wohnortDesAntragstellers = "Berlin";`
- Vermeiden Sie "Encoding" (z.B. `sName` für String-Name)

## Methoden: Verben nutzen!
- Methoden tun etwas!
- Schlecht: `void Rechnung();`
- Gut: `void BerechneRechnungsbetrag();`
- Gut: `bool IstWahlberechtigt();`

## Klassen: Nomen nutzen!
- Klassen sind Dinge oder Konzepte.
- Schlecht: `class DoRegistration`
- Gut: `class RegistrierungsService`
- Gut: `class MeldedatenEintrag`

# Funktionen & Methoden

## Die "Small"-Regel
- Funktionen sollten klein sein.
- Wie klein? Passt auf eine Seite (oder weniger).
- Ziel: 5 bis 15 Zeilen Code.

## Do One Thing (SRP)
- Eine Methode hat genau EINE Aufgabe.
- "Wenn Sie ein 'And' im Methodennamen brauchen, ist sie zu groß."
- Beispiel: `SpeichereDatenUndSendeEmail()` -> **Trennen!**

## Abstraktionsebenen
- Mischen Sie nicht High-Level ("Anspruch prüfen") mit Low-Level ("Datenbank-String escapen").
- Nutzen Sie Hilfsmethoden für Details.

## Funktions-Argumente
- 0 Argumente: Ideal
- 1-2 Argumente: Gut
- 3 Argumente: Grenzwertig
- \>3 Argumente: Nutzen Sie ein Objekt/Klasse als Parameter!

# Anti-Patterns vermeiden

## Magic Numbers
- Schlecht: `if (status == 12)`
- Wer weiß in 3 Monaten noch, was `12` ist?
- Lösung: **Enums** nutzen!

## Enums in C#
```csharp
public enum AntragsStatus {
    Offen, InBearbeitung, Genehmigt, Abgelehnt
}
```
- Viel lesbarer: `if (antrag.Status == AntragsStatus.Genehmigt)`

## Magic Strings
- Vermeiden Sie `"Admin"`, `"User"` etc. lose im Code.
- Nutzen Sie Konstanten: `public const string ROLE_ADMIN = "Admin";`

# Kommentare & Formatierung

## Kommentare vermeiden?
- "Jeder Kommentar ist ein Eingeständnis, dass der Code nicht klar genug war."
- Erklären Sie das **Warum**, nicht das **Was**.
- `i++; // i erhöhen` -> **Löschen!**

## Sinnvolle Kommentare
- Warnungen: `// Achtung: API antwortet sehr langsam`
- Rechtliche Hinweise (Lizenz)
- TODOs (für kurze Zeit)

## Formatierung
- Nutzen Sie Tools! (Visual Studio: Ctrl+K, Ctrl+D)
- Einrückung ist nicht optional für die Lesbarkeit.
- Team-Standards (Microsoft Conventions) sind Gesetz.

# Software-Prinzipien

## DRY - Don't Repeat Yourself
- Kopierter Code ist ein Fehler von morgen (an zwei Stellen!).
- Extrahieren Sie Logik in Methoden oder Basisklassen.

## KISS - Keep It Simple, Stupid
- Keine "Over-Engineering"
- Lösen Sie das Problem, das Sie haben, nicht das, das Sie vielleicht mal haben könnten.

## Die Pfadfinder-Regel (Boy Scout Rule)
- "Hinterlasse den Code immer sauberer, als du ihn vorgefunden hast."
- Kleine Verbesserungen bei jedem Check-in.

# Refactoring & Ausblick

## Was ist Refactoring?
- Struktur verbessern, Verhalten beibehalten.
- Nur möglich, wenn wir wissen, was der Code tut!
- Teil des täglichen Workflows.

## Die heutige Übung
- Analyse des "DirtyLibrarySystem"
- Schrittweise Umwandlung in ein sauberes OO-System
- Fokus: Datentypen, Namensgebung, Trennung der Belange

## Zusammenfassung Woche 1
- Code ist für Menschen, nicht für Maschinen.
- Namen sind wichtig.
- Methoden klein halten.
- OOP-Konzepte konsequent nutzen.

# Vielen Dank!
- Fragen?
- Ab zur Übung!
