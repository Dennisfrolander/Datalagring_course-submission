<h1 align="center"> Ärendehanteringssystem(Code-First) | Datalagring Inlämning </h1>
<h4 align="center">Av Dennis Frölander</h4>

## Hur man startar programmet:
***Ändra din _connectionstring i Contexts > DataContext*** 

```private readonly string _connectionString = @"Din ConnectString"; ```

***Se till att du lagt till en connection mellan sqlserver och din lokaladatabas som finns i Contexts mappen (Local_Property_Db.mdf).***

#

### Vill du skapa en egen databas med hjälp av Code-First bör du lägga till dessa två Query:

```
INSERT INTO Positions
VALUES ('Fastighetsförvaltare'), ('Uthyrningsansvarig'), ('Fastighetsekonom'), ('Underhållstekniker');
```

```
INSERT INTO Statuses
VALUES ('Ej påbörjad'), ('Pågående'), ('Avslutad');
```


<h3  align="center">ER-Diagram för databasen</h3>
<div  align="center">
<img src ="https://i.gyazo.com/101d54708b9d29e4bf346098acdd9828.png">
</div>

## Relationer:

* Addresses: Denna tabell beskriver en adress som kan tillhöra en eller flera ägare. Det är möjligt att flera ägare delar på samma adress, men varje adress kan endast vara kopplad till en enskild ägare i taget.

* Owners: Denna tabell beskriver en ägare/kund som kan vara kopplad till en eller flera adresser. En ägare kan ha flera adresser, men varje ägare kan endast vara kopplad till en enskild adress i taget. Det är också möjligt att en ägare kan skapa flera ärenden, men varje ärende kan bara vara kopplat till en enskild ägare i taget.

* Issues: Denna tabell beskriver ett ärende som skapas av en ägare. Ett ärende kan endast vara kopplat till en enskild ägare i taget, men en ägare kan skapa flera ärenden. Tabellen har också en till många-relation med Statuses, vilket innebär att flera ärenden kan ha samma status men en status kan endast vara kopplad till ett enskilt ärende. Den har också en till många-relation med Comments, vilket innebär att ett ärende kan ha flera kommentarer, men varje kommentar kan endast vara kopplad till ett enskilt ärende i taget.

* Statuses: Denna tabell innehåller olika typer av statusar som kan vara kopplade till ett eller flera ärenden. En status kan vara kopplad till flera ärenden, men varje ärende kan endast ha en enskild status i taget.

* Comments: Denna tabell beskriver en kommentar som kan vara kopplad till ett enskilt ärende. Ett ärende kan ha flera kommentarer, men varje kommentar kan endast vara kopplad till ett enskilt ärende i taget. Tabellen har också en till många-relation med Employees, vilket innebär att en anställd kan ha skrivit flera kommentarer, men varje kommentar kan endast vara kopplad till en enskild anställd i taget.

* Employees: Denna tabell beskriver en anställd. Den har en till många relation med comments, flera kommentarar kan skrivas av en anställd men bara en kommentar kan innehålla en anställd. Den har även många till en relation med positioner, flera anställda kan ha samma positon men en anställd kan bara ha en position.

* Positions: Denna tabell beskriver olika arbetstitlar, den har en till många relation med anställda, en position kan ha flera anställda men bara en anställd kan kopplas till en position.
