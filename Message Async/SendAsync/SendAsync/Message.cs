using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace SendAsync
{
    public class Message
    {

        public static void Create(BlockingCollection<string> bCollection)
        {
            var success = false;

            // Simple message with GUID
            var message = Guid.NewGuid().ToString();

            try
            {
                success = bCollection.TryAdd(message, 2);
            }
            // Last version with canceltoken
            catch (OperationCanceledException)
            {
                Console.WriteLine("Stop creating messages");
                bCollection.CompleteAdding();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Create Error: " + exception.Message);
            }

            // If sended successful, add to queue
            if (success)
            {
                Console.WriteLine("Sending new message: " + message);
                Console.WriteLine("//Collection current count: " + bCollection.Count);
            }

        }


        public static void Receive(BlockingCollection<string> bCollection)
        {
            try
            {
                if (bCollection.TryTake(out string result, 2))
                {
                    Console.WriteLine("Message {0} was sended", result);
                }
            }
            // Last version with canceltoken
            catch (OperationCanceledException)
            {
                Console.WriteLine("Stop sending messages");
            }
            catch (Exception exception)
            {
                Console.WriteLine("Receive Error: " + exception.Message);
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
