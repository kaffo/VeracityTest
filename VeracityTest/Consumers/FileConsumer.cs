using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using VeracityTest.Data;

namespace VeracityTest.Consumers
{
    public class FileConsumer : IConsumer
    {
        private IDataQueue _dataQueue;
        private FileInfo _outputFile;

        public FileConsumer(IDataQueue dataQueue, FileInfo outputFile)
        {
            if (!outputFile.Exists)
            {
                StreamWriter sw = File.CreateText(outputFile.FullName);
                sw.Close();
            }

            _dataQueue = dataQueue;
            _outputFile = outputFile;
        }

        public void OnItemAdded()
        {
            DataItem item = _dataQueue.GetNextItem(ConsumerType.FILE);
            if (item != null) {
                using (StreamWriter file = new StreamWriter(_outputFile.FullName, true))
                {
                    file.WriteLine(item.Payload);
                }
            }
        }
    }
}
