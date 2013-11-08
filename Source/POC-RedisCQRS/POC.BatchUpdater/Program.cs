using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

using POC.Data.Models;
using POC.ReadModel;

using ServiceStack.Common.Support;
using ServiceStack.Common.Utils;
using ServiceStack.Logging;
using ServiceStack.Redis;

namespace POC.BatchUpdater
{
    public static class TestConfig
    {
        static TestConfig()
        {
            LogManager.LogFactory = new InMemoryLogFactory();
        }

        public const bool IgnoreLongTests = true;

        public const string SingleHost = "localhost";
        public static readonly string[] MasterHosts = new[] { "localhost" };
        public static readonly string[] SlaveHosts = new[] { "localhost" };

        public const int RedisPort = 6379;
        public const int AlchemyPort = 6380;

        //public const string SingleHost = "chi-dev-mem1.ddnglobal.local";
        //public static readonly string [] MasterHosts = new[] { "chi-dev-mem1.ddnglobal.local" };
        //public static readonly string [] SlaveHosts = new[] { "chi-dev-mem1.ddnglobal.local", "chi-dev-mem2.ddnglobal.local" };
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            while(true)
            {
                Console.WriteLine("Empezando ...");
                var clock = new Stopwatch();
                clock.Start();
                using (var context = new NorthwindContext())
                {
                    var listProductLineItem = context.Products.Select(p => new ProductLineItem
                                                                                                   {
                                                                                                       Id = p.ProductID,
                                                                                                       Description = p.ProductName,
                                                                                                       CategoryName = p.Category.CategoryName,
                                                                                                       Stock = p.UnitsInStock,
                                                                                                       UnitPrice = p.UnitPrice
                                                                                                   });

                    using (IRedisClient client = new RedisClient())
                    {
                        using (var pipeline = client.CreatePipeline())
                        {
                            pipeline.QueueCommand(c => c.DeleteAll<ProductLineItem>());
                            pipeline.QueueCommand(c => c.As<ProductLineItem>().StoreAll(listProductLineItem));
                            pipeline.Flush();
                        }
                    }
                }

                Console.WriteLine("Termine en {0} milisegundos. Entrando en modo espera", clock.Elapsed);
                clock.Stop();
                Thread.Sleep(20.ToSeconds());
            }
        }

        
    }
    public static class TimeSpanExtended
    {
        public static TimeSpan ToMinutes(this Int32 minutes)
        {
            return new TimeSpan(0, minutes, 0);
        }

        public static TimeSpan ToSeconds(this Int32 seconds)
        {
            return new TimeSpan(0, 0, seconds);
        }

    }
}
