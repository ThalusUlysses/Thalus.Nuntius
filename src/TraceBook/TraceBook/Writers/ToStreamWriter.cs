using System;
using System.IO;
using TraceBook.Contracts;

namespace TraceBook.Writers
{
    /// <summary>
    /// Implements the <see cref="ITraceWriter"/> to write on <see cref="Stream"/>
    /// </summary>
    public class ToStreamWriter : ITraceWriter, IDisposable
    {
        private StreamWriter _stm;
        private Level _level;
        private IStringifier _stringifier;

        /// <summary>
        /// Creates an instance of <see cref="ToStreamWriter"/> with the passed parameters
        /// </summary>
        /// <param name="stm">Pass the <see cref="Stream"/> to write to</param>
        /// <param name="stringifier">Pass the to be used <see cref="IStringifier"/></param>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ITraceWriter"/></param>
        public ToStreamWriter(Stream stm, IStringifier stringifier, Level level)
        {
            _level = level;
            _stringifier = stringifier;
            _stm = new StreamWriter(stm);
        }

        /// <inheritdoc />
        public void Write(ITraceEntry entries)
        {
            if (!STrace.IsLog(_level, entries.Level))
            {
                return;
            }

            var st = _stringifier.Stringify(entries);
            _stm.WriteLine(st);
        }

        /// <inheritdoc />
        public void SetLevels(Level cats)
        {
            lock (_stm)
            {
                _level = cats;
            }
        }

        /// <summary>
        /// Disposes the passed stream
        /// </summary>
        public void Dispose()
        {
            _stm?.Dispose();
        }
    }
}