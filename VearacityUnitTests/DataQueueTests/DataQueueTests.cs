using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VeracityTest.Consumers;
using VeracityTest.Data;
using Moq;

namespace VearacityUnitTests.DataQueueTests
{
    [TestFixture]
    class DataQueueTests
    {
        [Test]
        public void CheckDataQueueInputCountMatchesOutputCount()
        {
            IDataQueue dataQueue = new DataQueue();

            var consumer = new Mock<IConsumer>();
            int inCount = 10;
            int outCount = 0;

            consumer.Setup(c => c.OnItemAdded()).Callback(() => outCount++);

            dataQueue.RegisterListener(ConsumerType.CONSOLE, consumer.Object);

            for (int i = 0; i < inCount; i++)
            {
                dataQueue.AddItem(new DataItem(ConsumerType.CONSOLE, ""));
            }

            Assert.That(inCount.Equals(outCount), $"Expected outCount to be {inCount} but was {outCount}");
        }

        [Test]
        public void CheckDataQueueDequeueTypeCorrect()
        {
            IDataQueue dataQueue = new DataQueue();

            var consumer = new Mock<IConsumer>();
            ConsumerType inType = ConsumerType.CONSOLE;
            ConsumerType outType = ConsumerType.FILE;

            consumer.Setup(c => c.OnItemAdded()).Callback(() => outType = dataQueue.GetNextItem(inType).ConsumerType);

            dataQueue.RegisterListener(inType, consumer.Object);

            dataQueue.AddItem(new DataItem(inType, "DUMMY"));

            Assert.That(inType.Equals(outType), $"Expected type to be {inType} but was {outType}");
        }
    }
}
