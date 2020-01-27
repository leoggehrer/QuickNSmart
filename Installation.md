# Installation
Diese Anleitung dient als Unterst�tzung zur Inbetriebnahme des System und beschreibt in Schritten jede einzelene Aktion.  
## Daten
Die Basisdaten liegen im csv-Format vor und sind im Projekt 'QuickNSmart.Logic' im Verzeichnis 
'CsvData' verf�gbar.
## Vorbereitungen
F�r die Verwendung der Datenbank (als Persistierungsschicht) m�ssen einige Voraussetzungen gegeben 
sein und einige Vorbereitungsschritte durchgef�hrt werden. Beachten Sie dazu den entsprechenden 
Abschnitt in diesem Dokument.  
F�r alle anderen Persistierungsarten, wie die Speicherung der Daten im csv-Format und die Speicherung 
der Daten mittels Serialisierung, m�ssen keine Vorbereitungsaktionen durchgef�hrt werden.
## Vorbereitung der Datenbank
Damit der Betrieb der Anwendung mit einer Datenbank funktioniert ist eine entsprechende Ausf�hrungsumgebung 
erforderlich. Zur Herstellung diese Umgebung beachten Sie bitte die beiden nachfolgenden Abschnitte.
### System-Voraussetzungen
+ Es muss eine Datenbank-Instanz installiert und ausgef�hrt werden. Im Standardfall wird bei der 
Installation von Visual Studio eine 'LocalDb' mitinstalliert. Es wird davon ausgegangen, dass 
diese bereits installiert und asgef�hrt wird. 
+ F�r den lokalen Rechner m�ssen die entsprechenden Rechte definiert sein. Diese werden ebenfalls 
mit der Standard-Installation von Visual Studio definiert.
+ Falls eine oder mehrere Voraussetzungen fehlen, m�ssen diese nachgeholt werden.

### Projekt-Voraussetzungen
Damit ein Betrieb mit einer Datenbank m�glich ist, m�ssen einige NuGet-Packages den Projekten hinzugef�gt werden. Im nachfolgenden sind die Packages f�r die einzelnen Projekte aufgef�hrt:

+ **QuickNSmart.Logic**
  + Microsoft.EntityFrameworkCore
  + Microsoft.EntityFrameworkCore.SqlServer
  + Microsoft.EntityFrameworkCore.Tools

+ **QuickNSmart.ConApp**
  + Microsoft.EnttyFrameworkCore.Design

### Erzeugen der Datenbank
+ **Schritt 1**  
Stellen Sie sicher, dass es kein Migrationsverzeichnis, im Projekt 'QuickNSmart.Logic', gibt. Wenn ja, bitte l�schen Sie dieses vollst�ndig.
+ **Schritt 2**  
�berpr�fen Sie, ob die gesamte Projektmappe vollst�ndig und ohne Fehler �bersetzt werden kann. 
Wenn dies nicht der Fall ist, dann treffen Sie alle notwendigen Ma�nahmen damit die Projektmappe 
ohne Fehler �bersetzt werden kann.
+ **Schritt 3**  
Wenn Sie den Namen der Datenbank �ndern wollen, dann k�nnen Sie den Namen in der Klasse 
'DbQuickNSmartContext' einstellen. �berpr�fen Sie, dass nicht bereits eine Datenbank mit dem gleichem Namen existiert.
+ **Schritt 4**  
 Legen Sie im Visual Studio das Startprojekt 'QuickNSmart.ConApp' fest.
+ **Schritt 5**  
�ffnen Sie im Visual Studio die 'Package Management Console' und stellen Sie das 'Default project' auf 
'QuickNSmart.Logic' ein.
+ **Schritt 6**  
Geben Sie in der 'Package Management Console' den folgenden Befehl ein:  
Add-Migration InitDb  
Anschlie�end wird ein Ordner mit der Bezeichnung 'Migrations' und den ntsprechenden Dateien erstellt.  
+ **Schritt 7**  
Geben Sie in der 'Package Management Console' den folgenden Befehl ein:  
Update-Database  
Anschlie�end wird die Datenbank erstellt und Sie k�nnen diese mit der Ansicht 
'SQL Server Object Explorer' �berpr�fen.
