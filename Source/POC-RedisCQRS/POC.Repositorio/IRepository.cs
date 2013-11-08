using System.Collections.Generic;

using ServiceStack.Redis;
using ServiceStack.Text;

namespace POC.Repositorio
{

    public class Publisher
    {
        public void Publish<T>(string channelName, T @event)
        {
            using (var redisClient = new RedisClient("localhost"))
            {
                var json = JsonSerializer.SerializeToString(@event);
                redisClient.PublishMessage(channelName, json);
            }
        }
    }

}
