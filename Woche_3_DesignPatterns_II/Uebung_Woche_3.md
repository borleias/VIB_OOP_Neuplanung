# Übungsaufgabe Woche 3: Strategy & Observer in der Verwaltung

## Aufgabe 1: Strategy - Variable Gebührenberechnung
Implementieren Sie ein System zur Berechnung von Parkgebühren in einer Stadtverwaltung.

**Anforderungen:**
1.  Erstellen Sie ein Interface `IParkGebuehrStrategie` mit einer Methode `BerechneParkGebuehr(int minuten)`.
2.  Implementieren Sie drei Strategien:
    -   `StandardParken`: 0.05€ pro Minute.
    -   `AnwohnerParken`: 0.01€ pro Minute.
    -   `ElektroAutoParken`: Die ersten 60 Minuten sind kostenlos, danach 0.03€ pro Minute.
3.  Erstellen Sie eine Klasse `ParkscheinAutomat`, die eine Strategie entgegennimmt und die Gebühr berechnet.

## Aufgabe 2: Observer - Antragsstatus-Benachrichtigung
Implementieren Sie ein System, das Bürger automatisch benachrichtigt, wenn sich der Status ihres Antrags ändert.

**Anforderungen:**
1.  Ein `Antrag` hat eine `ID`, einen `BuergerNamen` und einen `Status`.
2.  Erstellen Sie ein Interface `IBenachrichtigungsService` mit einer Methode `Informiere(Antrag antrag)`.
3.  Implementieren Sie zwei Benachrichtigungs-Services:
    -   `EmailService`: Gibt eine Konsolenmeldung aus ("E-Mail gesendet: Ihr Antrag #ID ist jetzt auf Status STATUS").
    -   `SmsService`: Gibt eine Konsolenmeldung aus ("SMS gesendet: Statusänderung bei Antrag #ID").
4.  Der `Antrag` sollte eine Liste von `IBenachrichtigungsService`-Objekten verwalten und alle benachrichtigen, sobald sich der Status ändert.

### Zusatzaufgabe (Optional):
Nutzen Sie C# `events` und `delegates` anstelle des "klassischen" Observer Patterns für Aufgabe 2.
