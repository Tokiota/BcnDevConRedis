using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using CacheNW.Model;

using ServiceStack.Redis;

namespace CacheNW
{
    public class Program
    {
        static void Main(string[] args)
        {

            var cat2Cache = new NwCategoriaToCache();

            cat2Cache.Load();

            var categorias = cat2Cache.GetFromRedis();

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1500);
                categorias = cat2Cache.GetFromRedis();
            }
            


            Console.ReadLine();

        }

	}
}
       

