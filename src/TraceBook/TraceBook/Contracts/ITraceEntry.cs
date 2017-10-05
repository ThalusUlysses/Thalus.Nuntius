using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TraceBook.Contracts
{
    /// <summary>
    /// Publishes the public memebers of <see cref="ITraceEntry"/> such
    /// like <see cref="UtcStamp"/> or <see cref="Text"/>.
    /// </summary>
    public interface ITraceEntry
    {
        /// <summary>
        /// Gets the time stamp when the trace entry has been logged ans <see cref="DateTime"/>
        /// </summary>
        [DataMember(Name = "stamp", Order = 0)]
        DateTime UtcStamp { get; }

        /// <summary>
        /// Gets the text assiciated with the <see cref="ITraceEntry"/>. A Descriptive user
        /// readable text
        /// </summary>
        [DataMember(Name = "text", Order = 3 )]
        string Text { get; }

        /// <summary>
        /// Gets the <see cref="Contracts.Level"/> of the <see cref="ITraceEntry"/>. Use only one flag
        /// to log it.
        /// </summary>
        [DataMember(Name = "level", Order = 1)]
        Level Level { get; }
      
        /// <summary>
        /// Gets the scope of the <see cref="ITraceEntry"/> has been logged in. Basically this is
        /// flagged as "general" if not set
        /// </summary>
        [DataMember(Name = "scope", Order = 2)]
        string Scope { get; }
        
        /// <summary>
        /// Gets the tags that are associated with teh <see cref="ITraceEntry"/> such like "caller", "line" or "file"
        /// </summary>
        /// <remarks>
        /// Please note tags can be added additionally to customize.</remarks>
        [DataMember(Name = "tags",Order = 4)]
        IDictionary<string, string> Tags { get; }

        /// <summary>
        /// Gets additional data items that are associated with the <see cref="ITraceEntry"/>
        /// </summary>
        [DataMember(Name = "data", Order = 5)]
        object[] Data { get; }
    }
}