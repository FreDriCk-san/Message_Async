using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SendAsync
{
    public class Process
    {

        public static void Receive(BlockingCollection<string> bCollection)
        {
            // Take elements from BlockingCollection
            foreach (var item in bCollection.GetConsumingEnumerable())
            {
                Console.WriteLine("Message {0} was sended", item);
            }
        }


        // Simulate sending statement
        public static async Task Send()
        {
            try
            {
                var random = new Random().Next(4000);
                await Task.Delay(random);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Sending Error: " + exception);
            }
        }

    }
}
