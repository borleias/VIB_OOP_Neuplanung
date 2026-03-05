# Projektaufgabe

## Einleitung

In der Praxis-Phase haben Sie nun Zeit, ein eigenes Fachverfahren zu entwickeln.
Es geht nicht darum, ein riesiges System mit hunderten Funktionen zu bauen. Es geht darum, ein **kleines System architektonisch möglichst gut** zu konstruieren.

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
- Starten Sie mit der Fachlogik und schreiben Sie Tests (TDD!).
- Bauen Sie die UI und die Datenbank-Anbindung erst ganz am Schluss um Ihre Logik herum.

## Projektvorschläge

### 1. Wohngeld-Prüfmodul

**Fachdomäne:** Das Wohngeld ist eine Sozialleistung zur Sicherung angemessenen Wohnens. Die Verwaltung muss hierbei strikt nach gesetzlichen Tabellen prüfen, ob das Einkommen eines Haushalts im Verhältnis zur Miete und Personenzahl zu hoch ist. In dieser Domäne ist die **Rechtssicherheit** entscheidend: Jeder Bürger muss bei gleichen Voraussetzungen das gleiche Ergebnis erhalten.

- **Use Case 1 (Bürger):** Prüfen des individuellen Wohngeldanspruchs. (Ziel: Gewissheit über Förderfähigkeit erhalten).
- **Use Case 2 (Sachbearbeiter):** Festsetzen des monatlichen Wohngeldbetrages. (Ziel: Rechtsverbindliche Leistungsfeststellung).
- **Use Case 3 (System):** Ausgeben eines schriftlichen Bescheids. (Ziel: Erfüllung der Informationspflicht gegenüber dem Bürger).

**Pattern-Einsatz:** Das **Strategy Pattern** wird genutzt, um verschiedene Berechnungsvarianten (z.B. "Mietstufe I" vs. "Mietstufe VI") austauschbar zu machen, ohne den Rechenkern zu verändern.

**Datenmodell:**

- **Stammdaten:** Mietstufen-Tabelle (6 Stufen mit jeweils max. förderfähiger Miete), Einkommensgrenzen-Tabelle (nach Haushaltsgröße 1-5 Personen).
- **Bewegungsdaten:** Bürger-Datensatz (Name, Adresse), Haushalts-Datensatz (Anzahl Personen, Brutto-Einkommen, Kaltmiete).
- **Menge:** Simuliere 3 verschiedene Haushalte und die gesetzliche Mietstufen-Tabelle (ca. 10-12 Datensätze).

### 2. Kfz-Bestandsverwaltung

**Fachdomäne:** Die Zulassungsbehörde führt das Fahrzeugregister. Dieses dient der Verkehrssicherheit und der Steuererhebung. Ein Fahrzeug durchläuft einen Lebenszyklus von der Erstzulassung bis zur Außerbetriebsetzung. Die Integrität des Registers ist von höchster Bedeutung für die Polizei und Versicherungen.

- **Use Case 1 (Bürger):** Anmelden eines Neufahrzeugs. (Ziel: Erhalt der Betriebserlaubnis für den öffentlichen Raum).
- **Use Case 2 (Sachbearbeiter):** Reservieren eines Wunschkennzeichens. (Ziel: Sicherstellung der Einmaligkeit einer Kennung).
- **Use Case 3 (Bürger):** Stilllegen eines Fahrzeugs. (Ziel: Beendigung der Steuer- und Versicherungspflicht).

**Pattern-Einsatz:** Das **State Pattern** bildet den Lebenszyklus ab (Zugelassen, Abgemeldet). Bestimmte Aktionen (wie Kennzeichen-Wechsel) sind nur in bestimmten Zuständen erlaubt.

**Datenmodell:**

- **Stammdaten:** Kennzeichen-Pool (Liste aller verfügbaren Kombinationen, z.B. B-AA 100 bis B-ZZ 999).
- **Bewegungsdaten:** Fahrzeug-Akte (Fahrzeugidentifikationsnummer VIN, Marke, Modell, aktueller Status, Kennzeichen, Halter-ID).
- **Menge:** Ein Pool von ca. 50 Kennzeichen und 5-10 zugelassenen Fahrzeugen.

### 3. Kita-Priorisierung

**Fachdomäne:** Kita-Plätze sind eine knappe Ressource der kommunalen Daseinsvorsorge. Die Verteilung muss nach **objektiven Fairneß-Kriterien** erfolgen. Die Verwaltung muss hierbei soziale Faktoren (Berufstätigkeit, Alleinerziehend, Geschwisterkinder) gewichten, um Diskriminierung zu vermeiden und den Rechtsanspruch auf einen Betreuungsplatz zu erfüllen.

- **Use Case 1 (Eltern):** Einreichen einer Bedarfsanmeldung. (Ziel: Aufnahme in das Verteilungssystem).
- **Use Case 2 (Sachbearbeiter):** Ermitteln der Prioritäts-Punktzahl. (Ziel: Objektive Einstufung der Dringlichkeit).
- **Use Case 3 (Leitung):** Zuweisen eines freien Platzes. (Ziel: Optimale Auslastung der Kapazitäten).

**Pattern-Einsatz:** Eine **Factory** erstellt verschiedene "Bewerber-Profile" (z.B. Krippenkind vs. Elementarkind), die unterschiedliche Grund-Prioritäten besitzen.

**Datenmodell:**

- **Stammdaten:** Kita-Stamm (Name, Kapazität, Altersgruppe U3/Ü3), Kriterien-Katalog (Punkte für Berufstätigkeit, Alleinerziehend etc.).
- **Bewegungsdaten:** Kind-Datensatz (Name, Geburtsdatum, Geschwister-ID), Bewerbung (Priorisierte Liste von 3 Kitas, Status der Zuweisung).
- **Menge:** 2 Kitas mit je 10 Plätzen und eine Warteliste von 15 Kindern.

### 4. Bürgeramt-Terminreservierung

**Fachdomäne:** Moderne Verwaltung versteht sich als Dienstleister. Terminbuchungssysteme verhindern lange Wartezeiten und erlauben eine effiziente Personalplanung. Die Domäne befasst sich mit der Verwaltung von Zeit-Ressourcen und der Vermeidung von Doppelbuchungen.

- **Use Case 1 (Bürger):** Buchen eines freien Terminfensters. (Ziel: Sicherstellung einer persönlichen Beratung).
- **Use Case 2 (Sachbearbeiter):** Verwalten des täglichen Terminspiegels. (Ziel: Übersicht über das Arbeitsaufkommen).
- **Use Case 3 (System):** Stornieren eines Termins bei Nicht-Erscheinen. (Ziel: Wiederfreigabe der Ressource Zeit).

**Pattern-Einsatz:** Das **Observer Pattern** informiert andere Module (z.B. das Statistik-Modul oder die Infotafel), wenn ein Termin gebucht oder storniert wurde.

**Datenmodell:**

- **Stammdaten:** Dienstleistungs-Katalog (Name der Leistung, Dauer in Minuten, z.B. "Pass beantragen" = 20 Min).
- **Bewegungsdaten:** Terminslot (Datum, Uhrzeit, Status gebucht/frei), Buchungs-Datensatz (Bürgername, gewählte Leistung, Buchungscode).
- **Menge:** Ein Arbeitstag mit Slots alle 15 Minuten von 08:00 bis 12:00 Uhr.

### 5. Fundbüro-Bestandsführung

**Fachdomäne:** Das Fundrecht (§§ 965-984 BGB) regelt die Pflichten des Finders und der Behörde. Die Verwaltung muss Fundsachen sicher verwahren, dokumentieren und versuchen, den Eigentümer zu ermitteln. Nach Ablauf einer Frist (6 Monate) geht das Eigentum ggf. auf den Finder oder die Kommune über.

- **Use Case 1 (Finder):** Anzeigen eines Fundes. (Ziel: Erfüllung der gesetzlichen Meldepflicht).
- **Use Case 2 (Verlierer):** Suchen nach einem verlorenen Gegenstand. (Ziel: Wiedererlangung des Eigentums).
- **Use Case 3 (Sachbearbeiter):** Dokumentieren der Rückgabe. (Ziel: Rechtssicherer Abschluss des Fundvorgangs).

**Pattern-Einsatz:** Das **Strategy Pattern** erlaubt verschiedene Such-Strategien (z.B. "Exakter Abgleich der Seriennummer" vs. "Grobe Kategorien-Suche").

**Datenmodell:**

- **Stammdaten:** Kategorien-Baum (Elektronik, Kleidung, Dokumente, Wertsachen).
- **Bewegungsdaten:** Fund-Objekt (Beschreibung, Funddatum, Fundort, Aufbewahrungsort im Lager, Status: im Lager/zurückgegeben/versteigert).
- **Menge:** Ein Lagerbestand von ca. 20 Fundstücken mit unterschiedlichen Fristen.

### 6. Bauantrags-Statusverfolgung

**Fachdomäne:** Das Baugenehmigungsverfahren stellt sicher, dass bauliche Anlagen den öffentlich-rechtlichen Vorschriften (Brandschutz, Statik) entsprechen. Es ist ein hoheitlicher Prozess mit klaren rechtlichen Stufen. Transparenz über den Bearbeitungsstand ist für Bauherren (Investitionssicherheit) essenziell.

- **Use Case 1 (Architekt):** Einreichen der Bauunterlagen. (Ziel: Start des Genehmigungsverfahrens).
- **Use Case 2 (Fachabteilung):** Dokumentieren eines Prüfungsschritts (z.B. Brandschutz). (Ziel: Fortschritt im Workflow).
- **Use Case 3 (Bauherr):** Einsehen des Genehmigungsstatus. (Ziel: Planungssicherheit für den Baustart).

**Pattern-Einsatz:** Das **State Pattern** stellt sicher, dass ein Bescheid erst erstellt werden kann, wenn alle Prüf-Zustände (Brandschutz, Statik) positiv durchlaufen wurden.

**Datenmodell:**

- **Stammdaten:** Liste der Fachabteilungen (Brandschutz, Statik, Denkmalschutz).
- **Bewegungsdaten:** Bauantrag (ID, Bauherr, Flurstücksnummer, Beschreibung, Liste der abgeschlossenen Teil-Prüfungen, Gesamtstatus).
- **Menge:** 5 laufende Bauanträge in unterschiedlichen Prüfungsphasen.

### 7. Hundesteuer-Veranlagung

**Fachdomäne:** Die Hundesteuer ist eine örtliche Aufwandsteuer. Sie dient der Finanzierung kommunaler Aufgaben und der Lenkung (Eindämmung der Hundehaltung, insbesondere gefährlicher Rassen). Die Verwaltung muss hierbei unterschiedliche Steuersätze und Befreiungsgründe rechtssicher anwenden.

- **Use Case 1 (Halter):** Anmelden eines neuen Hundes. (Ziel: Erfüllung der steuerlichen Meldepflicht).
- **Use Case 2 (Sachbearbeiter):** Festsetzen der jährlichen Steuerlast. (Ziel: Erstellung der Einnahmenbasis).
- **Use Case 3 (Halter):** Beantragen einer Steuerbefreiung (z.B. für Assistenzhunde). (Ziel: Finanzielle Entlastung bei berechtigtem Interesse).

**Pattern-Einsatz:** Das **Strategy Pattern** kapselt die Steuertarife (Normal, Kampfhund, Befreit), sodass diese bei Satzungsänderungen leicht getauscht werden können.

**Datenmodell:**

- **Stammdaten:** Steuersatz-Tabelle (Ersthund: 120€, Zweithund: 240€, Gefährlicher Hund: 600€).
- **Bewegungsdaten:** Halter-Datensatz (Name, Anschrift), Hund-Datensatz (Rasse, Chip-Nummer, Anmeldedatum, Befreiungsstatus).
- **Menge:** 10 Halter mit insgesamt 15 Hunden (inkl. Mehrhundehaushalte).

### 8. Wahlhelfer-Organisation

**Fachdomäne:** Wahlen sind das Fundament der Demokratie. Die Organisation erfordert die Einteilung von Tausenden Freiwilligen in Wahllokale. Die Verwaltung muss sicherstellen, dass jedes Wahllokal gesetzlich korrekt besetzt ist (Wahlvorstand, Schriftführer, Beisitzer).

- **Use Case 1 (Bürger):** Melden zur ehrenamtlichen Wahlhilfe. (Ziel: Übernahme staatsbürgerlicher Verantwortung).
- **Use Case 2 (Wahlbehörde):** Einteilen des Wahlvorstands. (Ziel: Sicherstellung der Wahlrechts-Konformität).
- **Use Case 3 (Helfer):** Abrufen der Einsatzinformationen. (Ziel: Information über Ort und Zeit des Dienstes).

**Pattern-Einsatz:** Eine **Factory** erzeugt die spezifischen Rollen-Objekte (Vorstand vs. Beisitzer), die unterschiedliche Befugnisse und Entschädigungssätze haben.

**Datenmodell:**

- **Stammdaten:** Wahllokal-Verzeichnis (Name, Adresse, Barrierefrei-Flag), Rollen-Definitionen (Vorstand, Schriftführer, Beisitzer mit Mindestanzahl).
- **Bewegungsdaten:** Helfer-Profil (Name, Erfahrungsschatz, Wunsch-Lokal, tatsächliche Zuweisung).
- **Menge:** 3 Wahllokale und eine Liste von 20 potenziellen Helfern.

### 9. Mängelmelder-System

**Fachdomäne:** Kommunen haben eine Verkehrssicherungspflicht. Defekte Straßenlaternen oder Schlaglöcher müssen zeitnah behoben werden, um Haftungsansprüche zu vermeiden. Der Mängelmelder stärkt zudem die Bürgerbeteiligung und die Identifikation mit dem Wohnort.

- **Use Case 1 (Bürger):** Melden eines Infrastruktur-Mangels. (Ziel: Gefahrenabwehr und Verbesserung des Stadtbildes).
- **Use Case 2 (Bauhof):** Übernehmen einer Reparatur-Aufgabe. (Ziel: Planmäßige Behebung des Schadens).
- **Use Case 3 (Bürger):** Verfolgen der Mängelbehebung. (Ziel: Transparenz über das Verwaltungshandeln).

**Pattern-Einsatz:** Das **Observer Pattern** benachrichtigt den Melder automatisch, sobald der Bauhof den Status auf "Behoben" setzt.

**Datenmodell:**

- **Stammdaten:** Zuständigkeits-Katalog (Straßenbeleuchtung -> Elektriker, Schlagloch -> Tiefbau).
- **Bewegungsdaten:** Mängel-Meldung (ID, Datum, Beschreibung, Standort, Foto-Link, Status: Neu/In Arbeit/Erledigt).
- **Menge:** 15 offene Mängelberichte in der Stadtverwaltung.

### 10. Gewerberegister-Eintrag

**Fachdomäne:** Die Gewerbeordnung verlangt eine Anzeige jeder gewerblichen Tätigkeit. Das Gewerberegister dient der Überwachung der Wirtschaft und der Information von Behörden (z.B. Finanzamt). Hierbei ist die korrekte Erfassung der Rechtsform und der Tätigkeit entscheidend.

- **Use Case 1 (Unternehmer):** Anzeigen einer Gewerbeeröffnung. (Ziel: Rechtmäßige Aufnahme der wirtschaftlichen Tätigkeit).
- **Use Case 2 (Gewerbeamt):** Prüfen der Anmeldung auf Plausibilität. (Ziel: Qualitätssicherung des Registers).
- **Use Case 3 (System):** Weiterleiten der Daten an das Finanzamt. (Ziel: Erfüllung der behördlichen Mitteilungspflicht).

**Pattern-Einsatz:** Das **Strategy Pattern** führt unterschiedliche Validierungen je nach Rechtsform aus (z.B. Prüfung des Handelsregistereintrags nur bei juristischen Personen).

**Datenmodell:**

- **Stammdaten:** Rechtsformen-Verzeichnis (Einzelunternehmen, GmbH, GbR, AG), Behörden-Verteiler (Finanzamt, IHK, HWK).
- **Bewegungsdaten:** Gewerbe-Anmeldung (Name des Betriebs, Tätigkeit, Startdatum, Rechtsform, Steuernummer).
- **Menge:** 5 neue Gewerbe-Anmeldungen pro Woche zur Bearbeitung.

### 11. Friedhofs-Grabverwaltung

**Fachdomäne:** Friedhöfe sind Orte der Trauer und der Kultur. Die Verwaltung muss Grabstätten über lange Zeiträume (20-30 Jahre) verwalten. Die Überwachung von Ruhefristen ist aus hygienischen und platzwirtschaftlichen Gründen zwingend erforderlich.

- **Use Case 1 (Angehöriger):** Erwerben eines Nutzungsrechts an einer Grabstätte. (Ziel: Vorsorge für einen Bestattungsfall).
- **Use Case 2 (Verwaltung):** Überwachen des Ablaufs der Ruhefrist. (Ziel: Geordnete Neubelegung von Flächen).
- **Use Case 3 (Angehöriger):** Verlängern der Grabnutzung. (Ziel: Erhalt der Gedenkstätte über die Mindestfrist hinaus).

**Pattern-Einsatz:** Das **Strategy Pattern** berechnet die Laufzeiten je nach Grabart (Urne vs. Sarg), da diese gesetzlich unterschiedlich geregelt sind.

**Datenmodell & Mengen:**

- **Stammdaten:** Friedhofsplan (Feld-Nummer, Grab-Nummer, Grabart: Urne/Sarg), Gebühren-Tabelle für Verlängerungen.
- **Bewegungsdaten:** Grab-Akte (Verstorbener Name, Bestattungsdatum, Nutzungsberechtigter Name, Ende der Ruhefrist).
- **Menge:** Ein Feld mit 30 Grabstellen, davon 10 kurz vor Ablauf der Frist.

### 12. Parkausweis-Validierung

**Fachdomäne:** Urbaner Parkraum ist begrenzt. Bewohnerparkausweise privilegieren Anwohner gegenüber Pendlern. Die Verwaltung muss hierbei den Wohnsitz und die Fahrzeughalterschaft prüfen, um Missbrauch zu verhindern und den Parkdruck für Bürger zu senken.

- **Use Case 1 (Anwohner):** Beantragen eines Bewohnerparkausweises. (Ziel: Erhalt einer Parkberechtigung im Wohnquartier).
- **Use Case 2 (Sachbearbeiter):** Validieren der Meldedaten. (Ziel: Sicherstellung der Anspruchsberechtigung).
- **Use Case 3 (Polizei/Ordnungsamt):** Prüfen der Gültigkeit eines Ausweises. (Ziel: Überwachung des ruhenden Verkehrs).

**Pattern-Einsatz:** Der **Adapter** (angedeutet) könnte genutzt werden, um Daten aus dem externen Einwohnermelderegister in das Parksystem zu übersetzen.

**Datenmodell:**

- **Stammdaten:** Parkzonen-Verzeichnis (Zone A-Z, zugehörige Straßennamen).
- **Bewegungsdaten:** Parkausweis-Antrag (Bürger-ID, Kennzeichen, Fahrzeugtyp, Ablaufdatum, Status: Aktiv/Abgelaufen).
- **Menge:** 10 aktive Anträge in einer spezifischen Parkzone.

### 13. Schulplatz-Zuweisung

**Fachdomäne:** Der Staat hat einen Bildungsauftrag. Die Zuweisung zu Grundschulen erfolgt meist nach Einzugsgebieten, bei weiterführenden Schulen nach Kapazität und pädagogischen Profilen. Die Verwaltung muss hierbei die Elternwünsche mit den vorhandenen Raumkapazitäten in Einklang bringen.

- **Use Case 1 (Eltern):** Anmelden eines schulpflichtigen Kindes. (Ziel: Erfüllung der Schulpflicht an einer Wunschschule).
- **Use Case 2 (Schulamt):** Prüfen der Kapazitätsgrenzen. (Ziel: Vermeidung von Klassen-Überfüllung).
- **Use Case 3 (Schulleitung):** Bestätigen der Aufnahme. (Ziel: Finalisierung der Klassenplanung).

**Pattern-Einsatz:** Ein **Singleton** verwaltet die zentrale Kapazitätsliste aller Schulen, um sicherzustellen, dass kein Platz doppelt vergeben wird.

**Datenmodell:**

- **Stammdaten:** Schul-Verzeichnis (Name, Typ, max. Plätze pro Jahrgang).
- **Bewegungsdaten:** Schüler-Datensatz (Name, Adresse, Geburtsdatum, Geschwisterkind-Status, Wunsch-Schule).
- **Menge:** 3 Schulen mit je 2 Klassen à 25 Plätzen und 160 Bewerbern.

### 14. Sperrmüll-Terminmanagement

**Fachdomäne:** Die geordnete Abfallentsorgung gehört zur kommunalen Umweltschutzpflicht. Die Abholung von Sperrmüll muss logistisch geplant werden, um Fahrwege zu minimieren und die Entsorgungsanlagen gleichmäßig auszulasten.

- **Use Case 1 (Bürger):** Buchen eines Abholtermins. (Ziel: Entsorgung von sperrigen Haushaltsgegenständen).
- **Use Case 2 (Entsorgungsbetrieb):** Erstellen der Tages-Tourenliste. (Ziel: Logistische Optimierung der Abholung).
- **Use Case 3 (System):** Berechnen der anfallenden Gebühren. (Ziel: Kostendeckung der Entsorgungsleistung).

**Pattern-Einsatz:** Das **Strategy Pattern** berechnet die Gebühren je nach Abfallmenge oder Haushaltsgröße.

**Datenmodell:**

- **Stammdaten:** Abholbezirke (Zuteilung von Straßen zu Wochentagen), Abfall-Katalog (Sofa, Kühlschrank, Holzschrank mit jeweiligen Volumeneinheiten).
- **Bewegungsdaten:** Buchungs-Datensatz (Datum, Adresse, Liste der Gegenstände, Gesamtvolumen, Gebühr).
- **Menge:** Ein Tourenplan für einen Bezirk mit 12 Abholstellen.

### 15. Integrationskurs-Tracking

**Fachdomäne:** Integration ist eine gesamtgesellschaftliche Aufgabe. Sprachkurse sind hierbei der Schlüssel. Die Verwaltung (oft im Auftrag des BAMF) muss die Teilnahme überwachen, da die Förderung an die Anwesenheit gekoppelt ist. Die Domäne befasst sich mit Bildungsbiografien und staatlichen Fördermitteln.

- **Use Case 1 (Teilnehmer):** Dokumentieren der täglichen Anwesenheit. (Ziel: Sicherung des Förderanspruchs).
- **Use Case 2 (Kursleiter):** Bestätigen des erfolgreichen Kursabschlusses. (Ziel: Nachweis der Integrationsleistung).
- **Use Case 3 (Behörde):** Abrechnen der Kurskosten. (Ziel: Korrekte Verwendung von Steuermitteln).

**Pattern-Einsatz:** Das **Observer Pattern** löst automatisch eine Meldung an die Ausländerbehörde aus, wenn ein Teilnehmer unentschuldigt fehlt (Verletzung der Mitwirkungspflicht).

**Datenmodell:**

- **Stammdaten:** Kurs-Angebot (ID, Sprache, Niveau A1-C1, Gesamtstundenzahl), Fördergeber-Liste (BAMF, Kommune).
- **Bewegungsdaten:** Teilnehmer-Akte (Name, ID, Fehlstunden-Konto, aktuelles Modul, Prüfungsstatus).
- **Menge:** 2 parallel laufende Kurse mit je 15 Teilnehmern.

---

## Umsetzungshinweise

1. **Keine Datenbank:** Speichere deine Daten in Listen (`List<T>`) innerhalb einer zentralen Klasse (z.B. `DatenSpeicher`).
2. **Initialisierung:** Erzeuge beim Programmstart direkt einige Test-Datensätze ("Mock-Daten"), damit du sofort Use Cases testen kannst.
3. **Klassendesign:** Erstelle für jede fachliche Entität (z.B. `Buerger`, `Antrag`, `Hund`) eine eigene Klasse.
4. **Pattern-Fokus:** Das Design Pattern sollte den Kern deines Problems lösen (z.B. die Berechnung oder den Workflow).
5. **Kapselung:** Achte darauf, dass Datenfelder `private` sind und der Zugriff nur über Properties oder Methoden erfolgt.
6. **Keine GUI:** Konzentriere dich auf die Logik. Nutze `Console.ReadLine()` für Eingaben und `Console.WriteLine()` für Ausgaben.
