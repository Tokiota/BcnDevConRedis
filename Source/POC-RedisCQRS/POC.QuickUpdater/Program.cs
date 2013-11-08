using System;

using POC.Data.Models;
using POC.Messages;
using POC.ReadModel;

using ServiceStack.Redis;
using ServiceStack.Text;

namespace POC.QuickUpdater
{
    class Program
    {
        static readonly Func<ProductCreated, ProductLineItem> MapProductLineItem = product =>
            {
                var result = new ProductLineItem
                    {
                        Id = product.Id,
                        Description = product.Name,
                        Stock = product.Stock,
                        UnitPrice = product.UnitPrice,
                        CategoryName = product.CategoryName
                    };
                return result;
            };

        static void Main(string[] args)
        {
            using (var redisConsumer = new RedisClient())
            using (var subscription = redisConsumer.CreateSubscription())
            {
                subscription.OnSubscribe = channel => Console.WriteLine("Subscribed to '{0}'", channel);

                subscription.OnMessage = (channel, msg) =>
                    {
                        if (channel == Channels.ChannelNewProduct)
                        {
                            var productoCreated =JsonSerializer.DeserializeFromString<ProductCreated>(msg);

                            proccessProductCreated(productoCreated);
                        }
                    };

                subscription.SubscribeToChannels(Channels.ChannelNewProduct);
                Console.WriteLine("Servicio escuchando");
                Console.ReadLine();
            }
        }

        static void proccessProductCreated(ProductCreated newProduct)
        {
            var pli = MapProductLineItem(newProduct);
            pli.CategoryName = newProduct.CategoryName;
            using (var redisClient = new RedisClient())
            using (var cacheProduct = redisClient.As<ProductLineItem>())
            {
                cacheProduct.Store(pli);
            }
        }
        
    }
}
