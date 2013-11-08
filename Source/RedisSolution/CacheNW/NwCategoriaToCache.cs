using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CacheNW.Model;

using ServiceStack.Redis;

namespace CacheNW
{
    public class NwCategoriaToCache
    {
        public void Load()
        {
            var before = DateTime.Now;
            var categorias = new List<ComboItem>();

            using (var nw = new NorthwindEntities1())
            {

                var query = nw.Order_Details.Select(cat => new
                                                           {
                                                               Id = cat.OrderID,
                                                               UP = cat.UnitPrice
                                                           });
                categorias.AddRange(query.AsEnumerable().Select(item => new ComboItem
                                                         {
                                                             Id = item.Id,
                                                             Descripcion = item.UP.ToString(CultureInfo.InvariantCulture)
                                                         }));

            }

            Console.WriteLine("Buscar datos de categorias en BBDD tarda {0}. Registros {1} ", (DateTime.Now - before), categorias.Count);

            before = DateTime.Now;
            using (IRedisClient client = new RedisClient())
            {
                client.Add("cmbCategorias", categorias);

                client.ExpireEntryIn("cmbCategorias", new TimeSpan(0,0, 10));

                Console.WriteLine("Insertar datos en Redis tarda {0} ", (DateTime.Now - before));
            }
        }

        public List<ComboItem> GetFromRedis()
        {
            List<ComboItem> categorias;
            using (IRedisClient client = new RedisClient())
            {
                var before = DateTime.Now;

                categorias = client.Get<List<ComboItem>>("cmbCategorias");
                Console.WriteLine("Buscar datos de categorias en Redis tarda {0} ", (DateTime.Now - before));
                if(categorias == null)
                {
                    Load();
                    categorias = client.Get<List<ComboItem>>("cmbCategorias");
                }
            }
            return categorias;
        }
    }
}
