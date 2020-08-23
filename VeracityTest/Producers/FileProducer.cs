using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using VeracityTest.Data;
using VeracityTest.Consumers;

namespace VeracityTest.Producers
{
    class FileProducer : IProducer
{
        private static IDataQueue _dataQueue;
        private static FileInfo _inputFileInfo;

        private Thread _producerThread;
        private static bool _runThread = true;

        public FileProducer(IDataQueue dataQueue, FileInfo inputFileInfo)
        {
            // Check the input file exists
            if (!inputFileInfo.Exists) { throw new Exception($"File {inputFileInfo.FullName} passed to producer doesn't exist!"); }

            _dataQueue = dataQueue;
            _inputFileInfo = inputFileInfo;

            _producerThread = new Thread(RunProducer);
        }

        private static void RunProducer(object obj)
        {
            int delay;
            StreamReader input = File.OpenText(_inputFileInfo.FullName);

            try
            {
                delay = (int)obj;
            }
            catch (InvalidCastException)
            {
                delay = 5000;
            }

            while (_runThread)
            {
                string currentLine = input.ReadLine();
                if (currentLine != null)
                {
                    List<string> lineSplit = new List<string>(currentLine.Split(','));

                    if (lineSplit.Count != 2) { continue; }

                    string lineType = lineSplit[0].ToLower();
                    ConsumerType currentType = ConsumerType.CONSOLE;
                    if (lineType.Equals("console"))
                    {
                        currentType = ConsumerType.CONSOLE;
                    } else if (lineType.Equals("file"))
                    {
                        currentType = ConsumerType.FILE;
                    }

                    DataItem currentItem = new DataItem(currentType, lineSplit[1]);
                    _dataQueue.AddItem(currentItem);
                }

                Thread.Sleep(delay);
            }
            Thread.Sleep(0);
        }

        public void StartProducer(int productionDelay)
        {
            if (productionDelay > 0 && !_producerThread.IsAlive)
            {
                _runThread = true;
                _producerThread.Start(productionDelay);
            }
        }

        public void StopProducer()
        {
            if (_producerThread != null && _producerThread.IsAlive)
            {
                _runThread = false;
                _producerThread.Join();
            }
        }
    }
}
