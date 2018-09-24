using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace SendAsync
{
    class Program
    {

        private static void UI(BlockingCollection<string> bCollection)
        {
            for (; ; )
            {
                //Console.Clear();
                //Console.WriteLine("Created : ");
                //Console.WriteLine("Processing : " + bCollection.Count);
                //Console.WriteLine("Received : ");
            }
        }


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


            var threadUI = new Thread(() => UI(bCollection));
            var threadCreate = new Thread(() => CreateMessage(bCollection));
            var threadProcess = new Thread(() => Processing(bCollection));

            threadCreate.Start();
            threadProcess.Start();
            threadUI.Start();


            Console.ReadKey();
        }
    }
}
