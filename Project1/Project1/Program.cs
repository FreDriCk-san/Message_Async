using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project1
{
    class Program
    {
        private static void CreateMessage(BlockingCollection<string> bCollection)
        {
            for (;;)
            {
                Message.Create(bCollection);
            }
        }


        private static void Processing(BlockingCollection<string> bCollection)
        {
            var send = Task.Run(async () =>
            {
                await Process.Send();
                Process.Receive(bCollection);
            });
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
