using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataReader.Sensors;
using CSRedis;
using System.Threading;

namespace DataReader
{
	class Program
	{
		static void Main(string[] args)
		{
            string autobusTarga = System.Configuration.ConfigurationSettings.AppSettings["AutobusTarga"];

            // init sensors
            List<ISensor> sensors = new List<ISensor>
                {
                     new VirtualCoordinatesSensor(),
                     new VirtualStopsSensor()
                };

            // configure Redis
            string redisIp = System.Configuration.ConfigurationSettings.AppSettings["RedisIP"];
            var redis = new RedisClient(redisIp);

            string aux_type = "";
            string aux_value = "";

            while (true)
            {
                foreach (ISensor sensor in sensors)
                {                  

                    aux_type = Convert.ToString(sensor.GetType());

                    if (aux_type == "DataReader.Sensors.VirtualCoordinatesSensor")
                    {
                        aux_value = sensor.ToJson();
                    }
                    else
                    {
                        var data = "{\"bus_id\":\"" + autobusTarga + "\"," + aux_value + sensor.ToJson();
                        Console.WriteLine(data);
                        redis.LPush("sensors_data", data);
                        System.Threading.Thread.Sleep(10000);
                    }
                }
            }
        }
							
		
	}
}
