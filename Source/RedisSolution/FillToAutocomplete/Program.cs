using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using ServiceStack.Common.Extensions;
using ServiceStack.Logging.Support.Logging;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using ServiceStack.Text;

namespace FillToAutocomplete
{
    public class PoblacionAutocomplete
    {
        public string Nombre { get; set; }
        public int IdPoblacion { get; set; }

        public string NombreCompleto { get; set; }

    }
    class Program
    {
        private const string KEY = "autocomplete";

        private static void Main(string[] args)
        {

            ProvisioningEstrategia01WithTyped();
            //ProvisioningEstrategia01();

            //FindByEstrategia01();
            //ProvisioningEstrategia02();
            //FindByEstrategia02();

            Console.ReadLine();
        }

        private static void FindByEstrategia02()
        {
            const string ToFind = "Zaragoza";
            using (IRedisNativeClient client = new RedisClient())
            {
                for (var endIndex = 0; endIndex < ToFind.Length; endIndex++)
                {
                    var find = ToFind.Substring(0, endIndex + 1);
                    var watch = Stopwatch.StartNew();

                    var result = client.SMembers(string.Format("{0}:{1}", KEY, find.ToLower()));

                    var poblaciones = result.Select(e => Encoding.UTF8.GetString(e)).OrderBy(s => s).ToList();

                    watch.Stop();
                    Debug.WriteLine("buscando {0} tardo {1}", find, watch.ElapsedMilliseconds);

                    poblaciones
                        .ForEach(Console.WriteLine);
                }
            }
        }

        private static void FindByEstrategia01()
        {
            const string ToFind = "vila";
            using (IRedisNativeClient client = new RedisClient())
            {
                
                for (var endIndex = 0; endIndex < ToFind.Length; endIndex++)
                {
                    var find = ToFind.Substring(0, endIndex + 1);
                    var watch = Stopwatch.StartNew();
                    var start = client.ZRank(KEY, Encoding.UTF8.GetBytes(find));
                    var begin = (int)start;

                    var entries = client.ZRange(KEY, begin, begin + 99);

                    Console.WriteLine();
                    Console.WriteLine();
                    if(entries != null)
                    {
                        var palabras = entries.Select(e => Encoding.UTF8.GetString(e))
                            .Where(s => s.StartsWith(find) && s.Last() == '*')
                            .Select(s => s.Substring(0, s.Length - 1))
                            .Take(20);

                        watch.Stop();
                        Debug.WriteLine("buscando {0} tardo {1}", find, watch.ElapsedMilliseconds);
                        palabras.ForEach(Console.WriteLine);
                    }
                }
            }
        }

        private static void ProvisioningEstrategia01()
        {
            using (var context = new Model.PoblacionesEntities())
            {
                var poblaciones = context.Poblacions.OrderBy(p => p.Name).ToList();

                using (IRedisNativeClient client = new RedisClient())
                {
                    client.Del(KEY);

                    foreach (var poblacion in poblaciones)
                    {
                        var pueblo = poblacion.Name;
                        var poblacionWithProvince = string.Format("{0} ({1})", pueblo, poblacion.Provincia.Name);

                        var chars = pueblo.ToCharArray();
                        for (int endIndex = 0; endIndex < chars.Length; endIndex++)
                        {
                            var prefix = poblacion.Name.Substring(0, endIndex + 1);

                            client.ZAdd(KEY, 0, Encoding.UTF8.GetBytes(prefix));
                        }
                        client.ZAdd(KEY, 0, Encoding.UTF8.GetBytes(pueblo + "*"));

                    }
                }
            }
        }

        private static void ProvisioningEstrategia01WithTyped()
        {
            using (var context = new Model.PoblacionesEntities())
            {
                var poblaciones = context.Poblacions.OrderBy(p => p.Name).ToList();
                using (IRedisNativeClient nativeClient = new RedisNativeClient())
                {
                    nativeClient.Del(KEY);
                }

                using (IRedisClient client = new RedisClient())
                {
                    //client.Del(KEY);

                    foreach (var poblacion in poblaciones)
                    {
                        var poblacionWithProvince = string.Format("{0} ({1})", poblacion.Name.ToLower(), poblacion.Provincia.Name);

                        var chars = poblacionWithProvince.ToCharArray();
                        for (int endIndex = 0; endIndex < chars.Length; endIndex++)
                        {
                            var prefix = poblacionWithProvince.Substring(0, endIndex + 1);

                            //var newPob = new PoblacionAutocomplete
                            //             {
                            //                 IdPoblacion = poblacion.Id,
                            //                 Nombre = prefix,
                            //                 NombreCompleto = poblacionWithProvince
                            //             };

                            client.AddItemToSortedSet(KEY, prefix,0);
                        }
                        client.AddItemToSortedSet(KEY, poblacionWithProvince + "*",0);
                    }
                }
            }
        }
        private static void ProvisioningEstrategia02()
        {

            using (var context = new Model.PoblacionesEntities())
            {
                var query = context.Poblacions.OrderBy(s => s.Name);


                using (IRedisNativeClient client = new RedisClient())
                {
                    client.Del(KEY);

                    foreach (var poblacion in query)
                    {
                        var pueblo = poblacion.Name.ToLower();
                        var chars = pueblo.ToCharArray();
                        var poblacionWithProvince = string.Format("{0} ({1})", poblacion.Name, poblacion.Provincia.Name);
 

                        for (var endIndex = 0; endIndex < chars.Length; endIndex++)
                        {
                            var prefix = pueblo.Substring(0, endIndex + 1);
                            client.SAdd(string.Format("{0}:{1}", KEY, prefix.ToLower()), Encoding.UTF8.GetBytes(poblacionWithProvince));
                        }
                        //client.ZAdd(KEY, 0, Encoding.UTF8.GetBytes(poblacion.Name + "*"));
                    }
                }
            }
        }
    }
}
