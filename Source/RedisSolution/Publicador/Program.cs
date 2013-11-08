using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ServiceStack.Redis;

namespace Publicador
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                for (int i = 0; i < 10; i++)
                {
                    using (IRedisClient client = new RedisClient())
                    {
                        client.PublishMessage("canal", i.ToString(CultureInfo.InvariantCulture));
                    }
                }

                Thread.Sleep(5000);
            }
            
        }
    }
}
