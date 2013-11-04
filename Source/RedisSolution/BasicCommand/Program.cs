using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

using ServiceStack.CacheAccess;
using ServiceStack.Common.Extensions;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace BasicCommand
{
    public class User
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public string Username { get;set; }
        public string LastName { get;set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", Id, Name, LastName, Username);
        }
    }
    class Program
    {
        private static void Main(string[] args)
        {
            new RedisNativeClient().UsingNativeClient();





            //using (IRedisClient client = new RedisClient())
            //{
            //    client.Set("saludo", "hola mundo");
            //    var result = client.Get<string>("saludo");
            //    Console.WriteLine(result);
            //}

            var user = new User
                       {
                           Name = "Daniel",
                           Username = "ElPibe",
                           LastName = "Mazzini",

                       };

            var keyUser = SaveUser(user);

            User finded;
            using (ICacheClient client = new RedisClient())
            {

                finded = client.Get<User>(keyUser);
            }

            Console.WriteLine(finded);

            var res = ReferenceEquals(user, finded);

            Console.ReadLine();
        }

        static string SaveUser(User usuario)
        {

            string clave;
            using (IRedisClient client = new RedisClient())
            {
                usuario.Id = client.Increment("user:id",1);
                clave = string.Format("user:{0}", usuario.Id);
                client.Set(clave, usuario);

                
            }
            return clave;
        }
    }
}
