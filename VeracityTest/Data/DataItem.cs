using System;
using System.Collections.Generic;
using System.Text;
using VeracityTest.Consumers;

namespace VeracityTest.Data
{
    public class DataItem
    {
        private ConsumerType _consumerType;
        private string _payload;

        public ConsumerType ConsumerType { get => _consumerType; }
        public string Payload { get => _payload; }

        public DataItem(ConsumerType consumerType, string payload)
        {
            _consumerType = consumerType;
            _payload = payload;
        }
    }
}
