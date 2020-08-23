using System;
using System.Collections.Generic;
using System.Text;
using VeracityTest.Consumers;

namespace VeracityTest.Data
{
    class DataQueue : IDataQueue
    {
        private Dictionary<ConsumerType, IConsumer> _listenerList;
        private List<DataItem> _dataQueue;

        public event ItemAddedEventHandler ItemAdded;

        public DataQueue()
        {
            _listenerList = new Dictionary<ConsumerType, IConsumer>();
            _dataQueue = new List<DataItem>();
        }

        public bool AddItem(DataItem item)
        {
            _dataQueue.Add(item);
            ItemAdded?.Invoke();
            return true;
        }

        public DataItem GetNextItem()
        {
            if (_dataQueue.Count > 0)
            {
                DataItem item = _dataQueue[0];
                _dataQueue.RemoveAt(0);
                return item;
            } else
            {
                return null;
            }
        }

        public DataItem GetNextItem(ConsumerType consumerType)
        {
            foreach (DataItem currentItem in _dataQueue)
            {
                if (currentItem.ConsumerType == consumerType)
                {
                    _dataQueue.Remove(currentItem);
                    return currentItem;
                }
            }

            return null;
        }

        public void RegisterListener(ConsumerType consumerType, IConsumer consumer)
        {
            if (!_listenerList.ContainsKey(consumerType))
            {
                _listenerList.Add(consumerType, consumer);
                ItemAdded += consumer.OnItemAdded;
            }
        }
    }
}
