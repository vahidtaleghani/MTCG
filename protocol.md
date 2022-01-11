https://github.com/vahidtaleghani/MTCG.git

Dieses Projekt umfasst den Hauptteil des Verbindens und Ausführens des Programms. Die allgemeine Leistung der Klassen ist wie folgt:
- Sie richten die Anwendung und den Server ein.
- Server- und Clientverbindung.
- Sie reagieren auf Anfragen und definieren die Fehlertypen.
- Sie geben den Pfad zum Ausführen des angeforderten Befehls an.

Die Port Nummer 10001 wird als Verbindungsport spezifiziert und definiert.
Eine von wichtigen Teils dieses Projekt ist endpoints Verzeichnis, die alle Mothoden für die Einführung des Projekts gesammelt hat.
Endpoint Klassen enthalten die zwei Hauptfunktionen "canProcess" und "handleRequest":
canProcess: überprüft empfangene Request(Path und Methode).  
handleRequest: führt die Überprüfte Request aus.

Database:
Die Verarbeitung zwischen Server und Datenbank findet im Data-Ordner statt.
Innerhalb des Repository-Ordners haben wir CardReps, StateReps, TradeReps und UserReps, die alle von der NpgsqlConn-Klasse erben. Die Database-Klasse stellt eine Verbindung zur Datenbank her und erhält die String-Verbindungsparameter von der Settings-Klasse.
- Das UserReps ist verantwortlich für die Authentifizierung eines Benutzers bei der Anmeldung am Server, das Abrufen von coins, die Aktualisierung von Informationen der Benutzer-inen und das Hinzufügen der Benutzer-inen.
- Das CardReps ist Verantwortlich für die Speicherung der Karten, Empfangen der Karteninformationen, die Feststellung der Bereitschaft der Karten des Benutzers zur Teilnahme am Wettbewerb, die Feststellung und änderung des Karteninhabers.
- Das StateReps ist Verantwortlich für die Ankündigung von Benutzerpunkten und das Ändern von Informationen nach Gewinnen und Verlusten oder Gleichstellung im Spiel.
- Das TradeReps ist Verantwortlich für Trading der Karten.

Zusatzfunktion:
- Wenn ein Spieler ohne 4 Karten auf dem Deck am Battle teilnehmen möchte, erhält er eine Fehlermeldung und kann nicht ins Spiel einsteigen.
- Wenn der Spieler für das Spiel bereit ist und nach 15 Sekunden ein anderer Spieler nicht bereit ist, wird diese Nachricht "No User ready to play" empfangen und hinterlassen.
- Es wurde auch eine Methode zum Löschen der Benutzer-inen erstellt.

Unit-Tests:
Unit-Tests wurden für Battle Part und Database Repository geschrieben.Die Unit-Tests für das battle prüfen verschiedene Ergebnisse in Abhängigkeit von verschiedenen Eingaben und stellen sicher, dass das Projekt und die Kampflogik unter allen Umständen funktionieren, bevor es eingesetzt wird.
Unit-Tests dieser Methoden sind von entscheidender Bedeutung, da sie uns die Zeit ersparen, alle Möglichkeiten manuell zu prüfen, und sie machen das Projekt zuverlässig. Sie helfen uns auch, uns alle möglichen Ergebnisse vorzustellen, so dass wir herausfinden können, ob der Code irgendeine Art von Fehler oder Inkonsistenzen hat.
Insgesamt habe ich 31 Test geschrieben.

Was ich gelernt habe:
- Organisierung und Planung vor dem Start des Projekts 
- Http Request and Response Methode
- Unit- und Integrationstest
- Database in C#

Geschätzte Zeit:
 ___________________________________________________
| 						    |
| Datenbank-Design:                   ~ 5 hours     |
| Projektanalyse:     		      ~ 15 hours    |
| Http-Server-Klassen:                ~ 10 hours    |
| Battle Logic implementieren:        ~ 20 hours    |
| Implementieren Repository-Klassen:  ~ 25 hours    |
| Datenbankimplementierungen:         ~ 10 hours    |  
| Unit-Test:                          ~ 5 hours     |
| Debuggen Fehlern:                   ~ 10 hours    |
| Tutorial sehen   		      ~ 10 hours    |                
| total time                          ~ 110 hours   |
|___________________________________________________|