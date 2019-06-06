# Transport Right

Transport Right è un applicazione atta al rilevamento e monitoraggio di mezzi di trasporti pubblici. L’applicazione è in grado di ricevere dati di posizionamento (GPS) e dati provenienti da sensori (apertura porte, conta persone) agganciati a dispositivi presenti a bordo dei mezzi.

## Getting Started

Scaricare il contenuto del progetto.

### Prerequisites

- Visual Studio Code
- Visual Studio 2017/2019
- Redis (Docker)
- InfluxDB

## Running the codes

- Eseguire da prompt "docker run --name pw-redis -p 6379:6379 -d redis" per creare il database redis su docker (solo l aprima volta).
- Avviare il database da prompt "docker start pw-redis".
- Aprire con Visual Studio Code l'API e scaricare nel file package.json le seguenti librerie di fastify e influx utilizzando il comando npm install.
- Eseguire tramite node.js il main.js dell'API.
- Eseguire il codice del DataReader su Visual Studio prestando attenzione ad aver installato il pacchetto NuGet "csredis".
- Avviare DataSender utilizzando Visual Studio.
- Collegarsi all'indirizzo locale per visualizzare il database Influx da browser.




## Authors

* **Alessandro Momesso**
* **Andrea Filippi**
* **Filippo De Marchi**
* **Francesco Rignanese**
* **Marco Zucchetto**

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details


