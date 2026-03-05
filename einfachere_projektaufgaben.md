# Einfache Projektaufgaben (2-Wochen-Sprint)

Diese Aufgaben sind speziell für einen Bearbeitungszeitraum von **zwei Wochen** konzipiert. Sie konzentrieren sich auf die Kernlogik und eine saubere Struktur, ohne sich in technischen Details zu verlieren.

**Wichtig:** Konzentriere dich auf die drei beschriebenen Anwendungsfälle. Nutze eine einfache Konsolenausgabe (Console.WriteLine).

---

## 1. Mini-Wohngeldrechner
*   **Szenario:** Ein Programm, das prüft, ob jemand Anspruch auf Wohngeld hat und wie hoch dieser (vereinfacht) ist.
*   **Use Case 1:** Eingabe von Haushaltsgröße und Monatseinkommen.
*   **Use Case 2:** Berechnung des Anspruchs: Wenn Einkommen unter 1500€ -> "Anspruch", sonst "Kein Anspruch".
*   **Use Case 3:** Ausgabe eines einfachen "Bescheids" auf dem Bildschirm.
*   **Pattern-Tipp:** Nutze das **Strategy Pattern** für zwei einfache Regeln: "Städtische Tabelle" vs. "Ländliche Tabelle" (unterschiedliche Grenzwerte).

## 2. Einfache Kfz-Verwaltung
*   **Szenario:** Ein System, um ein Auto anzumelden und ein Kennzeichen zu vergeben.
*   **Use Case 1:** Erfassen eines Fahrzeugs (Marke, Kennzeichen-Wunsch).
*   **Use Case 2:** Prüfung, ob das Kennzeichen noch frei ist (einfache Liste durchsuchen).
*   **Use Case 3:** Status des Autos von "Abgemeldet" auf "Zugelassen" setzen.
*   **Pattern-Tipp:** Nutze das **State Pattern** für die zwei Zustände: `Zugelassen` und `Abgemeldet`.

## 3. Kita-Punkt-Prüfer
*   **Szenario:** Ein Tool, das berechnet, wie viele Prioritäts-Punkte ein Kind für einen Kita-Platz erhält.
*   **Use Case 1:** Eingabe von Daten: Alter des Kindes und "Beide Eltern berufstätig?" (Ja/Nein).
*   **Use Case 2:** Punktevergabe: +5 Punkte für Berufstätigkeit, +2 Punkte wenn das Kind über 2 Jahre alt ist.
*   **Use Case 3:** Anzeige der Gesamtpunktzahl und der Priorität (Hoch/Niedrig).
*   **Pattern-Tipp:** Nutze eine **Factory**, um verschiedene "Antrags-Typen" zu erstellen (z.B. U3-Antrag oder Ü3-Antrag).

## 4. Bürgeramt-Termin-Check
*   **Szenario:** Ein kleiner Planer, der prüft, ob ein Termin für einen neuen Personalausweis frei ist.
*   **Use Case 1:** Anzeigen von drei festen Terminslots (z.B. 08:00, 09:00, 10:00).
*   **Use Case 2:** Einen Slot als "Gebucht" markieren.
*   **Use Case 3:** Versuchen, einen bereits gebuchten Slot erneut zu buchen (Fehlermeldung ausgeben).
*   **Pattern-Tipp:** Nutze das **Observer Pattern**, um eine Meldung auszugeben: "Kalender wurde aktualisiert!".

## 5. Einfaches Fundbüro
*   **Szenario:** Eine Liste von verlorenen Gegenständen verwalten.
*   **Use Case 1:** Neuen Fundgegenstand eingeben (Name, Farbe).
*   **Use Case 2:** Suche nach einem Begriff (z.B. "Schlüssel") in der Liste.
*   **Use Case 3:** Einen Gegenstand als "Abgeholt" aus der Liste entfernen.
*   **Pattern-Tipp:** Nutze das **Strategy Pattern** für die Suche: "Suche nach Name" oder "Suche nach Farbe".

## 6. Bauantrag-Light
*   **Szenario:** Verfolgung eines Bauantrags durch drei einfache Stufen.
*   **Use Case 1:** Antrag anlegen (Bauherr, Vorhaben).
*   **Use Case 2:** Den Antrag eine Stufe weiterbewegen (z.B. von "Eingereicht" zu "In Prüfung").
*   **Use Case 3:** Den Antrag final auf "Genehmigt" setzen.
*   **Pattern-Tipp:** Nutze das **State Pattern** für die Zustände: `Eingereicht`, `Pruefung`, `Genehmigt`.

## 7. Hundesteuer-Rechner
*   **Szenario:** Berechnung der jährlichen Steuer für einen Hund.
*   **Use Case 1:** Hund erfassen (Name, Rasse).
*   **Use Case 2:** Steuer berechnen: Normaler Hund = 100€, Gefährlicher Hund = 500€.
*   **Use Case 3:** Anzeige des fälligen Betrags.
*   **Pattern-Tipp:** Nutze das **Strategy Pattern** für die zwei Tarife: `NormalTarif` und `GefahrenTarif`.

## 8. Wahlhelfer-Liste
*   **Szenario:** Freiwillige für ein Wahllokal eintragen.
*   **Use Case 1:** Person mit Name und Funktion (Beisitzer/Vorstand) registrieren.
*   **Use Case 2:** Anzeige aller Personen, die für "Wahllokal A" eingetragen sind.
*   **Use Case 3:** Prüfung: Sind mindestens 3 Personen eingetragen? (Ja/Nein).
*   **Pattern-Tipp:** Nutze eine **Factory**, um Personen-Objekte mit der richtigen Funktion zu erstellen.

## 9. Einfacher Mängelmelder
*   **Szenario:** Bürger melden einen Mangel (z.B. "Schlagloch").
*   **Use Case 1:** Mangel erfassen (Beschreibung, Ort).
*   **Use Case 2:** Status ändern von "Offen" auf "In Arbeit".
*   **Use Case 3:** Kurze Bestätigung an den Bürger ausgeben ("Wir kümmern uns!").
*   **Pattern-Tipp:** Nutze das **Observer Pattern**, um eine Nachricht zu "versenden", wenn der Status sich ändert.

## 10. Gewerbe-Check
*   **Szenario:** Basis-Daten für eine Gewerbeanmeldung prüfen.
*   **Use Case 1:** Name und Rechtsform (z.B. "Einzelunternehmen") eingeben.
*   **Use Case 2:** Pflichtfeld-Check: Ist der Name länger als 3 Zeichen?
*   **Use Case 3:** Bestätigung der Anmeldung ausgeben.
*   **Pattern-Tipp:** Nutze das **Strategy Pattern** für die Prüfung: `CheckEinzelunternehmen` vs. `CheckGmbH`.

## 11. Friedhof-Fristen-Check
*   **Szenario:** Prüfen, ob eine Grabnutzung abläuft.
*   **Use Case 1:** Grabnummer und Jahr der Bestattung eingeben.
*   **Use Case 2:** Berechnung: Wenn (Aktuelles Jahr - Bestattungsjahr) > 20 -> "Ablauf".
*   **Use Case 3:** Liste aller abgelaufenen Gräber anzeigen.
*   **Pattern-Tipp:** Nutze das **Strategy Pattern** für: `Urnengrab` (15 Jahre) vs. `Erdgrab` (20 Jahre).

## 12. Parkausweis-Prüfer
*   **Szenario:** Darf ein Bürger einen Anwohnerparkausweis bekommen?
*   **Use Case 1:** Abfrage: "Wohnhaft im Bezirk?" (Ja/Nein).
*   **Use Case 2:** Abfrage: "Fahrzeuggewicht über 3.5 Tonnen?" (Ja/Nein).
*   **Use Case 3:** Ergebnis: "Ausweis genehmigt" oder "Abgelehnt".
*   **Pattern-Tipp:** Nutze das **Strategy Pattern** für `PrivatPKW` vs. `LKW`.

## 13. Schul-Platz-Verteiler
*   **Szenario:** Ein Kind einer Schule zuweisen.
*   **Use Case 1:** Name des Kindes und Wunsch-Schule eingeben.
*   **Use Case 2:** Prüfen, ob in der Wunsch-Schule noch Plätze frei sind (einfacher Zähler).
*   **Use Case 3:** Kind der Liste der Schule hinzufügen.
*   **Pattern-Tipp:** Nutze das **Singleton Pattern** für die Verwaltung der Schul-Kapazitäten.

## 14. Sperrmüll-Rechner
*   **Szenario:** Kosten für die Sperrmüllabholung schätzen.
*   **Use Case 1:** Anzahl der Möbelstücke eingeben.
*   **Use Case 2:** Kosten berechnen: 5€ pro Stück, aber mindestens 20€ Grundgebühr.
*   **Use Case 3:** Terminbestätigung ausgeben.
*   **Pattern-Tipp:** Nutze das **Strategy Pattern** für `Privathaushalt` vs. `Gewerbe`.

## 15. Kurs-Teilnehmer-Liste
*   **Szenario:** Anwesenheit in einem Sprachkurs tracken.
*   **Use Case 1:** Teilnehmer zur Liste hinzufügen.
*   **Use Case 2:** Fehlstunden für einen Teilnehmer eintragen.
*   **Use Case 3:** Warnung ausgeben, wenn Fehlstunden > 10.
*   **Pattern-Tipp:** Nutze das **Observer Pattern**, um die Warnung automatisch auszulösen.

---

## Tipps für den 2-Wochen-Sprint
1.  **Halte es einfach:** Keine Datenbank, keine grafische Oberfläche. Nutze Listen (`List<T>`) im Speicher.
2.  **Muster zuerst:** Überlege dir zuerst, wie du das Design Pattern einbaust, bevor du den Rest programmierst.
3.  **Testen:** Schreib einen kleinen Test für die Berechnung (z.B. in der `Main`-Methode).
