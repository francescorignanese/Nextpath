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
            Console.WriteLine("Quanti autobus vuoi gestire? ");
            int nBus = Convert.ToInt32(Console.ReadLine());
            Thread[] buses = new Thread[nBus];
            for (int i = 0; i < nBus; i++)
            {
                buses[i] = new Thread(Bus);
                buses[i].Start();
                Thread.Sleep(100);

            }




        }

        public static void Bus()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[5];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var bus_id = new String(stringChars);

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
                    /*Console.WriteLine(sensor.GetType());
                    // get current sensor value
                    var data = sensor.ToJson();
                    Console.WriteLine(data);

                    // push to redis queue
                    redis.LPush("sensors_data", data);

                    // wait 1 second
                    System.Threading.Thread.Sleep(100);*/

                    //DataReader.Sensors.VirtualCoordinatesSensor
                    //DataReader.Sensors.VirtualStopsSensor

                    aux_type = Convert.ToString(sensor.GetType());
                    if (aux_type == "DataReader.Sensors.VirtualCoordinatesSensor")
                    {
                        aux_value = sensor.ToJson();
                    }
                    else
                    {
                        var data = "{\"bus_id\":\"" + bus_id + "\"," + aux_value + sensor.ToJson();
                        Console.WriteLine(data);
                        redis.LPush("sensors_data", data);
                        System.Threading.Thread.Sleep(10000);
                    }




                }



            }
        }
    }
}
