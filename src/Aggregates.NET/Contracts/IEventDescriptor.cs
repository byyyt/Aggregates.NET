﻿using System;
using System.Collections.Generic;

namespace Aggregates.Contracts
{
    public interface IEventDescriptor
    {
        string EntityType { get; }

        int Version { get; }
        DateTime Timestamp { get; }

        IDictionary<string, string> Headers { get; }
    }
}