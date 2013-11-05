using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ServiceStack.Common.Extensions;
using ServiceStack.Redis;

namespace FillToAutocomplete
{
    class Program
    {
        private const string Key = "autocomplete";
        private const int PageSize = 200;

        private static void Main(string[] args)
        {

            CargandoLasPoblaciones();

            BuscarPalabara("barcelona");
            BuscarPalabara("zaragoza");
            BuscarPalabara("madrid");
            BuscarPalabara("avila");

            Console.ReadLine();
        }



        private static void BuscarPalabara(string toFind)
        {
            var watch = new Stopwatch();
            for (var endIndex = 0; endIndex < toFind.Length; endIndex++)
            {
                var find = toFind.Substring(0, endIndex + 1);
                List<string> entries = null;
                watch.Start();
                using (IRedisClient client = new RedisClient())
                {
                    var begin = client.GetItemIndexInSortedSet(Key, find).To<int>();
                    if (begin == -1)
                        continue;
                    entries = client.GetRangeFromSortedSet(Key, begin, begin + PageSize - 1);
                }

                Console.WriteLine();
                if(entries != null)
                {
                    var palabras = entries
                        .Where(s => s.StartsWith(find) && s.Last() == '*')
                        .Select(s => s.Substring(0, s.Length - 1))
                        .Take(20)
                        .ToList();

                    Debug.WriteLine("buscando {0} tardo {1} y encontro {2}", find, watch.Elapsed,palabras.Count());
                    palabras.ForEach(Console.WriteLine);
                }
                watch.Reset();
            }
        }


        private static void CargandoLasPoblaciones()
        {
            using (var context = new Model.PoblacionesEntities())
            {
                var poblaciones = context.Poblacions.OrderBy(p => p.Name).ToList();

                using (IRedisClient client = new RedisClient())
                {
                     
                    client.Remove("KEY");
                    foreach (var poblacion in poblaciones)
                    {
                        var poblacionWithProvince = string.Format("{0} ({1})", poblacion.Name.ToLower(), poblacion.Provincia.Name);

                        var chars = poblacionWithProvince.ToCharArray();
                        for (var endIndex = 1; endIndex < chars.Length; endIndex++)
                        {
                            var prefix = poblacionWithProvince.Substring(0, endIndex);

                            client.AddItemToSortedSet(Key, prefix,0);
                        }
                        client.AddItemToSortedSet(Key, poblacionWithProvince + "*",0);
                    }
                }
            }
        }
    }
}
