using System;
using System.Threading;
using System.Threading.Tasks;

using ServiceStack.Redis;

namespace BasicCommand
{
    public class Transaccion
    {
        public Transaccion()
        {
            using (IRedisClient client = new RedisClient())
            {
                client.IncrementValue("contador");
            }
        }
        public void NoTrans()
        {

            using (IRedisClient client = new RedisClient())
            {
                var result = client.IncrementValue("contador");
                Console.WriteLine(result);
                Thread.Sleep(100);
                client.DecrementValue("contador");
            }
        }

        public void LanzarSinTransaccion()
        {
            for (int i = 0; i < 4; i++)
            {
                Task.Factory.StartNew(NoTrans);
            }
        }


        public void LanzarConTransaccion()
        {
            for (int i = 0; i < 4; i++)
            {
                Task.Factory.StartNew(Trans);
            }
        }

        private void Trans()
        {
            using (IRedisClient client = new RedisClient())
            {
                using (var pipeline = client.CreatePipeline())
                {
                    pipeline.QueueCommand(c => c.IncrementValue("contador"));
                    pipeline.QueueCommand(c => Console.WriteLine(client.Get<string>("contador")));
                    Thread.Sleep(100);
                    pipeline.QueueCommand(c => c.DecrementValue("contador"));
                    pipeline.Flush();
                    Console.WriteLine(client.Get<string>("contador"));
                }
            }
        }
    }
}
