using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Thalus.Nuntius.Contracts;

namespace Thalus.Nuntius.Tracing.Contracts
{
    /// <summary>
    /// Publishes the public members of <see cref="ITraceEntryFacade"/> such like <see cref="Scope"/>
    /// or <see cref="Text"/>. Creates a facade based on <see cref="IEntry"/>
    /// </summary>
    public interface ITraceEntryFacade 
    {
        /// <summary>
        /// Gets thge <see cref="Text"/> if the trace entry as <see cref="string"/>
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Gets the <see cref="Scope"/> of teh <see cref="IEntry"/> as <see cref="string"/>
        /// </summary>
        string Scope { get; }

        /// <summary>
        /// Gets the <see cref="CallerMemberNameAttribute"/> of the <see cref="IEntry"/> as <see cref="string"/>
        /// </summary>
        string Caller { get; }

        /// <summary>
        /// Gets the <see cref="CallerFilePathAttribute"/> of the <see cref="IEntry"/> as <see cref="string"/>
        /// </summary>
        string File { get; }

        /// <summary>
        /// Gets the <see cref="CallerLineNumberAttribute"/> of the <see cref="IEntry"/> as <see cref="int"/>
        /// </summary>
        int Line { get; }

        /// <summary>
        /// Gets the <see cref="Tags"/> of the <see cref="IEntry"/> as <see cref="IDictionary{TKey,TValue}"/>
        /// </summary>
        IDictionary<string,string> Tags { get; }

        /// <summary>
        /// Gets the <see cref="Level"/> of the <see cref="IEntry"/> as <see cref="Level"/>
        /// </summary>
        Level Level { get; }

        /// <summary>
        /// Gets extra data of the <see cref="IEntry"/> as <see cref="object"/>
        /// </summary>
        object Extra { get; }

        /// <summary>
        /// gets th eUTC time stamp of the <see cref="IEntry"/> as <see cref="DateTime"/>
        /// </summary>
        DateTime UtcStamp { get; }
    }
}