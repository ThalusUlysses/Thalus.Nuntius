using System;
using System.Collections.Generic;
using Thalus.Nuntius.Contracts;
using Thalus.Nuntius.Logging.Contracts;

namespace Thalus.Nuntius.Logging
{
    /// <summary>
    /// Implements a facade for <see cref="ILogEntryFacade"/> using <see cref="IEntry"/> as seed
    /// </summary>
    public class LogEntryFacade : ILogEntryFacade
    {
        /// <summary>
        /// Creates an instance of <see cref="LogEntryFacade"/> using passed <see cref="IEntry"/>
        /// as seed
        /// </summary>
        /// <param name="e">Pass corresponding <see cref="IEntry"/> as seed</param>
        public LogEntryFacade(IEntry e)
        {
            Text = e.GetData<string>("text");
            InvariantText = e.GetData<string>("invariant-text");
            Scope = e.GetData<string>("scope");
            Level = e.GetData<Level>("level");
            Extra = e.Extra;
            UtcStamp = e.GetData<DateTime>("utc-stamp");

            Tags = e.Tags;
            Extra = e.Extra;
        }
        ///<inheritdoc cref="ILogEntryFacade.InvariantText"/>
        public string InvariantText { get; }

        ///<inheritdoc cref="ILogEntryFacade.Text"/>
        public string Text { get; }

        ///<inheritdoc cref="ILogEntryFacade.Scope"/>
        public string Scope { get; }

        ///<inheritdoc cref="ILogEntryFacade.Level"/>
        public Level Level { get; }

        ///<inheritdoc cref="ILogEntryFacade.Level"/>

        ///<inheritdoc cref="IEntry.Tags"/>
        public IDictionary<string, string> Tags { get; }

        ///<inheritdoc cref="IEntry.Extra"/>
        public object Extra { get; }

        ///<inheritdoc cref="ILogEntryFacade.UtcStamp"/>
        public DateTime UtcStamp { get; }
    }
}