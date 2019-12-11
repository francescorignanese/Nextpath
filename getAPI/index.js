
const Influx = require('influx');

var config = require("./config")()
//console.log(config.port);
//console.log(config.database);
//console.log(config.host);



const influx = new Influx.InfluxDB({
  host: config.host,
  database: config.database,
  port: config.port
})



const mqtt = require ('mqtt');
var client  = mqtt.connect('mqtt://127.0.0.1');

client.on('connect', function () {
    client.subscribe('/Bus');
    console.log('client has subscribed successfully');
  });

  client.on('message', function (topic, message){
    console.log(message.toString()); //if toString is not given, the message comes as buffer
    Inserisci();
    var dati = message.toString();

    //Se non è una fermata vengono inviati solo i dati di posizione
    if (dati.stop == 'False') {
      influx.writePoints([
        {
          measurement: 'posizioneBus',
          tags: { busId: dati.bus_id },
          measurement: { latitudine: dati.latitudine, longitudine: dati.longitude }
        }
      ]);
    }

    //Se è una fermata, oltre ai dati di posizione, vengono inviati anche quelli relativi alle persone che salgono e scendono e il numero totale di passeggeri a bordo
    else {
      influx.writePoints([
        {
          measurement: 'statoPorte',
          tags: { busId: dati.bus_id },
          fields: { fermata: dati.stop, latitudine: dati.latitudine, longitudine: dati.longitude, personeIn: dati.people_enter, personeOut: dati.people_left, personeTot: dati.counting }
        }
      ]);
    }
	
  });
  
  
