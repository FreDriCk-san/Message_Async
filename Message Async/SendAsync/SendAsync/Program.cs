using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace SendAsync
{
    class Program
    {


        private static void CreateMessage(BlockingCollection<string> bCollection)
        {
            for (; ; )
            {
                Message.Create(bCollection);
            }
        }


        private static void Processing(BlockingCollection<string> bCollection)
        {
            for (; ; )
            {
                var send = Task.Run(async () =>
                {
                    await Message.Send();
                });

                Message.Receive(bCollection);
            }
        }


        static void Main(string[] args)
        {
            var bCollection = new BlockingCollection<string>();

            var threadCreate = new Thread(() => CreateMessage(bCollection));
            var threadProcess = new Thread(() => Processing(bCollection));

            threadCreate.Start();
            threadProcess.Start();


            Console.ReadKey();
        }
    }
}
