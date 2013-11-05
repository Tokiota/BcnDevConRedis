using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

using ServiceStack.Common.Extensions;
using ServiceStack.Redis;

namespace Intersection
{
    class Program
    {
        private static readonly DateTime UnixTime = new DateTime(1970, 1, 1);
        private const int CuantosMinutosConsideroEnLinea = -5;
        private const string ClaveTodosUsuariosEnLinea = "todosUsuariosEnLinea";

        static void Main(string[] args)
        {
            SimularElMovimientoDeUsuariosEnLinea();
            var usuario = CrearUsuarioQueNecesitaSaberSiEstanSusAmigosEnLinea("5","1","6","3");

            
            var onlineFriends = ObtenerAmigosEnLinea(usuario);
            onlineFriends.ForEach(Console.WriteLine);

            Console.ReadLine();
        }

        private static void SimularElMovimientoDeUsuariosEnLinea()
        {
            using (IRedisClient client = new RedisClient())
            {
                client.AddItemToSet(string.Format("userOnline:{0}", DiffWithUnixTime(DateTime.Now.AddMinutes(-4))), "1");
                client.AddItemToSet(string.Format("userOnline:{0}", DiffWithUnixTime(DateTime.Now.AddMinutes(-3))), "2");
                client.AddItemToSet(string.Format("userOnline:{0}", DiffWithUnixTime(DateTime.Now.AddMinutes(-2))), "3");
                client.AddItemToSet(string.Format("userOnline:{0}", DiffWithUnixTime(DateTime.Now.AddMinutes(-1))), "4");
                client.AddItemToSet(string.Format("userOnline:{0}", DiffWithUnixTime(DateTime.Now)), "1");
            }
        }

        private static IEnumerable<string> ObtenerAmigosEnLinea(User currentUser)
        {
            var maxMin = DiffWithUnixTime(DateTime.Now);
            var minMin = DiffWithUnixTime(DateTime.Now.AddMinutes(CuantosMinutosConsideroEnLinea));

            var claves = new List<string>();
            
            for (var mt = minMin; mt < maxMin; mt++)
            {
                claves.Add(string.Format("userOnline:{0}", mt));
            }

            var timer = new Stopwatch();
            timer.Start();

            using (IRedisClient client = new RedisClient())
            {

                UnoLosUsuariosDeUltimosMinutos(client, claves);
                
                var claveFriends = PonerTodosLosAmigosEnUnConjunto(client, currentUser);
                Console.WriteLine(timer.Elapsed);
                var result = client.GetIntersectFromSets(ClaveTodosUsuariosEnLinea, claveFriends);

                Console.WriteLine(timer.Elapsed);
                return result.ToArray();
            }
           
        }

        private static string PonerTodosLosAmigosEnUnConjunto(IRedisClient client, User currentUser)
        {
            var claveFriends = string.Format("friends:{0}", currentUser.UserId);
            client.AddRangeToSet(claveFriends, currentUser.Friends.Select(f => f.UserId).ToList());
            return claveFriends;
        }

        private static void UnoLosUsuariosDeUltimosMinutos(IRedisClient client, List<string> claves)
        {
            client.Remove(ClaveTodosUsuariosEnLinea);
            client.StoreUnionFromSets(ClaveTodosUsuariosEnLinea, claves.ToArray());
        }

        private static long DiffWithUnixTime(DateTime date)
        {
            return date.Subtract(UnixTime).TotalMinutes.To<long>();
        }

        private static User CrearUsuarioQueNecesitaSaberSiEstanSusAmigosEnLinea(string userId, string friendId1, string friendId2, string friendId3)
        {
            var friend1 = new Friend
                          {
                              UserId = friendId1
                          };
            var friend2 = new Friend
                          {
                              UserId = friendId2
                          };
            var friend3 = new Friend
                          {
                              UserId = friendId3
                          };

            var currentUser = new User
                              {
                                  UserId = userId
                              };
            currentUser.AddFriend(friend1);
            currentUser.AddFriend(friend2);
            currentUser.AddFriend(friend3);
            return currentUser;
        }
    }
}
