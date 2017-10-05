using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TraceBook.Contracts;

namespace TraceBook
{
    [Serializable]
    [DataContract]
    class TraceEntry : ITraceEntry
    {
        [DataMember(Name = "utc-stamp", Order = 0)]
        public DateTime UtcStamp { get; set; }

        [DataMember(Name = "text", Order = 3)]
        public string Text { get; set; }

        [DataMember(Name = "level", Order = 1)]
        public Level Level { get; set; }

        [DataMember(Name = "scope", Order = 2)]
        public string Scope { get; set; }

        [DataMember(Name = "meta", Order = 4)]
        public IDictionary<string, string> Tags { get; set; }

        [DataMember(Name = "data", Order = 5)]
        public object[] Data { get; set; }
    }
}