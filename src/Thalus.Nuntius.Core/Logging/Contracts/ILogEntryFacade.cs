using System;
using System.Collections.Generic;
using System.Globalization;
using Thalus.Nuntius.Core.Contracts;

namespace Thalus.Nuntius.Core.Logging.Contracts
{

    /// <summary>
    /// Publishes the public members of <see cref="ILogEntryFacade"/> such like
    /// <see cref="InvariantText"/> or <see cref="Text"/>. Use this to build up a 
    /// facade on <see cref="IEntry"/>
    /// </summary>
    public interface ILogEntryFacade
    {
        /// <summary>
        /// Gets the <see cref="CultureInfo.InvariantCulture"/> text of the <see cref="IEntry"/>
        /// </summary>
        string InvariantText { get; }

        /// <summary>
        /// Gets the <see cref="CultureInfo.CurrentUICulture"/> text of the <see cref="IEntry"/>
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Gets the scope of logging as <see cref="string"/> such like "general", "UI", "Device", ...
        /// </summary>
        string Scope { get; }

        /// <summary>
        /// Gets the <see cref="Level"/> if the log entry such like <see>
        ///         <cref>Level.Error</cref>
        ///     </see>
        ///     of
        /// </summary>
        Level Level { get; }

        /// <summary>
        /// Gets the <see cref="Tags"/> associated with th e<see cref="ILogEntryFacade"/>
        /// </summary>
        IDictionary<string, string> Tags { get; }

        /// <summary>
        /// Gets the <see cref="Extra"/> data associated with the <see cref="ILogEntryFacade"/> as <see cref="object"/>
        /// </summary>
        object Extra { get; }

        /// <summary>
        /// Gets the <see cref="DateTime"/> time stamp as UTC
        /// </summary>
        DateTime UtcStamp { get; }
    }
}