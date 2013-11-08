using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ServiceStack.Redis;

namespace BasicCommand
{
    internal class PubSub
    {
        private void Publicar(int cantidad)
        {
            Thread.Sleep(5000);
            for (int i = 0; i < cantidad; i++)
            {
                using (IRedisClient client = new RedisClient())
                {
                    client.PublishMessage("canal", i.ToString(CultureInfo.InvariantCulture));
                }
            }

        }
        public void InitPubSub()
        {
            Task.Factory.StartNew(()=> Publicar(5));

            using (IRedisClient client = new RedisClient())
            {
                using (var subscription = client.CreateSubscription())
                {
                    subscription.OnSubscribe = channel => Console.WriteLine("Subscribed to '{0}'", channel);

                    subscription.OnMessage = (channel, msg) => Console.WriteLine("recibiendo mensaje {0} de canal {1}", msg, channel);

                    subscription.OnUnSubscribe = s => Console.WriteLine(" me dieron de baja al canal {0}", s);

                    subscription.SubscribeToChannels("canal");
                    Console.WriteLine("Servicio escuchando");
                    Console.ReadLine();

                }
            }
        }
    }
}
