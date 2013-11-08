using System;
using System.Threading;

using ServiceStack.CacheAccess;
using ServiceStack.Redis;

namespace BasicCommand
{
    class Program
    {
        private static void Main(string[] args)
        {
            //new RedisNativeClient().UsingNativeClient();

            //var trans = new Transaccion();
            //trans.LanzarSinTransaccion();

            //Thread.Sleep(5000);

            //trans.LanzarConTransaccion();

            //Console.ReadLine();

            var pubsub = new PubSub();
            pubsub.InitPubSub();


            Console.ReadLine();


        }

        //private static void UsingLikeCache()
        //{
        //    var user = new User
        //               {
        //                   Name = "Daniel",
        //                   Username = "ElPibe",
        //                   LastName = "Mazzini",
        //               };

        //    var keyUser = SaveUser(user);

        //    User finded;
        //    using (ICacheClient client = new RedisClient())
        //    {
        //        finded = client.Get<User>(keyUser);
        //    }

        //    Console.WriteLine(finded);

        //    Console.ReadLine();
        //}

        //static string SaveUser(User usuario)
        //{

        //    string clave;
        //    using (ICacheClient client = new RedisClient())
        //    {
        //        usuario.Id = client.Increment("user:id",1);
        //        clave = string.Format("user:{0}", usuario.Id);
        //        client.Set(clave, usuario);

                
        //    }
        //    return clave;
        //}
    }
}
