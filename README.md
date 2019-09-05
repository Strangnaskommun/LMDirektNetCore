# LMDirektNetCore

Klient för hämtning av fastighetsinformation från Lantmäteriets Direkttjänster 

## Användning

### `GET`

### Ett objekt

    /lmdirect?objektids={objektidentitet}

### Flera objekt

    /lmdirect?objektids={objektidentitet_objektidentitet}

### `POST`

### Filter

    /lmdirect/filter?json={"objektids":["objektidentitet","objektidentitet"],"categories":["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16"]}

### Autentiseringsuppgifter

    Projekt/Network/Creds.cs

### Aktdirekt proxy url


    Projekt/Views/LMDirekt/Index.cshtml {string aktdirektproxybase}

_Exempelvis /origoserver/document/index.djvu?archive=04&id=_

## Copyright
The project is licensed under the MIT license. It is specified in the [license file](LICENSE.txt).
