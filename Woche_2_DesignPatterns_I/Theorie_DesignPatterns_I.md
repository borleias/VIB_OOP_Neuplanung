# Theorie: Design Patterns I (Erzeugung & Struktur)

## 1. Was sind Design Patterns?
- Von der "Gang of Four" (GoF) formuliert.
- Erprobte Lösungen für Standardprobleme in der OOP.
- Kategorien: **Erzeugungsmuster** (Creational), **Strukturmuster** (Structural), **Verhaltensmuster** (Behavioral).

## 2. Singleton (Erzeugung)
- **Problem:** Es darf nur eine Instanz einer Klasse im gesamten System geben (z.B. Konfigurationsmanager).
- **Lösung:** Privater Konstruktor und statische `Instance`-Eigenschaft.
- **Kritik:** Erschwert Unit Testing und kann globalen State einführen (Anti-Pattern-Gefahr!).

## 3. Factory Method (Erzeugung)
- **Problem:** Die genaue Klasse eines zu erzeugenden Objekts soll nicht im Client-Code festgeschrieben sein.
- **Lösung:** Auslagerung der Erzeugung in eine spezialisierte Methode oder Klasse.
- **Vorteil:** Leichte Erweiterbarkeit durch neue Typen, ohne den Client zu ändern (Open-Closed Principle).

## 4. Adapter (Struktur)
- **Problem:** Zwei Klassen haben inkompatible Schnittstellen, müssen aber zusammenarbeiten (z.B. Einbindung einer Drittanbieter-Bibliothek).
- **Lösung:** Eine Zwischenklasse (der Adapter), welche die Aufrufe "übersetzt".
- **Prinzip:** "Wrapper" um das inkompatible Objekt.

## 5. Decorator (Struktur)
- **Problem:** Verhalten eines Objekts soll zur Laufzeit erweitert werden, ohne die Vererbungshierarchie aufzublähen.
- **Lösung:** Das Objekt wird in ein anderes Objekt "eingepackt", das dieselbe Schnittstelle hat.
