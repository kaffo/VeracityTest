using System;
using System.IO;
using System.Threading;
using VeracityTest.Producers;
using VeracityTest.Consumers;
using VeracityTest.Data;

namespace VeracityTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataQueue dataQueue = new DataQueueDebug();
            IProducer producer = new FileProducer(dataQueue, new FileInfo(@"D:\workspace\VeracityTest\CodingTest2020InputStimulus.csv"));
            producer.StartProducer(1000);
            Thread.Sleep(5000);
            producer.StopProducer();
            Console.WriteLine("Complete");
        }
    }
}
