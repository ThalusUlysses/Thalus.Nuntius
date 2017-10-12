using System;
using System.Collections.Generic;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Tracing.Contracts;

namespace Thalus.Nuntius.Core.Tracing
{
    public class TraceEntryFacade : ITraceEntryFacade
    {
        public TraceEntryFacade(IEntry e)
        {
            Text = e.GetData<string>("text");
            Scope = e.GetData<string>("scope");
            Caller = e.GetData<string>("caller");
            Line = e.GetData<int>("line");
            Level = e.GetData<Level>("level");
            Extra = e.Extra;
            UtcStamp = e.GetData<DateTime>("utc-stamp");
        }
    
        public string Text { get; }
        public string Scope { get; }
        public string Caller { get; }
        public string File { get; }
        public int Line { get; }
        public Level Level { get; }
        public IDictionary<string, string> Tags { get; }
        public object Extra { get; }

        public DateTime UtcStamp { get; }
    }
}