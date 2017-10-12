using System;
using System.Collections.Generic;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Tracing.Contracts;

namespace Thalus.Nuntius.Core.Tracing
{
    /// <summary>
    /// Implements the <see cref="ITraceEntryFacade"/> functionality such like <see cref="Scope"/>
    /// or <see cref="Caller"/>
    /// </summary>
    public class TraceEntryFacade : ITraceEntryFacade
    {
        /// <summary>
        /// Creates an instance of <see cref="TraceEntryFacade"/> with the passed <see cref="IEntry"/>
        /// </summary>
        /// <param name="e">pass the <see cref="IEntry"/> to create facade from</param>
        public TraceEntryFacade(IEntry e)
        {
            Text = e.GetData<string>("text");
            Scope = e.GetData<string>("scope");
            Caller = e.GetData<string>("caller");
            Line = e.GetData<int>("line");
            File = e.GetData<string>("file");
            Level = e.GetData<Level>("level");
            Extra = e.Extra;
            UtcStamp = e.GetData<DateTime>("utc-stamp");

            Tags = StripTags(e.Tags);
        }

        private IDictionary<string, string> StripTags(IDictionary<string, string> d)
        {
            Dictionary<string,string> items = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in d)
            {
                switch (pair.Key)
                {
                    case "text":
                    case "scope":
                    case "caller":
                    case "line":
                    case "file":
                    case "level":
                    case "utc-stamp":
                        continue;
                }

                items[pair.Key] = pair.Value;
            }

            return items;
        }

        ///<inheritdoc cref="ITraceEntryFacade.Text"/>
        public string Text { get; }

        ///<inheritdoc cref="ITraceEntryFacade.Scope"/>
        public string Scope { get; }

        ///<inheritdoc cref="ITraceEntryFacade.Caller"/>
        public string Caller { get; }

        ///<inheritdoc cref="ITraceEntryFacade.File"/>
        public string File { get; }

        ///<inheritdoc cref="ITraceEntryFacade.Line"/>
        public int Line { get; }

        ///<inheritdoc cref="ITraceEntryFacade.Level"/>
        public Level Level { get; }

        ///<inheritdoc cref="ITraceEntryFacade.Tags"/>
        public IDictionary<string, string> Tags { get; }

        ///<inheritdoc cref="ITraceEntryFacade.Extra"/>
        public object Extra { get; }

        ///<inheritdoc cref="ITraceEntryFacade.UtcStamp"/>
        public DateTime UtcStamp { get; }
    }
}