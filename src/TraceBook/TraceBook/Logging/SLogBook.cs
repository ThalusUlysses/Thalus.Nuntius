using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Logging.Contracts;
using Thalus.Nuntius.Core.Tracing;
using Thalus.Nuntius.Core.Tracing.Contracts;

namespace Thalus.Nuntius.Core.Logging
{
    /// <summary>
    /// Static access to all log book components
    /// </summary>
    public static class SLogBook
    {
        private static List<ILeveledPusher<ILeveledEntry>> _writers = new List<ILeveledPusher<ILeveledEntry>>();
        private static IDictionary<string, ILogBook> _books = new ConcurrentDictionary<string, ILogBook>();

        /// <summary>
        /// Registers a trace write to underlying <see cref="ILeveledPusher{T}"/> colleciton
        /// </summary>
        /// <param name="writer">Pass the tobe registered <see cref="ILeveledPusher{T}"/></param>
        public static void Register(ILeveledPusher<ILeveledEntry> writer)
        {
            lock (_writers)
            {
                _writers.Add(writer);
            }
        }

        /// <summary>
        /// Cleans up all stataic resources used by <see cref="STraceBook"/>
        /// </summary>
        public static void Cleanup()
        {
            lock (_writers)
            {
                _writers = null;
                _books = null;
            }
        }

        /// <summary>
        /// Creates a <see cref="IEntry"/> initialized with the passed parameters
        /// </summary>
        /// <param name="level">Pass the trace level of the <see cref="IEntry"/></param>
        /// <param name="scope">Pass the scope of the <see cref="IEntry"/>. Is "general" if not passed</param>
        /// <param name="innvariantText"></param>
        /// <param name="text">Pass the text associated with the <see cref="IEntry"/>. Is "null" if not passed</param>
        /// <param name="obj">Pass the additional extra data of the <see cref="IEntry"/>. Is "null" if not passed</param>
        /// <param name="utc">Pass the <see cref="DateTime"/> the trace entry occured as UTC </param>
        internal static IEntry InternalEntry(Level level, string scope, string innvariantText, string text = null,
            object[] obj = null, DateTime utc = default(DateTime))
        {
            if (utc == default(DateTime))
            {
                utc = DateTime.UtcNow;
            }
            return new Entry
            {
                Level = level,
                Tags = new Dictionary<string, string>
                {
                    {"scope", scope},
                    {"invariant-text", innvariantText},
                    {"text", text},
                    {"utc-stamp", utc.ToString(CultureInfo.InvariantCulture)},
                },
                Extra = obj
            };
        }

        /// <summary>
        /// Creates a <see cref="IEntry"/> initialized with the passed parameters
        /// </summary>
        /// <param name="level">Pass the trace level of the <see cref="IEntry"/></param>
        /// <param name="scope">Pass the scope of the <see cref="IEntry"/>. Is "general" if not passed</param>
        /// <param name="invarianttext">Pass the invariant text associated with the <see cref="IEntry"/>. Is "null" if not passed</param>
        /// <param name="text">Pass the text associated with the <see cref="IEntry"/>. Is "null" if not passed</param>
        /// <param name="obj">Pass teh additional extra data of the <see cref="IEntry"/>. Is "null" if not passed</param>
        /// <returns></returns>
        public static ILogEntryFacade Entry(Level level, string invarianttext, string text = null, string scope = "general", object[] obj = null)
        {
            var item = InternalEntry(level, scope, invarianttext, text);
            return new LogEntryFacade(item);
        }

        /// <summary>
        /// Gets an instance of <see cref="ITraceBook"/> associated with the passed scope
        /// </summary>
        /// <param name="scope">Pass the scope of trace book you like</param>
        /// <returns>Returns an instance of <see cref="ITraceBook"/> for the passed <see cref="scope"/></returns>
        public static ILogBook Get(string scope = "general")
        {
            ILogBook book;

            List<ILeveledPusher<ILeveledEntry>> writers;
            lock (_writers)
            {
                writers = _writers.ToList();
            }

            lock (_books)
            {
                if (!_books.TryGetValue(scope, out book))
                {
                    book = new LogBook(writers, scope);
                    _books[scope] = book;
                }
            }

            return book;
        }
    }
}