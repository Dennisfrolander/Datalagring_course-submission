<h1 align="center"> Ärendehanteringssystem | Datalagring Inlämning </h1>
<h4 align="center">Av Dennis Frölander</h4>

## Hur man startar programmet:
***Ändra din _connectionstring i Contexts > DataContext*** 

```private readonly string _connectionString = @"Din ConnectString"; ```

***Se till att du lagt till en connection mellan sqlserver och din lokaladatabas som finns i Contexts mappen (Local_Property_Db.mdf).***


<h3  align="center">ER-Diagram för databasen</h3>
<div  align="center">
<img src ="https://i.gyazo.com/101d54708b9d29e4bf346098acdd9828.png">
</div>

## Relationer:

* Addresses: Denna tabell beskriver en adress som en eller flera ägare har. En adress kan gå till flera ägare men bara en ägare kan kopplas till en adress.

* Owners: Denna tabellbeskriver en ägare / kund som kan kopplas med en adress. Den kan även kopplas till flera stycken issues men bara en issue kan kopplas med en ägare / kund. 
* Issues: Denna tabell beskriver ett ärende som kan skappas av en ägare. Ett ärende kan endast kopplas ihop med en ägare men en ägare kan kopplas ihop med flera ärende. 
Den innehåller även många till en relation med statuses, flera ärende kan kopplas till en och samma status men bara en status kan kopplas till ett ärende. Den har också en till många relation med comments då ett ärende kan ha många kommenterar men bara en kommentar kan kopplas till ett ärende.
* Statuses: Denna tabell innehåller olika typer av statusar som kopplas ihop med en till många relation med issues, en status kan gå till flera ärenden men bara ett ärende kan gå till en status.
* Comments: Denna tabell innehåller en kommentar som har många till en relation med ärenden. Ett ärende kan ha flera kommentarer men en kommentar kan bara kopplas med ett ärende. det finns även  många till en relation med employees, en employee kan kopplas till flera kommenterar men bara en kommentar kan kopplas till en employee.
Employees: Denna tabell innehåller information om en anställd, den har även  en till många relation med comments. Den har också en många till en relation med position för att en position kan finns i flera anställda medans en anställd kan bara ha en position.
