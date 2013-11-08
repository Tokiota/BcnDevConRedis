using System;
using System.Text;

using ServiceStack.Redis;

namespace BasicCommand
{
    public class RedisNativeClient
    {
        public const string STRING_KEY = "saludo";

        public const string LIST_KEY = "list-key";

        public const string SET_KEY = "set-key";

        public const string HASH_KEY = "hash-key";

        public const string SET_ORDER_KEY = "zset-key";

        public void UsingNativeClient()
        {
            WhenStringIsValue();
            WhenListIsValue();
            WhenSetIsValue();
            WhenHashIsValue();
            WhenSortedSetIsValue();
        }

        private static void WhenSortedSetIsValue()
        {
            using (IRedisNativeClient client = new RedisClient())
            {
                client.Del(SET_ORDER_KEY);

                client.ZAdd(SET_ORDER_KEY, 728, Encoding.UTF8.GetBytes("usuario01"));
                client.ZAdd(SET_ORDER_KEY, 300, Encoding.UTF8.GetBytes("usuario02"));
                client.ZAdd(SET_ORDER_KEY, 450, Encoding.UTF8.GetBytes("usuario03"));
                client.ZAdd(SET_ORDER_KEY, 280, Encoding.UTF8.GetBytes("usuario04"));
                client.ZAdd(SET_ORDER_KEY, 101, Encoding.UTF8.GetBytes("usuario05"));
                client.ZAdd(SET_ORDER_KEY, 300, Encoding.UTF8.GetBytes("usuario06")); 
                client.ZAdd(SET_ORDER_KEY, 325, Encoding.UTF8.GetBytes("usuario07"));


                client.ZRangeByScore(SET_ORDER_KEY,0, 1000,null,null).ToConsole();


                client.ZRevRangeByScore(SET_ORDER_KEY, 0, long.MaxValue,0, 3).ToConsole();
            }
        }

        private static void WhenHashIsValue()
        {
            using (IRedisNativeClient client = new RedisClient())
            {
                client.Del(HASH_KEY);

                var nombre = Encoding.UTF8.GetBytes("nombre");
                var apellido = Encoding.UTF8.GetBytes("apellido");


                client.HSet(HASH_KEY, nombre, Encoding.UTF8.GetBytes("Daniel"));
                client.HSet(HASH_KEY, apellido, Encoding.UTF8.GetBytes("Mazzini"));

                client.HGet(HASH_KEY, nombre).ToConsole();

                

                client.HGetAll(HASH_KEY).ToConsole();

                
                
            }

        }

        private static void WhenSetIsValue()
        {
            using (IRedisNativeClient client = new RedisClient())
            {
                client.Del(SET_KEY);

                client.SAdd(SET_KEY, Encoding.UTF8.GetBytes("item"));
                client.SAdd(SET_KEY, Encoding.UTF8.GetBytes("item2"));
                client.SAdd(SET_KEY, Encoding.UTF8.GetBytes("item3"));
                client.SAdd(SET_KEY, Encoding.UTF8.GetBytes("item4"));


                client.SMembers(SET_KEY).ToConsole();

                Console.WriteLine(client.SIsMember(SET_KEY, Encoding.UTF8.GetBytes("item")));
                Console.WriteLine(client.SIsMember(SET_KEY, Encoding.UTF8.GetBytes("noToy")));

                client.SRem(SET_KEY, Encoding.UTF8.GetBytes("item"));

                client.SMembers(SET_KEY).ToConsole();


            }
        }
        private static void WhenStringIsValue()
        {
            using (IRedisNativeClient client = new RedisClient())
            {
                client.Del(STRING_KEY);


                client.Set(STRING_KEY, Encoding.UTF8.GetBytes("Hola mundo"));

                var result = GetRedisValue(client, STRING_KEY);
                Console.WriteLine((string)"encontre {0}", (object)result);

                client.Del(STRING_KEY);
                result = GetRedisValue(client, STRING_KEY);

                Console.WriteLine((string)"encontre {0}", (object)result);
            }


        }

        private static void WhenStringIsValueAndRedisClient()
        {
            using (IRedisClient client = new RedisClient())
            {
                client.Remove(STRING_KEY);

                client.Add(STRING_KEY, "daniel");
                var nombre = client.Get<string>("daniel");
                Console.WriteLine(nombre);
            }
        }

        private static void WhenListIsValue()
        {
            using (IRedisNativeClient client = new RedisClient())
            {
                client.Del(LIST_KEY);
                client.RPush(LIST_KEY, Encoding.UTF8.GetBytes("item"));
                client.RPush(LIST_KEY, Encoding.UTF8.GetBytes("item2"));
                client.RPush(LIST_KEY, Encoding.UTF8.GetBytes("item3"));
                client.RPush(LIST_KEY, Encoding.UTF8.GetBytes("item"));
                client.RPush(LIST_KEY, Encoding.UTF8.GetBytes("item5"));



                client.LRange(LIST_KEY, 0, -1).ToConsole();

               

                var entryByIndex = client.LIndex(LIST_KEY, 1);

                Console.WriteLine("Get by index {0} ",Encoding.UTF8.GetString(entryByIndex));

                Console.WriteLine("Count {0}", client.LLen(LIST_KEY));
                

                var entryByPop = client.LPop(LIST_KEY);
                Console.WriteLine("First element {0}", Encoding.UTF8.GetString(entryByPop));

                Console.WriteLine("Count {0}", client.LLen(LIST_KEY));

                client.LSet(LIST_KEY, 3, Encoding.UTF8.GetBytes("nuevo valor en la posicion 3"));
                entryByIndex = client.LIndex(LIST_KEY, 3);

                Console.WriteLine("Get by index {0} ", Encoding.UTF8.GetString(entryByIndex));
                
            }
        }

        private static string GetRedisValue(IRedisNativeClient client, string key)
        {
            var redisValue = client.Get(key);
            return redisValue != null
                ? Encoding.UTF8.GetString(redisValue)
                : "no found";
        }
    }
}