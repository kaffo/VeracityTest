﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VeracityTest.Consumers
{
    public enum ConsumerType
    {
        CONSOLE,
        FILE
    }

    public interface IConsumer
    {
        void OnItemAdded();
    }
}
