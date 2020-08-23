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
            if (args.Length <= 0)
            {
                Console.WriteLine("usage: ./VeracityTest <input file path> <time to run (ms)> <delay between producer writes (ms)>");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("esc to quit early");

            FileInfo inputFile = new FileInfo(args[0]);
            int runLength = 10000;
            int delay = 500;

            if (args.Length > 1 && int.TryParse(args[1], out int val))
            {
                runLength = val;
            }

            if (args.Length > 2 && int.TryParse(args[2], out int val2))
            {
                delay = val2;
            }

            IDataQueue dataQueue = new DataQueue();
            IProducer producer = new FileProducer(dataQueue, inputFile);
            IConsumer consoleConsumer = new ConsoleConsumer(dataQueue);
            IConsumer fileConsumer = new FileConsumer(dataQueue, new FileInfo(@".\output.txt"));

            dataQueue.RegisterListener(ConsumerType.CONSOLE, consoleConsumer);
            dataQueue.RegisterListener(ConsumerType.FILE, fileConsumer);

            producer.StartProducer(delay);

            int curTime = 0;
            while (curTime < runLength)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo curKey = Console.ReadKey(true);
                    if (curKey.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
                curTime += 100;
                Thread.Sleep(100);
            }
            producer.StopProducer();
        }
    }
}
