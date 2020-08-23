using System;
using System.Collections.Generic;
using System.Text;
using VeracityTest.Consumers;

namespace VeracityTest.Data
{
    public class DataQueueDebug : IDataQueue
    {
        public event ItemAddedEventHandler ItemAdded;

        public bool AddItem(DataItem item)
        {
            Console.Out.WriteLine($"Added {item.ConsumerType}:{item.Payload}");
            return true;
        }

        public DataItem GetNextItem()
        {
            return new DataItem(ConsumerType.CONSOLE, "DUMMY");
        }

        public DataItem GetNextItem(ConsumerType consumerType)
        {
            return new DataItem(consumerType, "DUMMY");
        }

        public void RegisterListener(ConsumerType consumerType, IConsumer consumer)
        {
            Console.Out.WriteLine($"Attempted to register {consumer.ToString()} as {consumerType}");
        }
    }
}
