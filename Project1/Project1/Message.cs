using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
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
