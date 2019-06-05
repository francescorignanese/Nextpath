/*
    ignoreTrailingSlash
    Fastify uses find-my-way to handle routing. 
    This option may be set to true to ignore trailing slashes in routes. 
    This option applies to all route registrations for the resulting server instance.
*/

// Require the framework and instantiate it

const fastify = require('fastify')({
    logger: true,
    ignoreTrailingSlash: true
});


/*
    Route Prefixing
    Sometimes you need to maintain two or more different versions of the same api, 
    a classic approach is to prefix all the routes with the api version number, /v1/user for example. 
    Fastify offers you a fast and smart way to create different version 
    of the same api without changing all the route names by hand, route prefixing.
*/

const Influx = require('influx');


//const influx = new Influx.InfluxDB('http://192.168.101.96:8086/mydb');

const influx = new Influx.InfluxDB({
    host: '192.168.101.109',
    database: 'nextpath',
    port: 8086
    //measurement: 'nextpath'
})



fastify.post('/api/test', async (request, reply) => {
    var dati = request.body;
    console.log("\n" + dati.bus_id + "\n");

    influx.writePoints([
        {
          measurement: 'transportright',
          tags: { busId: dati.bus_id, latitudine: dati.latitude, longitudine: dati.longitude },
          fields: { fermata: dati.stop, personeIn: dati.people_enter, personeOut: dati.people_left, personeTot: dati.counting },
        }
      ]).catch(err => {
        console.error(`Error saving data to InfluxDB! ${err.stack}`)
      })

    


    reply.status(202).send();

});

/*function esQuery(){
    influx.query(`select * FROM "nextpath"`)
    .then( result => console.log(result) )
    .catch( error =>  console.log({ error }) );
  
}*/

/*function salvaSuInflux() {
    
    //influx.query(`show databases`)
    /*influx.query('SELECT * FROM transportright')
    //influx.query('insert transportright,busId=5,latitudine=6,longitudine=7 personeIn=8,personeOut=9,personeTot=10')
        .then(result => {
            console.log(result)
        }).catch(err => {
            console.log(err);
        })*/



        /*influx.writePoints([
            {
              measurement: 'transportright',
              tags: { host: "aa444" },
              fields: {  path: "b6666" },
            }
          ]).catch(err => {
            console.error(`Error saving data to InfluxDB! ${err.stack}`)
          })
        
}*/






// Run the server!
const start = async () => {
    try {
        await fastify.listen(3000, "127.0.0.2")
        fastify.log.info(`server listening on ${fastify.server.address().port}`)
    } catch (err) {
        fastify.log.error(err)
        process.exit(1)
    }
}
start();