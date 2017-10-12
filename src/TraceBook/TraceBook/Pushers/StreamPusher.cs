using System;
using System.IO;
using Thalus.Nuntius.Core.Contracts;

namespace Thalus.Nuntius.Core.Writers
{
    /// <summary>
    /// Implements the <see cref="ILeveledPusher"/> to write on <see cref="Stream"/>
    /// </summary>
    public class StreamPusher<TType> : ILeveledPusher<TType>, IDisposable where TType: ILeveledEntry
    {
        private StreamWriter _stm;
        private Level _logLevels;
        private IStringifier<TType> _stringifier;

        /// <summary>
        /// Creates an instance of <see cref="StreamPusher{TType}"/> with the passed parameters
        /// </summary>
        /// <param name="stm">Pass the <see cref="Stream"/> to write to</param>
        /// <param name="stringifier">Pass the to be used <see cref="IStringifier"/></param>
        /// <param name="logLevels">Pass the <see cref="Level"/> flags associated with the <see cref="ILeveledPusher"/></param>
        public StreamPusher(Stream stm, IStringifier<TType> stringifier, Level logLevels)
        {
            _logLevels = logLevels;
            _stringifier = stringifier;
            _stm = new StreamWriter(stm);
        }

        /// <inheritdoc />
        public void Push(TType entries)
        {
            if (!SLevel.IsLog(_logLevels, entries.Level))
            {
                return;
            }

            var st = _stringifier.Stringify(entries);
            _stm.WriteLine(st);
            _stm.Flush();
        }

        /// <inheritdoc />
        public void SetLevels(Level cats)
        {
            lock (_stm)
            {
                _logLevels = cats;
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