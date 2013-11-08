using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServiceStack.Common.Extensions;
using ServiceStack.Redis;

namespace Intersection
{
    public class UsuariosOnLine
    {
        private static readonly DateTime UnixTime = new DateTime(1970, 1, 1);
        private const int CuantosMinutosConsideroEnLinea = -5;
        private const string ClaveTodosUsuariosEnLinea = "todosUsuariosEnLinea";

        public void Launch()
        {
            SimularElMovimientoDeUsuariosEnLinea();
            var usuario = CrearUsuarioQueNecesitaSaberSiEstanSusAmigosEnLinea("5", "1", "6", "3");

            Enumerable.Range(0, 1500).Select(num => num * 19).ForEach(id => usuario.AddFriend(id.ToString()));

            var onlineFriends = ObtenerAmigosEnLinea(usuario);
            onlineFriends.ForEach(Console.WriteLine);
        }

        private void SimularElMovimientoDeUsuariosEnLinea()
        {
            using (IRedisClient client = new RedisClient())
            {
                Enumerable.Range(1, 10000).ForEach(num => client.AddItemToSet(string.Format("userOnline:{0}", DiffWithUnixTime(DateTime.Now.AddMinutes(-4))), num.ToString()));
                Enumerable.Range(10001, 20000).ForEach(num => client.AddItemToSet(string.Format("userOnline:{0}", DiffWithUnixTime(DateTime.Now.AddMinutes(-3))), num.ToString()));
                Enumerable.Range(20001, 30000).ForEach(num => client.AddItemToSet(string.Format("userOnline:{0}", DiffWithUnixTime(DateTime.Now.AddMinutes(-2))), num.ToString()));
                Enumerable.Range(30001, 40000).ForEach(num => client.AddItemToSet(string.Format("userOnline:{0}", DiffWithUnixTime(DateTime.Now.AddMinutes(-1))), num.ToString()));

                client.AddItemToSet(string.Format("userOnline:{0}", DiffWithUnixTime(DateTime.Now.AddMinutes(-4))), "1");
                client.AddItemToSet(string.Format("userOnline:{0}", DiffWithUnixTime(DateTime.Now.AddMinutes(-3))), "2");
                client.AddItemToSet(string.Format("userOnline:{0}", DiffWithUnixTime(DateTime.Now.AddMinutes(-2))), "3");
                client.AddItemToSet(string.Format("userOnline:{0}", DiffWithUnixTime(DateTime.Now.AddMinutes(-1))), "4");
                client.AddItemToSet(string.Format("userOnline:{0}", DiffWithUnixTime(DateTime.Now)), "1");
            }
        }

        private IEnumerable<string> ObtenerAmigosEnLinea(User currentUser)
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
                Debug.WriteLine("tiempo para hacer la union {0}", timer.Elapsed);
                timer.Restart();
                var result = client.GetIntersectFromSets(ClaveTodosUsuariosEnLinea, claveFriends);
                Debug.WriteLine("tiempo para la interseccion {0}", timer.Elapsed);
                return result.ToArray();
            }
        }

        private string PonerTodosLosAmigosEnUnConjunto(IRedisClient client, User currentUser)
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
            var currentUser = new User
            {
                UserId = userId
            };
            currentUser.AddFriend(friendId1);
            currentUser.AddFriend(friendId2);
            currentUser.AddFriend(friendId3);
            return currentUser;
        }
    }
}
