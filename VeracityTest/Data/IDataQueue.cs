using System;
using System.Collections.Generic;
using System.Text;
using VeracityTest.Consumers;

namespace VeracityTest.Data
{
    interface IDataQueue
    {
        void RegisterListener(ConsumerType consumerType, IConsumer consumer);
        bool AddItem(DataItem item);
    }
}
