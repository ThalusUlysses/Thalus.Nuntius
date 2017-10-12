using System;
using System.Collections.Generic;
using Thalus.Nuntius.Core.Contracts;

namespace Thalus.Nuntius.Core.Tracing.Contracts
{
    public interface ITraceEntryFacade 
    {
        string Text { get; }
        string Scope { get; }
        string Caller { get; }
        string File { get; }
        int Line { get; }
        IDictionary<string,string> Tags { get; }
        Level Level { get; }

        object Extra { get; }

        DateTime UtcStamp { get; }
    }
}