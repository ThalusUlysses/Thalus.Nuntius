using System;
using TraceBook.Contracts;

namespace TraceBook.Writers
{
    /// <summary>
    /// Implements the <see cref="ITraceWriter"/> functionality for an unspecified
    /// text writer
    /// </summary>
    public class UnspecifiedTextWriter : ITraceWriter
    {
        private Level _level;
        private Action<ITraceEntry> _writeLine;

        /// <summary>
        /// Create san instance of <see cref="UnspecifiedTextWriter"/> initialized with 
        /// the passed parameters
        /// </summary>
        /// <param name="writeLineDelegate">Pass th eto be used writer delegate</param>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ITraceWriter"/></param>
        public UnspecifiedTextWriter(Action<ITraceEntry> writeLineDelegate,Level level)
        {
            _level = level;
            _writeLine = writeLineDelegate;
        }

        /// <inheritdoc />
        public void Write(ITraceEntry entries)
        {
            if (!STrace.IsLog(_level, entries.Level))
            {
                return;
            }

            _writeLine(entries);
        }

        /// <inheritdoc />
        public void SetLevels(Level cats)
        {
            lock (_writeLine)
            {
                _level = cats;
            }
        }
    }
}