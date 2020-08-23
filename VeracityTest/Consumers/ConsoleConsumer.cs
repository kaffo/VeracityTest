using System;
using System.Collections.Generic;
using System.Text;
using VeracityTest.Data;

namespace VeracityTest.Consumers
{
    class ConsoleConsumer : IConsumer
    {
        private IDataQueue _dataQueue;

        public ConsoleConsumer(IDataQueue dataQueue)
        {
            _dataQueue = dataQueue;
        }

        public void OnItemAdded()
        {
            DataItem item = _dataQueue.GetNextItem(ConsumerType.CONSOLE);
            if (item != null) { Console.Out.WriteLine($"{item.Payload}"); }
        }
    }
}
