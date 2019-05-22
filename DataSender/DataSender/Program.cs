using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CSRedis;
using Newtonsoft.Json;

namespace DataSender
{
    class Program
    {

        

        static async Task Main(string[] args)
        {
            // configure Redis
            

            var redis = new RedisClient("127.0.0.1");

            while (true)
            {
                // read from Redis queue
                Console.WriteLine(redis.BLPop(30, "sensors_data"));

                // send value to remote API
                // TODO...

                using (var client = new HttpClient())
                {
                    //This would be the like http://www.uber.com
                    client.BaseAddress = new Uri("http://127.0.0.2:3000");
                    //serialize your json using newtonsoft json serializer then add it to the StringContent
                    var content = new StringContent(redis.BLPop(30, "sensors_data"), Encoding.UTF8, "application/json");
                    //method address would be like api/callUber:SomePort for example
                    var result = await client.PostAsync("http://127.0.0.2:3000/api/test/", content);
                    string resultContent = await result.Content.ReadAsStringAsync();

                }


            }
        }
    }
}
