https://github.com/vahidtaleghani/MTCG.git

Dieses Projekt umfasst den Hauptteil des Verbindens und Ausführens des Programms. Die allgemeine Leistung der Klassen ist wie folgt:
- Richten sie die Anwendung und den Server ein
- Server- und Clientverbindung
- Reagieren sie auf Anfragen und Definieren sie die Definition der Fehlertypen
- Geben Sie den Pfad zum Ausführen des angeforderten Befehls an

Die Port Nummer 10001 wird als Verbindungsport spezifiziert und definiert.
Eine von wichtigem Teil dieses Projekt ist endpoints Verzeichnis, die alle Mothoden für die Einführung des Projekts gesammelt hat.
Endpoint Klasse enthält die beiden Hauptfunktionen canprocess und handleRequest,dann wird die empfangene Anforderung an den definierten Endpoint gesendet und diesen Funktionen werden überschrieben und ausgeführt.

Database:
Die Verarbeitung zwischen Server und Datenbank findet in dieser Ebene des Projekts im "data" ordner statt.
Innerhalb des Repository-Ordners haben wir CardReps, StateReps, TradeReps und UserReps, die alle von der Database-Klasse erben. Die Database-Klasse stellt eine Verbindung zur Datenbank her und erhält die String-Verbindungsparameter von der Settings-Klasse.
- Das UserReps ist verantwortlich für die Authentifizierung eines Benutzers bei der Anmeldung am Server, das Abrufen von coin, die Aktualisierung des Benutzersatzes und das Hinzufügen von Benutzern. 
- Das CardReps ist Verantwortlich für die Speicherung von Karten, Empfang von Karteninformationen, die Feststellung der Bereitschaft der Karten des Benutzers zur Teilnahme am Wettbewerb, die Feststellung und änderung des Karteninhabers.
- Das StateReps ist Verantwortlich für die Ankündigung von Benutzerpunkten und das Ändern von Informationen nach Gewinnen und Verlusten oder Unentschieden im Spiel.
- Das TradeReps ist Verantwortlich für die Aufbewahrung von Karten zum Verkauf.

Zusatzfunktion:
- Wenn ein Spieler ohne 4 Karten auf dem Deck am Turnier teilnehmen möchte, erhält er eine Fehlermeldung und kann nicht ins Spiel einsteigen.
- Wenn der Spieler für das Spiel bereit ist und nach 15 Sekunden ein anderer Spieler nicht bereit ist, wird diese Nachricht empfangen und hinterlassen.
- Es wurde auch eine Methode zum Löschen von Benutzern erstellt.

unit testing:
Unit Test für Battle Part und Database Repository geschrieben.Die Unit-Tests für das battle prüfen verschiedene Ergebnisse in Abhängigkeit von verschiedenen Eingaben und stellen sicher, dass das Projekt und die Kampflogik unter allen Umständen funktionieren, bevor es eingesetzt wird.
Unit-Tests dieser Methoden sind von entscheidender Bedeutung, da sie uns die Zeit ersparen, alle Möglichkeiten manuell zu prüfen, und sie machen das Projekt zuverlässig. Sie helfen uns auch, uns alle möglichen Ergebnisse vorzustellen, so dass wir herausfinden können, ob der Code irgendeine Art von Fehler oder Inkonsistenzen hat.

Geschätzte Zeit:
 ___________________________________________________
| 						    |
| Datenbank-Design:                   ~ 5 hours     |
| Entwurf einer Projektanalyse:       ~ 15 hours    |
| Http-Server-Klassen:                ~ 10 hours    |
| Battle Logic implementieren:        ~ 20 hours    |
| Implementieren Repository-Klassen:  ~ 25 hours    |
| Datenbankimplementierungen:         ~ 10 hours    |  
| unit testing:                       ~ 5 hours     |
| Debuggen aller Arten von Fehlern!:  ~ 10 hours    |
| Tutorial sehen   		      ~ 10 hours    |                
| total time                          ~ 110 hours   |
|___________________________________________________|