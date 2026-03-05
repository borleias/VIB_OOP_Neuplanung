# Projektaufgaben: Abschlussprojekt (OOP für Verwaltungsinformatiker)

Diese Projektsammlung dient als Grundlage für Ihre Abschlussarbeit im Modul "Objektorientierte Programmierung". Wählen Sie eines der folgenden 15 Szenarien aus. Ihr Ziel ist es, eine technisch saubere, erweiterbare und fachlich korrekte Lösung zu entwickeln.

---

## 1. Wohngeldrechner
*   **Szenario:** Entwicklung eines Systems zur automatisierten Berechnung von Wohngeldansprüchen. Verarbeitet werden Daten zu Haushaltsmitgliedern, dem Gesamteinkommen (abzüglich Freibeträgen) und der zuschussfähigen Miete (inkl. Mietstufen).
*   **Fachliche Herausforderung:** Die Berechnungsformeln sind komplex und hängen von sich jährlich ändernden Tabellenwerten und regionalen Mietstufen ab. Das System muss zudem verschiedene Bescheid-Typen (Bewilligung/Ablehnung) generieren.
*   **Muster-Empfehlungen:** Nutzen Sie das **Strategy Pattern** für die verschiedenen Berechnungsvarianten (z.B. Stand 2023 vs. Reform 2025). Setzen Sie die **Factory Method** ein, um basierend auf dem Ergebnis den passenden Bescheid-Typ zu erstellen.
*   **Architektur:** Trennen Sie die Berechnungslogik strikt von der Dateneingabe (Layered Architecture). Achten Sie auf das *Single Responsibility Principle (SRP)*: Eine Klasse berechnet, eine andere formatiert den Bescheid.

## 2. Digitales Kfz-Zulassungswesen
*   **Szenario:** Verwaltung von Fahrzeugdaten (VIN, Typ, Emission), Haltern und Kennzeichen-Reservierungen. Das System validiert die Versicherungsbestätigung (eVB-Nummer) und verwaltet den Status der Zulassung.
*   **Fachliche Herausforderung:** Die Koordination zwischen Kennzeichen-Pool, technischen Prüfungen (HU/AU) und der Halterhistorie. Es dürfen keine Dubletten bei Kennzeichen entstehen.
*   **Muster-Empfehlungen:** Ein **Singleton** bietet sich für die zentrale Verwaltung des Kennzeichen-Pools an. Das **State Pattern** ist ideal, um den Lebenszyklus eines Fahrzeugs (Abgemeldet, Zugelassen, Stillgelegt) abzubilden.
*   **Architektur:** Nutzen Sie Dependency Injection, um den Kennzeichen-Service in die Zulassungslogik einzuspielen. Dies erleichtert das Testen (Mocking).

## 3. Kita-Platz-Vergabesystem
*   **Szenario:** Ein System zur fairen Vergabe von Kita-Plätzen basierend auf einem Punktesystem. Kriterien sind unter anderem Alter des Kindes, Wohnortnähe, Berufstätigkeit der Eltern und Geschwisterkinder in der Einrichtung.
*   **Fachliche Herausforderung:** Das Matching-Verfahren zwischen verfügbaren Plätzen in verschiedenen Einrichtungen und der priorisierten Wunschliste der Eltern.
*   **Muster-Empfehlungen:** Verwenden Sie das **Strategy Pattern**, um unterschiedliche Scoring-Algorithmen (z.B. Sozialpunkte vs. Geschwister-Bonus) zu kapseln. Das **Observer Pattern** kann Eltern automatisch informieren, wenn sich der Status ihrer Bewerbung ändert.
*   **Architektur:** Halten Sie die Vergabelogik unabhängig von der Benutzeroberfläche (UI), um den Algorithmus automatisiert mit Unit-Tests validieren zu können.

## 4. Bürgeramt-Terminplaner
*   **Szenario:** Verwaltung von Terminslots für verschiedene Dienstleistungen (Reisepass, Anmeldung, Führerschein). Jede Dienstleistung hat eine spezifische Dauer und benötigte Ressourcen (z.B. Biometrie-Station).
*   **Fachliche Herausforderung:** Vermeidung von Doppelbuchungen und die effiziente Verteilung von Terminen über mehrere Standorte und Sachbearbeiter hinweg.
*   **Muster-Empfehlungen:** Nutzen Sie die **Factory Method**, um Termin-Objekte mit den jeweils korrekten Zeitintervallen für die gewählte Dienstleistung zu erzeugen. Ein **Singleton** kann den Zugriff auf den globalen Kalender-Ressourcen-Pool steuern.
*   **Architektur:** Befolgen Sie das *Open-Closed Principle (OCP)*: Neue Dienstleistungstypen sollten hinzugefügt werden können, ohne den bestehenden Buchungscode zu ändern.

## 5. Digitales Fundbüro
*   **Szenario:** Erfassung von Fundstücken (Kategorie, Fundort, Zeit) und Verlustmeldungen. Das System soll automatisiert Übereinstimmungen finden und die Rückgabe an den rechtmäßigen Eigentümer dokumentieren.
*   **Fachliche Herausforderung:** Fuzzy-Matching bei Beschreibungen (z.B. "schwarzes Handy" vs. "iPhone, dunkel") und die Verwaltung von Aufbewahrungsfristen.
*   **Muster-Empfehlungen:** Der **Adapter** kann genutzt werden, um Daten aus externen Systemen (z.B. Polizeidatenbanken) in das interne Format zu überführen. Der **Decorator** erlaubt es, Fundstücke dynamisch mit Zusatzinformationen (z.B. "Beschädigt", "Wertvoll", "Versteigert") zu versehen.
*   **Architektur:** Implementieren Sie eine klare Service-Schicht für das Matching, die unabhängig von der Datenbank-Technologie arbeitet.

## 6. Baugenehmigungs-Workflow
*   **Szenario:** Abbildung des Genehmigungsprozesses für Bauvorhaben. Ein Antrag durchläuft Stationen wie Formale Prüfung, Brandschutz, Denkmalschutz und Statik, bevor der finale Bescheid ergeht.
*   **Fachliche Herausforderung:** Komplexe Abhängigkeiten (einige Prüfungen parallel, andere sequenziell) und die Notwendigkeit, bei Ablehnung in einem Teilschritt den Gesamtprozess zu steuern.
*   **Muster-Empfehlungen:** Das **State Pattern** ist hier essenziell, um die verschiedenen Phasen des Antrags sauber zu trennen. Das **Observer Pattern** benachrichtigt beteiligte Fachabteilungen, sobald ein Antrag in deren Zuständigkeitsbereich wechselt.
*   **Architektur:** Nutzen Sie das Interface-Segregation-Principle, um den Fachabteilungen nur die für sie relevanten Methoden des Antrags anzuzeigen.

## 7. Hundesteuer-Portal
*   **Szenario:** Anmeldung von Hunden durch Bürger. Das System berechnet automatisch die Steuerlast basierend auf Rasse (Kampfhund-Zuschlag), Anzahl der Hunde im Haushalt und Ermäßigungsgründen (z.B. Rettungshunde).
*   **Fachliche Herausforderung:** Abbildung der kommunalen Satzung, die sich oft durch Ausnahmeregelungen und unterschiedliche Steuersätze pro Folgehund auszeichnet.
*   **Muster-Empfehlungen:** Das **Strategy Pattern** kapselt die Steuersatzungen verschiedener Kommunen. Eine **Factory** erstellt die jährlichen Steuerbescheide als PDF-Repräsentation.
*   **Architektur:** Trennen Sie die "Halter"-Entität strikt von der "Steuerberechnungs"-Logik (Low Coupling).

## 8. Wahlhelfer-Management
*   **Szenario:** Verwaltung von Freiwilligen für Wahltage. Das System teilt Personen basierend auf ihrer Erfahrung (Wahlvorstand, Schriftführer, Beisitzer) und ihrem Wohnort den Wahllokalen zu.
*   **Fachliche Herausforderung:** Koordination von Verfügbarkeiten und die Sicherstellung, dass jedes Wahllokal mit der gesetzlich vorgeschriebenen Mindestbesetzung und Qualifikation ausgestattet ist.
*   **Muster-Empfehlungen:** Nutzen Sie das **Strategy Pattern**, um verschiedene Zuweisungs-Algorithmen (z.B. "Kürzester Weg" vs. "Erfahrungsmix") zu implementieren. Der **Observer** informiert Helfer über Änderungen in ihrer Einteilung.
*   **Architektur:** Setzen Sie auf Komposition statt Vererbung bei der Modellierung von Helfer-Rollen, um flexiblere Profile zu ermöglichen.

## 9. Mängelmelder (Bürger-App)
*   **Szenario:** Bürger melden Schäden im öffentlichen Raum (Schlaglöcher, defekte Straßenlaternen, Müll). Das Backend kategorisiert die Meldungen, weist sie dem zuständigen Bauhof zu und gibt Status-Updates.
*   **Fachliche Herausforderung:** Priorisierung von Meldungen (Gefahr im Verzug) und die Integration von Geodaten und Fotos in den Workflow.
*   **Muster-Empfehlungen:** Der **Decorator** kann Meldungen dynamisch mit Metadaten (GPS-Fix, Foto-Anhang, Dringlichkeits-Flag) erweitern. Der **Observer** dient der Benachrichtigungskette vom Bauhof zurück zum Bürger.
*   **Architektur:** Nutzen Sie eine Schichtenarchitektur, um die API-Logik (Meldungsannahme) von der Business-Logik (Zuweisung) zu trennen.

## 10. Gewerbeanmeldung
*   **Szenario:** Ein digitaler Assistent zur Erfassung von Gewerbeanmeldungen, -ummeldungen und -abmeldungen. Das System führt Plausibilitätsprüfungen durch und ermittelt die Gebührenpflicht.
*   **Fachliche Herausforderung:** Abhängige Pflichtfelder basierend auf der gewählten Rechtsform (z.B. Handelsregisterauszug bei GmbH) und die Anbindung an verschiedene Empfängerstellen.
*   **Muster-Empfehlungen:** Das **State Pattern** steuert den Ausfüllprozess des Formulars. Eine **Factory** erzeugt die spezifischen Datensätze für die Übermittlung an Finanzamt, IHK und Berufsgenossenschaften.
*   **Architektur:** Implementieren Sie Validatoren als eigene Klassen (SRP), die gegen Interfaces prüfen, um die Testbarkeit zu erhöhen.

## 11. Friedhofsverwaltung
*   **Szenario:** Verwaltung von Grabstätten, Belegungsdauern und Verlängerungsoptionen. Das System führt ein digitales Register über Verstorbene und die dazugehörigen Nutzungsberechtigten.
*   **Fachliche Herausforderung:** Überwachung von Fristen (Ruhezeiten), die Verwaltung unterschiedlicher Grabarten (Urne, Wahlgrab, anonym) und die Abrechnung von Pflegeverträgen.
*   **Muster-Empfehlungen:** Das **Strategy Pattern** berechnet die Kosten je nach Grabart und Nutzungsdauer. Der **Decorator** kann Grabstätten um Zusatzleistungen wie "Dauergrabpflege" oder "Denkmalschutz-Status" erweitern.
*   **Architektur:** Achten Sie auf eine saubere Datenkapselung, insbesondere beim Umgang mit sensiblen Daten von Verstorbenen und Hinterbliebenen.

## 12. Parkausweis-System
*   **Szenario:** Prüfung und Ausstellung von Bewohnerparkausweisen. Das System validiert den Wohnsitz (Melderegister-Check) und den Fahrzeugbesitz (Halter-Check) über Schnittstellen.
*   **Fachliche Herausforderung:** Abgleich von Daten aus verschiedenen Quellen und die Durchsetzung von Regeln (nur ein Ausweis pro Person, Fahrzeug darf nicht über 3,5t wiegen).
*   **Muster-Empfehlungen:** Der **Adapter** harmonisiert die Schnittstellen zum Melderegister und zum Kraftfahrt-Bundesamt. Das **Strategy Pattern** entscheidet über die Gültigkeitsdauer basierend auf dem gewählten Tarifmodell.
*   **Architektur:** Nutzen Sie das *Dependency Inversion Principle (DIP)*, um die konkreten Schnittstellen-Adapter gegen abstrakte Interfaces auszutauschen.

## 13. Schulanmelde-Portal
*   **Szenario:** Koordination der Anmeldungen für Grundschulen oder weiterführende Schulen. Eltern geben Wunschschulen an; das System verteilt die Plätze nach Kapazität und festgelegten Kriterien.
*   **Fachliche Herausforderung:** Handling von Überbuchungen an "beliebten" Schulen und die Berücksichtigung von Härtefällen oder Geschwisterkindern bei der automatisierten Zuweisung.
*   **Muster-Empfehlungen:** Das **State Pattern** bildet den Status der Anmeldung ab (Eingereicht, In Prüfung, Zugewiesen, Abgelehnt). Das **Strategy Pattern** erlaubt den Wechsel zwischen verschiedenen Verteilungsschlüsseln.
*   **Architektur:** Entkoppeln Sie die Logik der Platzvergabe von der Persistenzschicht (Datenbank), um verschiedene Szenarien simulieren zu können.

## 14. Sperrmüll-Logistik
*   **Szenario:** Bürger buchen online Abholtermine für Sperrmüll. Das System berechnet Gebühren basierend auf der Menge (Kubikmeter) und plant die Abholzonen für die Entsorgungsfahrzeuge.
*   **Fachliche Herausforderung:** Optimierung der Tourenplanung basierend auf Postleitzahlengebieten und Kapazitätsgrenzen der Fahrzeuge pro Tag.
*   **Muster-Empfehlungen:** Das **Strategy Pattern** kapselt die Logik für die Routenoptimierung. Der **Observer** sendet Erinnerungen (E-Mail/SMS) am Tag vor der Abholung an die Bürger.
*   **Architektur:** Trennen Sie die Logistik-Domäne (Touren, Fahrzeuge) strikt von der Abrechnungs-Domäne (Gebühren, Bescheide).

## 15. Integrationskurs-Verwaltung
*   **Szenario:** Verwaltung von Sprach- und Integrationskursen an einer Volkshochschule. Tracking von Anwesenheiten, Verwaltung von Fördermitteln (BAMF) und Ausstellung von Zertifikaten.
*   **Fachliche Herausforderung:** Komplexität der Abrechnung mit Bundesbehörden basierend auf tatsächlicher Anwesenheit und die Überwachung von Fehlzeiten-Grenzwerten.
*   **Muster-Empfehlungen:** Der **Observer** schlägt Alarm, wenn ein Teilnehmer zu viele Fehlstunden akkumuliert. Die **Factory Method** erstellt je nach Kurstyp und Erfolg das passende Abschlusszertifikat.
*   **Architektur:** Nutzen Sie das *Interface Segregation Principle*, um unterschiedliche Sichten (Lehrer, Verwaltung, Behörde) auf die Kursdaten zu realisieren.

---

## Bewertungskriterien

Ihre Abgabe wird nach folgenden vier Säulen bewertet:

1.  **Code-Qualität & Clean Code:**
    *   Sinnvolle Benennung (Variablen, Methoden, Klassen).
    *   Einhaltung von C#-Konventionen.
    *   Vermeidung von Redundanz (DRY-Prinzip).
2.  **Anwendung von Design Patterns:**
    *   Mindestens zwei der behandelten Muster (Woche 2 & 3) müssen sinnvoll integriert sein.
    *   Die Implementierung muss die Probleme lösen, für die das Pattern gedacht ist (kein "Overengineering").
3.  **Testing & Validierung:**
    *   Unit-Tests für die Kern-Logik (z.B. Berechnungen, Statusübergänge).
    *   Nachweis der Lauffähigkeit durch eine Konsolenanwendung oder ein einfaches UI.
4.  **Architektur & Dokumentation:**
    *   Einhaltung der SOLID-Prinzipien.
    *   Klare Trennung der Verantwortlichkeiten (Schichten).
    *   Kurze Begründung der Pattern-Wahl in einer README-Datei.
