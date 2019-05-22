# Changelog
All notable changes to this project will be documented in this file.

## [1.0.0] - 2019-05-22
### Added
- Creato DataSender dei dati.
- Creato DataReader dei dati.
- Creata API per gestione dei dati.
- Creato database Influx per raccolta dei dati.

### DataReader changes
- Implementata funzione di generazione dati di posizione casuali (latitudine e longitudine).
- Implementata funzione di generazione dati di apertura e chiusura porte.
- Implementata funzione di generazione dati del numero di persone che salgono e scendono dal mezzo.
- Collegamento DataSender con database di Redis per catalogazione dei dati.

### API changes
- Implementata funzione di ricezione dei dati dal database Redis.
- Implementata funzione di invio dati al database Influx.

### DataSender changes
- Implementata funzione di invio dati dal database Redis all'API.





[1.0.0]: https://github.com/Francesco-Rignanese/Nextpath.git

