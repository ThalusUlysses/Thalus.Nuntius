using System;
using Thalus.Nuntius.Contracts;

namespace Thalus.Nuntius.Pushers
{
    /// <summary>
    /// Implements the <see cref="ILeveledPusher{T}"/> functionality for an unspecified
    /// text writer
    /// </summary>
    public class UnspecifiedPusher<TType> : ILeveledPusher<TType> where TType : ILeveledEntry
    {
        private Level _level;
        private readonly Action<TType> _writeLine;

        /// <summary>
        /// Create san instance of <see cref="UnspecifiedPusher{TType}"/> initialized with 
        /// the passed parameters
        /// </summary>
        /// <param name="writeLineDelegate">Pass th eto be used writer delegate</param>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ILeveledPusher{T}"/></param>
        public UnspecifiedPusher(Action<TType> writeLineDelegate,Level level)
        {
            _level = level;
            _writeLine = writeLineDelegate;
        }

        /// <inheritdoc />
        public void Push(TType entries)
        {
            if (!SLevel.IsLog(_level, entries.Level))
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