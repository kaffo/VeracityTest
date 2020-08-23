using System;
using System.Collections.Generic;
using System.Text;
using VeracityTest.Consumers;

namespace VeracityTest.Data
{
    class DataQueueDebug : IDataQueue
    {
        public bool AddItem(DataItem item)
        {
            Console.Out.WriteLine($"Added {item.ConsumerType}:{item.Payload}");
            return true;
        }

        public void RegisterListener(ConsumerType consumerType, IConsumer consumer)
        {
            Console.Out.WriteLine($"Attempted to register {consumer.ToString()} as {consumerType}");
        }
    }
}
