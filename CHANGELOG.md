# Changelog
All notable changes to this project will be documented in this file.

## [0.1.9] - 2019-12-02
### Changes
- Implementato l'invio separato dei dati all'interno delle API. Nel caso di una fermata vengono inviati al database i dati riguardanti la posizione della fermata, il numero di persone che salgono e scendono dal mezzo nonchè in numero totale di passeggeri a bordo. Nel caso, invece, in cui non si tratti di una fermata verranno inviati solo i dati relativi alla posizione dell'autobus.

## [0.1.8] - 2019-11-18
### Changes
- Aggiunta autenticazione tramite token nelle API e nuovo file di configurazione esterno chiamato configApi.

## [0.1.7] - 2019-06-05
### Changes
- Rimosso multithread e aggiunta la possibilità di inserire l'ID del bus manualmente nel file .config.

## [0.1.6] - 2019-06-05
### Added
- Sostituito il file connection.config con il file .config esterno chiamato settings.config per DataReader e DataSender.

## [0.1.5] - 2019-06-05
### Changes
- Corretto bug che generava dati duplicati.

## [0.1.4] - 2019-06-05
### Added
- Aggiunto multithread per poter raccogliere dati su più autobus.

## [0.1.3] - 2019-06-05
### Added
- Aggiunto file di configurazione connection.config per gestione degli indirizzi IP.

## [0.1.1] - 2019-05-22
### Changes
- Minor bug fixing.

## [0.1.0] - 2019-05-22
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


[Latest version]: https://github.com/Francesco-Rignanese/Nextpath.git





















