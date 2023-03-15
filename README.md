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

* AddressEntity: Denna entitet beskriver en adress som en ägare har. En ägare kan ha flera adresser men bara en adress
