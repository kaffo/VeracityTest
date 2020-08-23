using System;
using System.Collections.Generic;
using System.Text;
using VeracityTest.Consumers;

namespace VeracityTest.Data
{
    public delegate void ItemAddedEventHandler();

    interface IDataQueue
    {
        void RegisterListener(ConsumerType consumerType, IConsumer consumer);
        bool AddItem(DataItem item);
        event ItemAddedEventHandler ItemAdded;
        DataItem GetNextItem();
        DataItem GetNextItem(ConsumerType consumerType);
    }
}
