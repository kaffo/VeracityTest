using System;
using System.Collections.Generic;
using System.Text;

namespace VeracityTest.Producers
{
    public interface IProducer
    {
        void StartProducer(int productionDelay);
        void StopProducer();
    }
}
