using System;
using System.Collections.Concurrent;

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
            catch (Exception exception)
            {
                Console.WriteLine("Create Error: " + exception.Message);
            }

            // If sended successful, add to queue
            if (success)
            {
                Console.WriteLine("Sending new message: " + message);
                Console.WriteLine("\n//Collection current count: " + bCollection.Count + "\n");
            }

        }

    }
}
