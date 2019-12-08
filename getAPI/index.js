const fastify = require('fastify')({
  logger: true,
  ignoreTrailingSlash: true
});


fastify.register(require('fastify-cors'));

const sql = require('mssql');
const bcrypt = require("bcrypt");

fastify.register(require('fastify-jwt'), {
  secret: 'supersecret'
})

const Influx = require('influx');
const fs = require('fs');

var config = require("./config")()
//console.log(config.port);
//console.log(config.database);
//console.log(config.host);

var configApi = JSON.parse(fs.readFileSync('config/configApi.json', 'utf8'));
//console.log(configApi.user);
//console.log(configApi.password);
//console.log(configApi.server);
//console.log(configApi.database);


const influx = new Influx.InfluxDB({
  host: config.host,
  database: config.database,
  port: config.port
})



//Identificazione tramite token

fastify.post('/token', async (request, reply) => {
  try {
    let pool = await sql.connect(configApi);
    let model = request.body;
    var response = await pool.request().query(`select Hash from dbo.Login where Username = '${model.User}';`)
    if (model.Hash == response.recordset[0].Hash) {
      var user = {
        id: 1,
        user: "nextpathgroup"
      };
      const token = fastify.jwt.sign({ payload: user });
      reply.send(token);
    }
    else {
      reply.status(404).send({
        "Error": "Lo Username o la Password sono errati"
      });
    }

  }
  catch (err) {
    reply.send(err);
  }
});


fastify.register(async function (fastify, opts) {
  fastify.addHook('preHandler', async (request, reply) => {
    try {
      let model = request.body;
      await request.jwtVerify(model.Token);
    }
    catch (err) {
      reply.send(err);
    }
  });




  // Invio dati a InfluxDB

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
        reply.status(403).send();
    })

    reply.status(202).send();

});




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