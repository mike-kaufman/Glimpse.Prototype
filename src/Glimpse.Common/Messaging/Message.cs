﻿using System;
using System.Collections.Generic;
using Glimpse.Internal;
using Newtonsoft.Json;

namespace Glimpse
{

    public class Message : IMessage
    {
        public Guid Id { get; set; }

        public IEnumerable<string> Types { get; set; }

        [JsonConverter(typeof(RawStringConverter))]
        public string Payload { get; set; }

        public int Ordinal { get; set; }

        public MessageContext Context { get; set; }
        
        public IReadOnlyDictionary<string, object> Indices { get; set; }
    }
}