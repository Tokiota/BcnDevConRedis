using System.Collections.Generic;
using System.Linq;

using ServiceStack.Redis;

namespace POC.ReadModel
{
    public class ReadModelAccess
    {
        static readonly string RedisServer;
        static readonly int RedisPort;

        static ReadModelAccess()
        {
            RedisServer = System.Configuration.ConfigurationManager.AppSettings["redisServer"];
            var port = System.Configuration.ConfigurationManager.AppSettings["redisPort"];
            RedisPort = int.Parse(port);
        }

        public IList<ProductLineItem> GetAllProductLineItems()
        {
            IList<ProductLineItem> productLineItems;
            using (var redisClient = new RedisClient(RedisServer,RedisPort))
            using (var cacheProduct = redisClient.As<ProductLineItem>())
            {
                productLineItems = cacheProduct.GetAll();
            }
            return productLineItems;
        }

        public IList<OrderLineItem> GetAllOrderLineItems()
        {
            IList<OrderLineItem> rtv;
            using (var redisClient = new RedisClient(RedisServer, RedisPort))
            using (var cacheProduct = redisClient.As<OrderLineItem>())
            {
                rtv = cacheProduct.GetAll().Skip(200).Take(50).ToList();
            }
            return rtv;
        }
    }
}