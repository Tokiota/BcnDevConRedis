using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using AutoComplete.Models;

using ServiceStack.Common;
using ServiceStack.Common.Extensions;
using ServiceStack.Redis;

using Simple.Data;

namespace AutoComplete.Controllers
{

    
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };

        }

        // GET api/values/test
        public IDictionary<int, string> GetDataBase(string term)
        {
            var watch = Stopwatch.StartNew();
            Dictionary<int, string> result;

            using (var context = new PoblacionesEntities())
            {
                result = context.Poblacions
                    .Where(p => p.Name.ToLower().StartsWith(term))
                    .ToDictionary(p => p.Id, q => string.Format("{0} ({1})", q.Name, q.Provincia.Name));
            }

            watch.Stop();

            Debug.WriteLine("busqueda {0} tarda {1}", term, watch.ElapsedMilliseconds);

            return result;
        }

        //public IEnumerable<string> GetByRedis(string term)
        //{
        //    string toFind = term.ToLower();
        //    const string KEY = "autocomplete";
        //    List<string> palabras;
        //    using (IRedisNativeClient client = new RedisClient())
        //    {
        //        var watch = Stopwatch.StartNew();
        //        var start = client.ZRank(KEY, Encoding.UTF8.GetBytes(toFind));
        //        var begin = (int)start;

        //        var entries = client.ZRange(KEY, begin, begin + 150);

        //        if (entries != null)
        //        {
        //            palabras = entries.Select(e => Encoding.UTF8.GetString(e))
        //                .Where(s => s.StartsWith(toFind) && s.Last() == '*')
        //                .Select(s => char.ToUpper(s[0]) + s.Substring(1, s.Length - 2))
        //                .Take(20)
        //                .ToList();


        //        }
        //        else
        //        {
        //            palabras = new List<string>();
        //        }
        //        watch.Stop();
        //        Debug.WriteLine("buscando {0} tardo {1}", term, watch.ElapsedMilliseconds);
        //        palabras.ForEach(Console.WriteLine);
        //    }

        //    return palabras;
        //}

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post(string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}