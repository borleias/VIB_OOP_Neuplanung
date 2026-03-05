# Einfache Projektaufgaben (2-Wochen-Sprint)
## Fokus: Fachdomäne und zielorientierte Use Cases

Diese Projektsammlung richtet sich an Studierende der Verwaltungsinformatik im 3. Semester. Die Aufgaben sind so reduziert, dass sie in **zwei Wochen** umsetzbar sind. Der Fokus liegt nicht auf der Menge des Codes, sondern auf der **Abbildung fachlicher Logik in einer sauberen Objektstruktur**.

---

## 1. Wohngeld-Prüfmodul
**Fachdomäne:** Das Wohngeld ist eine Sozialleistung zur Sicherung angemessenen Wohnens. Die Verwaltung muss hierbei strikt nach gesetzlichen Tabellen prüfen, ob das Einkommen eines Haushalts im Verhältnis zur Miete und Personenzahl zu hoch ist. In dieser Domäne ist die **Rechtssicherheit** entscheidend: Jeder Bürger muss bei gleichen Voraussetzungen das gleiche Ergebnis erhalten.
*   **Use Case 1 (Bürger):** Prüfen des individuellen Wohngeldanspruchs. (Ziel: Gewissheit über Förderfähigkeit erhalten).
*   **Use Case 2 (Sachbearbeiter):** Festsetzen des monatlichen Wohngeldbetrages. (Ziel: Rechtsverbindliche Leistungsfeststellung).
*   **Use Case 3 (System):** Ausgeben eines schriftlichen Bescheids. (Ziel: Erfüllung der Informationspflicht gegenüber dem Bürger).
*   **Pattern-Einsatz:** Das **Strategy Pattern** wird genutzt, um verschiedene Berechnungsvarianten (z.B. "Mietstufe I" vs. "Mietstufe VI") austauschbar zu machen, ohne den Rechenkern zu verändern.

## 2. Kfz-Bestandsverwaltung
**Fachdomäne:** Die Zulassungsbehörde führt das Fahrzeugregister. Dieses dient der Verkehrssicherheit und der Steuererhebung. Ein Fahrzeug durchläuft einen Lebenszyklus von der Erstzulassung bis zur Außerbetriebsetzung. Die Integrität des Registers ist von höchster Bedeutung für die Polizei und Versicherungen.
*   **Use Case 1 (Bürger):** Anmelden eines Neufahrzeugs. (Ziel: Erhalt der Betriebserlaubnis für den öffentlichen Raum).
*   **Use Case 2 (Sachbearbeiter):** Reservieren eines Wunschkennzeichens. (Ziel: Sicherstellung der Einmaligkeit einer Kennung).
*   **Use Case 3 (Bürger):** Stilllegen eines Fahrzeugs. (Ziel: Beendigung der Steuer- und Versicherungspflicht).
*   **Pattern-Einsatz:** Das **State Pattern** bildet den Lebenszyklus ab (Zugelassen, Abgemeldet). Bestimmte Aktionen (wie Kennzeichen-Wechsel) sind nur in bestimmten Zuständen erlaubt.

## 3. Kita-Priorisierung
**Fachdomäne:** Kita-Plätze sind eine knappe Ressource der kommunalen Daseinsvorsorge. Die Verteilung muss nach **objektiven Fairneß-Kriterien** erfolgen. Die Verwaltung muss hierbei soziale Faktoren (Berufstätigkeit, Alleinerziehend, Geschwisterkinder) gewichten, um Diskriminierung zu vermeiden und den Rechtsanspruch auf einen Betreuungsplatz zu erfüllen.
*   **Use Case 1 (Eltern):** Einreichen einer Bedarfsanmeldung. (Ziel: Aufnahme in das Verteilungssystem).
*   **Use Case 2 (Sachbearbeiter):** Ermitteln der Prioritäts-Punktzahl. (Ziel: Objektive Einstufung der Dringlichkeit).
*   **Use Case 3 (Leitung):** Zuweisen eines freien Platzes. (Ziel: Optimale Auslastung der Kapazitäten).
*   **Pattern-Einsatz:** Eine **Factory** erstellt verschiedene "Bewerber-Profile" (z.B. Krippenkind vs. Elementarkind), die unterschiedliche Grund-Prioritäten besitzen.

## 4. Bürgeramt-Terminreservierung
**Fachdomäne:** Moderne Verwaltung versteht sich als Dienstleister. Terminbuchungssysteme verhindern lange Wartezeiten und erlauben eine effiziente Personalplanung. Die Domäne befasst sich mit der Verwaltung von Zeit-Ressourcen und der Vermeidung von Doppelbuchungen.
*   **Use Case 1 (Bürger):** Buchen eines freien Terminfensters. (Ziel: Sicherstellung einer persönlichen Beratung).
*   **Use Case 2 (Sachbearbeiter):** Verwalten des täglichen Terminspiegels. (Ziel: Übersicht über das Arbeitsaufkommen).
*   **Use Case 3 (System):** Stornieren eines Termins bei Nicht-Erscheinen. (Ziel: Wiederfreigabe der Ressource Zeit).
*   **Pattern-Einsatz:** Das **Observer Pattern** informiert andere Module (z.B. das Statistik-Modul oder die Infotafel), wenn ein Termin gebucht oder storniert wurde.

## 5. Fundbüro-Bestandsführung
**Fachdomäne:** Das Fundrecht (§§ 965-984 BGB) regelt die Pflichten des Finders und der Behörde. Die Verwaltung muss Fundsachen sicher verwahren, dokumentieren und versuchen, den Eigentümer zu ermitteln. Nach Ablauf einer Frist (6 Monate) geht das Eigentum ggf. auf den Finder oder die Kommune über.
*   **Use Case 1 (Finder):** Anzeigen eines Fundes. (Ziel: Erfüllung der gesetzlichen Meldepflicht).
*   **Use Case 2 (Verlierer):** Suchen nach einem verlorenen Gegenstand. (Ziel: Wiedererlangung des Eigentums).
*   **Use Case 3 (Sachbearbeiter):** Dokumentieren der Rückgabe. (Ziel: Rechtssicherer Abschluss des Fundvorgangs).
*   **Pattern-Einsatz:** Das **Strategy Pattern** erlaubt verschiedene Such-Strategien (z.B. "Exakter Abgleich der Seriennummer" vs. "Grobe Kategorien-Suche").

## 6. Bauantrags-Statusverfolgung
**Fachdomäne:** Das Baugenehmigungsverfahren stellt sicher, dass bauliche Anlagen den öffentlich-rechtlichen Vorschriften (Brandschutz, Statik) entsprechen. Es ist ein hoheitlicher Prozess mit klaren rechtlichen Stufen. Transparenz über den Bearbeitungsstand ist für Bauherren (Investitionssicherheit) essenziell.
*   **Use Case 1 (Architekt):** Einreichen der Bauunterlagen. (Ziel: Start des Genehmigungsverfahrens).
*   **Use Case 2 (Fachabteilung):** Dokumentieren eines Prüfungsschritts (z.B. Brandschutz). (Ziel: Fortschritt im Workflow).
*   **Use Case 3 (Bauherr):** Einsehen des Genehmigungsstatus. (Ziel: Planungssicherheit für den Baustart).
*   **Pattern-Einsatz:** Das **State Pattern** stellt sicher, dass ein Bescheid erst erstellt werden kann, wenn alle Prüf-Zustände (Brandschutz, Statik) positiv durchlaufen wurden.

## 7. Hundesteuer-Veranlagung
**Fachdomäne:** Die Hundesteuer ist eine örtliche Aufwandsteuer. Sie dient der Finanzierung kommunaler Aufgaben und der Lenkung (Eindämmung der Hundehaltung, insbesondere gefährlicher Rassen). Die Verwaltung muss hierbei unterschiedliche Steuersätze und Befreiungsgründe rechtssicher anwenden.
*   **Use Case 1 (Halter):** Anmelden eines neuen Hundes. (Ziel: Erfüllung der steuerlichen Meldepflicht).
*   **Use Case 2 (Sachbearbeiter):** Festsetzen der jährlichen Steuerlast. (Ziel: Erstellung der Einnahmenbasis).
*   **Use Case 3 (Halter):** Beantragen einer Steuerbefreiung (z.B. für Assistenzhunde). (Ziel: Finanzielle Entlastung bei berechtigtem Interesse).
*   **Pattern-Einsatz:** Das **Strategy Pattern** kapselt die Steuertarife (Normal, Kampfhund, Befreit), sodass diese bei Satzungsänderungen leicht getauscht werden können.

## 8. Wahlhelfer-Organisation
**Fachdomäne:** Wahlen sind das Fundament der Demokratie. Die Organisation erfordert die Einteilung von Tausenden Freiwilligen in Wahllokale. Die Verwaltung muss sicherstellen, dass jedes Wahllokal gesetzlich korrekt besetzt ist (Wahlvorstand, Schriftführer, Beisitzer).
*   **Use Case 1 (Bürger):** Melden zur ehrenamtlichen Wahlhilfe. (Ziel: Übernahme staatsbürgerlicher Verantwortung).
*   **Use Case 2 (Wahlbehörde):** Einteilen des Wahlvorstands. (Ziel: Sicherstellung der Wahlrechts-Konformität).
*   **Use Case 3 (Helfer):** Abrufen der Einsatzinformationen. (Ziel: Information über Ort und Zeit des Dienstes).
*   **Pattern-Einsatz:** Eine **Factory** erzeugt die spezifischen Rollen-Objekte (Vorstand vs. Beisitzer), die unterschiedliche Befugnisse und Entschädigungssätze haben.

## 9. Mängelmelder-System
**Fachdomäne:** Kommunen haben eine Verkehrssicherungspflicht. Defekte Straßenlaternen oder Schlaglöcher müssen zeitnah behoben werden, um Haftungsansprüche zu vermeiden. Der Mängelmelder stärkt zudem die Bürgerbeteiligung und die Identifikation mit dem Wohnort.
*   **Use Case 1 (Bürger):** Melden eines Infrastruktur-Mangels. (Ziel: Gefahrenabwehr und Verbesserung des Stadtbildes).
*   **Use Case 2 (Bauhof):** Übernehmen einer Reparatur-Aufgabe. (Ziel: Planmäßige Behebung des Schadens).
*   **Use Case 3 (Bürger):** Verfolgen der Mängelbehebung. (Ziel: Transparenz über das Verwaltungshandeln).
*   **Pattern-Einsatz:** Das **Observer Pattern** benachrichtigt den Melder automatisch, sobald der Bauhof den Status auf "Behoben" setzt.

## 10. Gewerberegister-Eintrag
**Fachdomäne:** Die Gewerbeordnung verlangt eine Anzeige jeder gewerblichen Tätigkeit. Das Gewerberegister dient der Überwachung der Wirtschaft und der Information von Behörden (z.B. Finanzamt). Hierbei ist die korrekte Erfassung der Rechtsform und der Tätigkeit entscheidend.
*   **Use Case 1 (Unternehmer):** Anzeigen einer Gewerbeeröffnung. (Ziel: Rechtmäßige Aufnahme der wirtschaftlichen Tätigkeit).
*   **Use Case 2 (Gewerbeamt):** Prüfen der Anmeldung auf Plausibilität. (Ziel: Qualitätssicherung des Registers).
*   **Use Case 3 (System):** Weiterleiten der Daten an das Finanzamt. (Ziel: Erfüllung der behördlichen Mitteilungspflicht).
*   **Pattern-Einsatz:** Das **Strategy Pattern** führt unterschiedliche Validierungen je nach Rechtsform aus (z.B. Prüfung des Handelsregistereintrags nur bei juristischen Personen).

## 11. Friedhofs-Grabverwaltung
**Fachdomäne:** Friedhöfe sind Orte der Trauer und der Kultur. Die Verwaltung muss Grabstätten über lange Zeiträume (20-30 Jahre) verwalten. Die Überwachung von Ruhefristen ist aus hygienischen und platzwirtschaftlichen Gründen zwingend erforderlich.
*   **Use Case 1 (Angehöriger):** Erwerben eines Nutzungsrechts an einer Grabstätte. (Ziel: Vorsorge für einen Bestattungsfall).
*   **Use Case 2 (Verwaltung):** Überwachen des Ablaufs der Ruhefrist. (Ziel: Geordnete Neubelegung von Flächen).
*   **Use Case 3 (Angehöriger):** Verlängern der Grabnutzung. (Ziel: Erhalt der Gedenkstätte über die Mindestfrist hinaus).
*   **Pattern-Einsatz:** Das **Strategy Pattern** berechnet die Laufzeiten je nach Grabart (Urne vs. Sarg), da diese gesetzlich unterschiedlich geregelt sind.

## 12. Parkausweis-Validierung
**Fachdomäne:** Urbaner Parkraum ist begrenzt. Bewohnerparkausweise privilegieren Anwohner gegenüber Pendlern. Die Verwaltung muss hierbei den Wohnsitz und die Fahrzeughalterschaft prüfen, um Missbrauch zu verhindern und den Parkdruck für Bürger zu senken.
*   **Use Case 1 (Anwohner):** Beantragen eines Bewohnerparkausweises. (Ziel: Erhalt einer Parkberechtigung im Wohnquartier).
*   **Use Case 2 (Sachbearbeiter):** Validieren der Meldedaten. (Ziel: Sicherstellung der Anspruchsberechtigung).
*   **Use Case 3 (Polizei/Ordnungsamt):** Prüfen der Gültigkeit eines Ausweises. (Ziel: Überwachung des ruhenden Verkehrs).
*   **Pattern-Einsatz:** Der **Adapter** (angedeutet) könnte genutzt werden, um Daten aus dem externen Einwohnermelderegister in das Parksystem zu übersetzen.

## 13. Schulplatz-Zuweisung
**Fachdomäne:** Der Staat hat einen Bildungsauftrag. Die Zuweisung zu Grundschulen erfolgt meist nach Einzugsgebieten, bei weiterführenden Schulen nach Kapazität und pädagogischen Profilen. Die Verwaltung muss hierbei die Elternwünsche mit den vorhandenen Raumkapazitäten in Einklang bringen.
*   **Use Case 1 (Eltern):** Anmelden eines schulpflichtigen Kindes. (Ziel: Erfüllung der Schulpflicht an einer Wunschschule).
*   **Use Case 2 (Schulamt):** Prüfen der Kapazitätsgrenzen. (Ziel: Vermeidung von Klassen-Überfüllung).
*   **Use Case 3 (Schulleitung):** Bestätigen der Aufnahme. (Ziel: Finalisierung der Klassenplanung).
*   **Pattern-Einsatz:** Ein **Singleton** verwaltet die zentrale Kapazitätsliste aller Schulen, um sicherzustellen, dass kein Platz doppelt vergeben wird.

## 14. Sperrmüll-Terminmanagement
**Fachdomäne:** Die geordnete Abfallentsorgung gehört zur kommunalen Umweltschutzpflicht. Die Abholung von Sperrmüll muss logistisch geplant werden, um Fahrwege zu minimieren und die Entsorgungsanlagen gleichmäßig auszulasten.
*   **Use Case 1 (Bürger):** Buchen eines Abholtermins. (Ziel: Entsorgung von sperrigen Haushaltsgegenständen).
*   **Use Case 2 (Entsorgungsbetrieb):** Erstellen der Tages-Tourenliste. (Ziel: Logistische Optimierung der Abholung).
*   **Use Case 3 (System):** Berechnen der anfallenden Gebühren. (Ziel: Kostendeckung der Entsorgungsleistung).
*   **Pattern-Einsatz:** Das **Strategy Pattern** berechnet die Gebühren je nach Abfallmenge oder Haushaltsgröße.

## 15. Integrationskurs-Tracking
**Fachdomäne:** Integration ist eine gesamtgesellschaftliche Aufgabe. Sprachkurse sind hierbei der Schlüssel. Die Verwaltung (oft im Auftrag des BAMF) muss die Teilnahme überwachen, da die Förderung an die Anwesenheit gekoppelt ist. Die Domäne befasst sich mit Bildungsbiografien und staatlichen Fördermitteln.
*   **Use Case 1 (Teilnehmer):** Dokumentieren der täglichen Anwesenheit. (Ziel: Sicherung des Förderanspruchs).
*   **Use Case 2 (Kursleiter):** Bestätigen des erfolgreichen Kursabschlusses. (Ziel: Nachweis der Integrationsleistung).
*   **Use Case 3 (Behörde):** Abrechnen der Kurskosten. (Ziel: Korrekte Verwendung von Steuermitteln).
*   **Pattern-Einsatz:** Das **Observer Pattern** löst automatisch eine Meldung an die Ausländerbehörde aus, wenn ein Teilnehmer unentschuldigt fehlt (Verletzung der Mitwirkungspflicht).

---

## Umsetzungshinweise für Studierende
1.  **Keine GUI:** Konzentriere dich auf die Logik. Nutze `Console.ReadLine()` für Eingaben und `Console.WriteLine()` für Ausgaben.
2.  **Klassendesign:** Erstelle für jede fachliche Entität (z.B. `Buerger`, `Antrag`, `Hund`) eine eigene Klasse.
3.  **Pattern-Fokus:** Das Design Pattern sollte den Kern deines Problems lösen (z.B. die Berechnung oder den Workflow).
4.  **Zeitmanagement:** Nutze die erste Woche für das Design der Klassen und der Use Cases, die zweite Woche für die Implementierung und einen kleinen Unit-Test.
