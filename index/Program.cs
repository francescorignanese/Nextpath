using Newtonsoft.Json;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using index.Strumenti;
using System.Net.NetworkInformation;
using ServiceStack.Redis;
using System.Net.Http.Headers;
using System.Net.Http;
using CSRedis;

namespace index
{
	class Program
	{
		static void Main(string[] args)
		{
			//string autobusTarga = System.Configuration.ConfigurationSettings.AppSettings["AutobusTarga"];

			string text = File.ReadAllText("D:\\ITS Kennedy\\Proget Work\\Repository\\Coordinate.geojson");
			dynamic array = JsonConvert.DeserializeObject(text);

			new VirtualStop();

			foreach (var item in array.geometry.coordinates)
			{
				model coordinate = new model()
				{
					Latitudine = item[1],
					Longitudine = item[0]
				};

				var data = JsonConvert.SerializeObject(coordinate);
				Console.WriteLine(data);

				string APIaddress = System.Configuration.ConfigurationSettings.AppSettings["address"];
				string redisIp = System.Configuration.ConfigurationSettings.AppSettings["RedisIP"];

				RedisClient redis = new RedisClient(redisIp);
				//Prova collegamento con il sito per la renderizzazione della mappa

				Ping pingSender = new Ping();

				PingReply reply = pingSender.Send(APIaddress);
				//Da controllare >>
				using (var client = new HttpClient())
				{
					string baseAddress = System.Configuration.ConfigurationSettings.configuration["baseAddress"];
					client.BaseAddress = new Uri(baseAddress);
				}
				//<<
				if (reply.Status == IPStatus.Success)
				{
					string x = redis.LPop("sensors_data");

					try
					{
						var result = await client.PostAsync(APIaddress, coordinate);
						string resultContent = await result.Content.ReadAsStringAsync();
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message + " invio non riuscito");
						redis.LPush("sensors_data", coordinate);
					}
				}
				else
				{
					//Salva i dati su redis se l host non è raggiungibile
					Console.WriteLine("host non raggiungibile");
					// push to redis queue
					redis.RPush("sensors_data", coordinate);
				}

				System.Threading.Thread.Sleep(10000);

			}
		}
	}
}
