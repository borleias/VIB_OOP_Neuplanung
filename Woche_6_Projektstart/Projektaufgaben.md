# Projektaufgabe

## Einleitung

In der Praxis-Phase haben Sie nun Zeit, ein eigenes Fachverfahren zu entwickeln.
Es geht nicht darum, ein riesiges System mit hunderten Funktionen zu bauen. Es geht darum, ein **kleines System architektonisch möglichst gut** zu konstruieren.

## Projektvorschläge

- Wohngeld-Prüfmodul
- Kfz-Bestandsverwaltung
- Kita-Priorisierung
- Bürgeramt-Terminreservierung
- Fundbüro-Bestandsführung
- Bauantrags-Statusverfolgung
- Hundesteuer-Veranlagung
- Mängelmelder-System
- Gewerberegister-Eintragung
- Friedhofs-Grabverwaltung

## Architektur-Anforderungen

Ihr Projekt muss folgende Kriterien erfüllen:

1. **Clean Code** berücksichtigen
2. **Schichtenarchitektur:** UI, Business Logic und Data Access Layer strikt getrennt.
3. **SOLID:** Bewusste Anwendung der Prinzipien (insb. DI).
4. **Design Patterns:** Mindestens zwei Muster implementiert.
5. **Testing:** Kernlogik mit Unit Tests (xUnit) abgesichert. (wenn möglich)
6. **Dokumentation des Projekts** Ein kurzes README, das Ihre Design-Entscheidungen (Welche Patterns? Warum diese Architektur?) erläutert.

## Vorstellung Ihrer Ergebnisse

In der letzten Woche werden die Ergebnisse per Vorträge (jeweils 20 Min. zzgl. Q&A) vorgestellt.

1. **Ihren Code souverän präsentieren:** Den roten Faden der Software-Struktur erläutern.
2. **Architektur-Entscheidungen begründen:** Warum wurden bestimmte Design-Entscheidungen getroffen?
3. **Kritisches Feedback geben und annehmen:** Peer-Review in der Gruppe durchführen.

## Bewertungskriterien

- **Struktur (20%):** Korrekte Schichtentrennung und Interface-Nutzung.
- **Patterns (20%):** Sinnvoller Einsatz von Design Patterns.
- **Tests (20%):** Aussagekräftige Unit Tests für die Business-Logik.
- **Funktionalität (20%):** Das Kernfeature läuft fehlerfrei.
- **Dokumentation (20%):** Auskunftsfähigkeit über das Projekt und die getroffenen Entscheidungen

## Vorgeschlagener Workflow

- Überlegen Sie sich Ihr Thema und definieren Sie das Kern-Feature.
- Skizzieren Sie die Klassenstruktur (Welche Schichten? Welche Patterns?).
- Die vorgeschlagenen Patterns können oft durch weitere sinnvoll ergänzt werden!
- Starten Sie mit der Fachlogik und schreiben Sie Tests (TDD!).
- Bauen Sie die UI und die Datenbank-Anbindung erst ganz am Schluss um Ihre Logik herum.

## Projektvorschläge im Detail

### 1. Wohngeld-Prüfmodul

**Fachdomäne:** Das Wohngeld ist eine Sozialleistung zur Sicherung angemessenen Wohnens. Die Verwaltung muss hierbei strikt nach gesetzlichen Tabellen prüfen, ob das Einkommen eines Haushalts im Verhältnis zur Miete und Personenzahl zu hoch ist. In dieser Domäne ist die **Rechtssicherheit** entscheidend: Jeder Bürger muss bei gleichen Voraussetzungen das gleiche Ergebnis erhalten.

- **Use Case 1 (Bürger):** Stellen eines Wohngeldantrages. (Ziel: Wahrnehmen eines ggf. gegegebenen Anspruchs).
- **Use Case 2 (Sachbearbeiter):** Festsetzen des monatlichen Wohngeldbetrages. (Ziel: Rechtsverbindliche Leistungsfeststellung).
- **Use Case 3 (System):** Automatisches Ausgeben eines schriftlichen Bescheids nach der Festsetzung. (Ziel: Erfüllung der Informationspflicht gegenüber dem Bürger).

**Pattern-Einsatz:** Das **Strategy Pattern** wird genutzt, um verschiedene Berechnungsvarianten (z.B. "Mietstufe I" vs. "Mietstufe VI") austauschbar zu machen, ohne den Rechenkern zu verändern. Setzen Sie die **Factory Method** ein, um basierend auf dem Ergebnis den passenden Bescheid-Typ zu erstellen.

**Architektur:** Trennen Sie die Berechnungslogik strikt von der Dateneingabe (Layered Architecture). Achten Sie auf das *Single Responsibility Principle (SRP)*: Eine Klasse berechnet, eine andere formatiert den Bescheid.

**Datenmodell:**

- **Stammdaten:** Mietstufen-Tabelle (6 Stufen mit jeweils max. förderfähiger Miete), Einkommensgrenzen-Tabelle (nach Haushaltsgröße 1-5 Personen).
- **Bewegungsdaten:** Bürger-Datensatz (Name, Adresse), Haushalts-Datensatz (Anzahl Personen, Brutto-Einkommen, Kaltmiete).
- **Menge:** Simuliere 3 verschiedene Haushalte und die gesetzliche Mietstufen-Tabelle (ca. 10-12 Datensätze).

### 2. Kfz-Bestandsverwaltung

**Fachdomäne:** Die Zulassungsbehörde führt das Fahrzeugregister. Dieses dient der Verkehrssicherheit und der Steuererhebung. Ein Fahrzeug durchläuft einen Lebenszyklus von der Erstzulassung bis zur Außerbetriebsetzung. Die Integrität des Registers ist von höchster Bedeutung für die Polizei und Versicherungen.

- **Use Case 1 (Bürger):** Anmelden eines Neufahrzeugs. (Ziel: Erhalt der Betriebserlaubnis für den öffentlichen Raum).
- **Use Case 2 (Sachbearbeiter):** Reservieren eines Wunschkennzeichens. (Ziel: Sicherstellung der Einmaligkeit einer Kennung).
- **Use Case 3 (Bürger):** Stilllegen eines Fahrzeugs. (Ziel: Beendigung der Steuer- und Versicherungspflicht).

**Pattern-Einsatz:** Ein **Singleton** bietet sich für die zentrale Verwaltung des Kennzeichen-Pools an. Das **State Pattern** bildet den Lebenszyklus ab (Zugelassen, Abgemeldet). Bestimmte Aktionen (wie Kennzeichen-Wechsel) sind nur in bestimmten Zuständen erlaubt.

**Architektur:** Nutzen Sie Dependency Injection, um den Kennzeichen-Service in die Zulassungslogik einzuspielen. Dies erleichtert das Testen (Mocking).

**Datenmodell:**

- **Stammdaten:** Kennzeichen-Pool (Liste aller verfügbaren Kombinationen, z.B. B-AA 100 bis B-ZZ 999).
- **Bewegungsdaten:** Fahrzeug-Akte (Fahrzeugidentifikationsnummer VIN, Marke, Modell, aktueller Status, Kennzeichen, Halter-ID).
- **Menge:** Ein Pool von ca. 20 Kennzeichen und 5-10 zugelassenen Fahrzeugen.

### 3. Kita-Priorisierung

**Fachdomäne:** Kita-Plätze sind eine knappe Ressource der kommunalen Daseinsvorsorge. Die Verteilung muss nach **objektiven Fairneß-Kriterien** erfolgen. Die Verwaltung muss hierbei soziale Faktoren (Berufstätigkeit, Alleinerziehend, Geschwisterkinder) gewichten, um Diskriminierung zu vermeiden und den Rechtsanspruch auf einen Betreuungsplatz zu erfüllen.

- **Use Case 1 (Eltern):** Einreichen einer Bedarfsanmeldung. (Ziel: Aufnahme in das Verteilungssystem).
- **Use Case 2 (Sachbearbeiter):** Ermitteln der Prioritäts-Punktzahl. (Ziel: Objektive Einstufung der Dringlichkeit).
- **Use Case 3 (System):** Automatische Zuweisung eines freien Platzes / Wartelistenplatzes. (Ziel: Optimale Auslastung der Kapazitäten).

**Pattern-Einsatz:** Das **Strategy Pattern** hilft unterschiedliche Punkte-Vergabe-Algorithmen (z.B. Sozialpunkte vs. Geschwister-Bonus) zu kapseln. Das **Observer Pattern** kann Eltern automatisch informieren, wenn sich der Status ihrer Bewerbung ändert.

**Architektur:** Halten Sie die Vergabelogik unabhängig von der Benutzeroberfläche (UI), um den Algorithmus automatisiert mit Unit-Tests validieren zu können.

**Datenmodell:**

- **Stammdaten:** Kita-Stamm (Name, Kapazität, Altersgruppe U3/Ü3), Kriterien-Katalog (Punkte für Berufstätigkeit, Alleinerziehend etc.).
- **Bewegungsdaten:** Kind-Datensatz (Name, Geburtsdatum, Geschwister-ID), Bewerbung (Priorisierte Liste von 3 Kitas, Status der Zuweisung).
- **Menge:** 2 Kitas mit je 5 Plätzen und eine Warteliste von 5 Kindern.

### 4. Bürgeramt-Terminreservierung

**Fachdomäne:** Moderne Verwaltung versteht sich als Dienstleister. Terminbuchungssysteme verhindern lange Wartezeiten und erlauben eine effiziente Personalplanung. Die Domäne befasst sich mit der Verwaltung von Zeit-Ressourcen und der Vermeidung von Doppelbuchungen.

- **Use Case 1 (Bürger):** Buchen eines freien Terminfensters. (Ziel: Sicherstellung einer persönlichen Beratung).
- **Use Case 2 (Sachbearbeiter):** Bestätigung der Abarbeitung eines Termins. (Ziel: Übersicht über das Arbeitsaufkommen).
- **Use Case 3 (System):** Kontinuierliches Aktualisieren einer Infotafel mit dem Buchungsstand. (Ziel: Effektive Nutzung der Ressource Zeit).

**Pattern-Einsatz:** Nutzen Sie die **Factory Method**, um Termin-Objekte mit den jeweils korrekten Zeitintervallen für die gewählte Dienstleistung zu erzeugen. Ein **Singleton** kann den Zugriff auf den globalen Kalender-Ressourcen-Pool steuern. Das **Observer Pattern** informiert andere Module (z.B. das Statistik-Modul oder die Infotafel), wenn ein Termin gebucht oder storniert wurde.

**Architektur:** Befolgen Sie das *Open-Closed Principle (OCP)*: Neue Dienstleistungstypen sollten hinzugefügt werden können, ohne den bestehenden Buchungscode zu ändern.

**Datenmodell:**

- **Stammdaten:** Dienstleistungs-Katalog (Name der Leistung, Dauer in Minuten, z.B. "Pass beantragen" = 20 Min).
- **Bewegungsdaten:** Terminslot (Datum, Uhrzeit, Status gebucht/frei), Buchungs-Datensatz (Bürgername, gewählte Leistung, Buchungscode).
- **Menge:** Ein Arbeitstag mit Slots alle 15 Minuten von 08:00 bis 16:00 Uhr. 5 Dienstleistungen unterschiedlicher Dauer. Zwei Sachbearbeiter.

### 5. Fundbüro-Bestandsführung

**Fachdomäne:** Das Fundrecht (§§ 965-984 BGB) regelt die Pflichten des Finders und der Behörde. Die Verwaltung muss Fundsachen sicher verwahren, dokumentieren und versuchen, den Eigentümer zu ermitteln. Nach Ablauf einer Frist (6 Monate) geht das Eigentum ggf. auf den Finder oder die Kommune über.

- **Use Case 1 (Finder):** Anzeigen eines Fundes. (Ziel: Erfüllung der gesetzlichen Meldepflicht).
- **Use Case 2 (Verlierer):** Suchen nach einem verlorenen Gegenstand. (Ziel: Wiedererlangung des Eigentums).
- **Use Case 3 (System):** Automatische Benachrichtigung des Sachbearbeiters, wenn die Frist bei mindestens fünf Fundstücken abgelaufen ist. (Ziel: Rechtssicherer Abschluss des Fundvorgangs).

**Pattern-Einsatz:** Das **Strategy Pattern** erlaubt verschiedene Such-Strategien (z.B. "Exakter Abgleich der Seriennummer" vs. "Grobe Kategorien-Suche"). Der **Decorator** erlaubt es, Fundstücke dynamisch mit Zusatzinformationen (z.B. "Beschädigt", "Wertvoll", "Versteigert") zu versehen.

**Architektur:** Implementieren Sie eine klare Service-Schicht für das Matching, die unabhängig von der Datenbank-Technologie arbeitet.

**Datenmodell:**

- **Stammdaten:** Kategorien-Baum (Elektronik, Kleidung, Dokumente, Wertsachen).
- **Bewegungsdaten:** Fund-Objekt (Beschreibung, Funddatum, Fundort, Aufbewahrungsort im Lager, Status: im Lager/zurückgegeben/versteigert).
- **Menge:** Ein Lagerbestand von ca. 25 Fundstücken mit unterschiedlichen Fristen.

### 6. Bauantrags-Statusverfolgung

**Fachdomäne:** Das Baugenehmigungsverfahren stellt sicher, dass bauliche Anlagen den öffentlich-rechtlichen Vorschriften (Brandschutz, Statik) entsprechen. Es ist ein hoheitlicher Prozess mit klaren rechtlichen Stufen. Transparenz über den Bearbeitungsstand ist für Bauherren (Investitionssicherheit) essenziell.

- **Use Case 1 (Architekt):** Einreichen der Bauunterlagen. (Ziel: Start des Genehmigungsverfahrens).
- **Use Case 2 (Fachabteilung):** Dokumentieren eines Prüfungsschritts (z.B. Brandschutz). (Ziel: Fortschritt im Workflow).
- **Use Case 3 (Bauherr):** Information über Änderungen des Antragsstatus. (Ziel: Planungssicherheit für den Baustart).

**Pattern-Einsatz:** Das **State Pattern** stellt sicher, dass ein Bescheid erst erstellt werden kann, wenn alle Prüf-Zustände (Brandschutz, Statik) positiv durchlaufen wurden. Das **Observer Pattern** benachrichtigt den Bauherr, sobald ein Antrag den Zuständigkeitsbereich wechselt.

**Architektur:** Nutzen Sie das Interface-Segregation-Principle, um den Fachabteilungen nur die für sie relevanten Methoden des Antrags anzuzeigen.

**Datenmodell:**

- **Stammdaten:** Liste der Fachabteilungen (Brandschutz, Statik, Denkmalschutz).
- **Bewegungsdaten:** Bauantrag (ID, Bauherr, Flurstücksnummer, Beschreibung, Liste der abgeschlossenen Teil-Prüfungen, Gesamtstatus). Teil-Prüfung (Fachabteilung, Ergebnis).
- **Menge:** 3 Prüfungsphasen. 10 laufende Bauanträge in unterschiedlichen Prüfungsphasen.

### 7. Hundesteuer-Veranlagung

**Fachdomäne:** Die Hundesteuer ist eine örtliche Aufwandsteuer. Sie dient der Finanzierung kommunaler Aufgaben und der Lenkung (Eindämmung der Hundehaltung, insbesondere gefährlicher Rassen). Die Verwaltung muss hierbei unterschiedliche Steuersätze und Befreiungsgründe rechtssicher anwenden.

- **Use Case 1 (Halter):** Anmelden eines neuen Hundes. (Ziel: Erfüllung der steuerlichen Meldepflicht).
- **Use Case 2 (Sachbearbeiter):** Festsetzen der jährlichen Steuerlast. (Ziel: Erstellung der Einnahmenbasis).
- **Use Case 3 (System):** Sich aktualisierende Übersicht über die jährlichen Steuereinnahmen erstellen. (Ziel: Finanziellen Überblick verschaffen).

**Pattern-Einsatz:** Das **Strategy Pattern** kapselt die Steuertarife (Normal, Kampfhund, Befreit), sodass diese bei Satzungsänderungen leicht getauscht werden können. Eine **Factory** erstellt die jährlichen Steuerbescheide als PDF-Repräsentation.

**Architektur:** Trennen Sie die "Halter"-Entität strikt von der "Steuerberechnungs"-Logik (Low Coupling).

**Datenmodell:**

- **Stammdaten:** Steuersatz-Tabelle (Ersthund: 120€, Zweithund: 240€, Gefährlicher Hund: 600€, Assistenzhund: stuerfrei).
- **Bewegungsdaten:** Halter-Datensatz (Name, Anschrift), Hund-Datensatz (Rasse, Chip-Nummer, Anmeldedatum, Befreiungsstatus).
- **Menge:** 10 Halter mit insgesamt 15 Hunden (inkl. Mehrhundehaushalte).

### 8. Mängelmelder-System

**Fachdomäne:** Kommunen haben eine Verkehrssicherungspflicht. Defekte Straßenlaternen oder Schlaglöcher müssen zeitnah behoben werden, um Haftungsansprüche zu vermeiden. Der Mängelmelder stärkt zudem die Bürgerbeteiligung und die Identifikation mit dem Wohnort. Es können jederzeit neue Mängelarten hinzukommen.

- **Use Case 1 (Bürger):** Melden eines Infrastruktur-Mangels. (Ziel: Gefahrenabwehr und Verbesserung des Stadtbildes).
- **Use Case 2 (System):** Benachrichtigung des Bauhofs über eine Reparatur-Aufgabe. (Ziel: Planmäßige Behebung des Schadens).
- **Use Case 3 (Bürger):** Verfolgen der Mängelbehebung. (Ziel: Transparenz über das Verwaltungshandeln).

**Pattern-Einsatz:** Das **Observer Pattern** benachrichtigt den Bauhof automatisch. Der **Decorator** kann Meldungen dynamisch mit Metadaten (GPS-Fix, Foto-Anhang, Dringlichkeits-Flag) erweitern.

**Architektur:** Nutzen Sie eine Schichtenarchitektur, um die API-Logik (Meldungsannahme) von der Business-Logik (Zuweisung) zu trennen.

**Datenmodell:**

- **Stammdaten:** Zuständigkeits-Katalog (Straßenbeleuchtung -> Elektriker, Schlagloch -> Tiefbau).
- **Bewegungsdaten:** Mängel-Meldung (ID, Datum, Beschreibung, Standort, Foto-Link, Status: Neu/In Arbeit/Erledigt).
- **Menge:** 15 offene Mängelberichte in der Stadtverwaltung. 5 Mängelarten.

### 9. Gewerberegister-Eintragung

**Fachdomäne:** Die Gewerbeordnung verlangt eine Anzeige jeder gewerblichen Tätigkeit. Das Gewerberegister dient der Überwachung der Wirtschaft und der Information von Behörden (z.B. Finanzamt). Hierbei ist die korrekte Erfassung der Rechtsform und der Tätigkeit entscheidend.

- **Use Case 1 (Unternehmer):** Anzeigen einer Gewerbeeröffnung. (Ziel: Rechtmäßige Aufnahme der wirtschaftlichen Tätigkeit).
- **Use Case 2 (Gewerbeamt):** Prüfen der Anmeldung auf Plausibilität. (Ziel: Qualitätssicherung des Registers).
- **Use Case 3 (System):** Weiterleiten der Daten an das Finanzamt. (Ziel: Erfüllung der behördlichen Mitteilungspflicht).

**Pattern-Einsatz:** Das **Strategy Pattern** führt unterschiedliche Validierungen je nach Rechtsform aus (z.B. Prüfung des Handelsregistereintrags nur bei juristischen Personen, Startkapital). Das **State Pattern** steuert den Workflow von der Anmeldung über die Prüfung bis zur Weiterleitung.

**Architektur:** Implementieren Sie Validatoren als eigene Klassen (SRP), die gegen Interfaces prüfen, um die Testbarkeit zu erhöhen.

**Datenmodell:**

- **Stammdaten:** Rechtsformen-Verzeichnis (Einzelunternehmen, GmbH, GbR, AG), Behörden-Verteiler (Finanzamt, IHK, HWK).
- **Bewegungsdaten:** Gewerbe-Anmeldung (Name des Betriebs, Tätigkeit, Startdatum, Rechtsform, Steuernummer).
- **Menge:** 15 neue Gewerbe-Anmeldungen zur Bearbeitung.

### 10. Friedhofs-Grabverwaltung

**Fachdomäne:** Friedhöfe sind Orte der Trauer und der Kultur. Die Verwaltung muss Grabstätten über lange Zeiträume (20-30 Jahre) verwalten. Die Überwachung von Ruhefristen ist aus hygienischen und platzwirtschaftlichen Gründen zwingend erforderlich.

- **Use Case 1 (Angehöriger):** Erwerben eines Nutzungsrechts an einer Grabstätte. (Ziel: Vorsorge für einen Bestattungsfall).
- **Use Case 2 (System):** Überwachen des Ablaufs der Ruhefrist für die nächsten 12 Monate. (Ziel: Geordnete Neubelegung von Flächen).
- **Use Case 3 (Angehöriger):** Verlängern der Grabnutzung. (Ziel: Erhalt der Gedenkstätte über die Mindestfrist hinaus).

**Pattern-Einsatz:** Das **Strategy Pattern** berechnet die Laufzeiten je nach Grabart (Urne vs. Sarg), da diese gesetzlich unterschiedlich geregelt sind. Der **Decorator** kann Grabstätten um Zusatzleistungen wie "Dauergrabpflege" oder "Denkmalschutz-Status" erweitern. Das **Observer Pattern** informiert die Verwaltung automatisch, sechs Monate vor dem Ablaufen derRuhefrist.

**Architektur:** Achten Sie auf eine saubere Datenkapselung, insbesondere beim Umgang mit sensiblen Daten von Verstorbenen und Hinterbliebenen.

**Datenmodell & Mengen:**

- **Stammdaten:** Friedhofsplan (Feld-Nummer, Grab-Nummer, Grabart), Gebühren-Tabelle für Ersterwerb/Verlängerung je Grabart.
- **Bewegungsdaten:** Grab-Akte (Feld, Verstorbener Name, Bestattungsdatum, Nutzungsberechtigter Name, Ende der Ruhefrist).
- **Menge:** Ein Feld mit 30 Grabstellen. 5 Grab-Arten (Sarg, Urne, etc.)

---

## Umsetzungshinweise

1. **Keine Datenbank:** Speichere deine Daten in Listen (`List<T>`) innerhalb einer zentralen Klasse (z.B. `DatenSpeicher`).
2. **Initialisierung:** Erzeuge beim Programmstart direkt einige Test-Datensätze ("Mock-Daten"), damit du sofort Use Cases testen kannst.
3. **Klassendesign:** Erstelle für jede fachliche Entität (z.B. `Buerger`, `Antrag`, `Hund`) eine eigene Klasse.
4. **Pattern-Fokus:** Das Design Pattern sollte den Kern deines Problems lösen (z.B. die Berechnung oder den Workflow).
5. **Kapselung:** Achte darauf, dass Datenfelder `private` sind und der Zugriff nur über Properties oder Methoden erfolgt.
6. **Keine GUI:** Konzentriere dich auf die Logik. Nutze `Console.ReadLine()` für Eingaben und `Console.WriteLine()` für Ausgaben.
