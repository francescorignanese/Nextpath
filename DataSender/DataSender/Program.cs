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
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace DataSender
{
	class Program
	{

		static async Task Main(string[] args)
		{
			// configure Redis

			string redisIp = System.Configuration.ConfigurationSettings.AppSettings["RedisIP"];
			var redis = new RedisClient(redisIp);

			while (true)
			{
				
				Console.WriteLine(redis.BLPop(30, "sensors_data"));
				//var content = new StringContent(redis.BLPop(30, "sensors_data"), Encoding.UTF8, "application/json");
				string BrokerAddress = "192.168.101.67";

				MqttClient client = new MqttClient(BrokerAddress);


				string clientId = Guid.NewGuid().ToString();
				client.Connect(clientId);

				string Topic = "/Bus";

				// publish a message on "/home/temperature" topic with QoS 2
				client.Publish(Topic, Encoding.UTF8.GetBytes(redis.BLPop(30, "sensors_data")), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);




			}
		}
	}
}
