using System;

namespace Thalus.Nuntius.Contracts
{
    /// <summary>
    /// Defines the log levels suhc like <see cref="Error"/>
    /// </summary>
    [Flags]
    public enum Level
    {
        /// <summary>
        /// Error level
        /// </summary>
        Error = 8,
        /// <summary>
        /// Warning level
        /// </summary>
        Warning = 4,
        /// <summary>
        /// Info level
        /// </summary>
        Info = 2,
        /// <summary>
        /// Debug Level
        /// </summary>
        Debug = 1,
        /// <summary>
        /// Fatal level
        /// </summary>
        Fatal = 16
    }
}
