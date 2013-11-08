using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace POC.UI
{
    public class Publisher
    {
        static readonly string RedisServer;
        static readonly int RedisPort;

        static Publisher()
        {
            RedisServer = System.Configuration.ConfigurationManager.AppSettings["redisServer"];
            var port = System.Configuration.ConfigurationManager.AppSettings["redisPort"];
            RedisPort = int.Parse(port);
        }
        public static void Publish<T>(string channelName, T @event)
        {
            var json = @event.ToJson();
            using (var redisClient = new RedisClient(RedisServer,RedisPort))
            {
                redisClient.PublishMessage(channelName, json);
            }
        }
    }
}
