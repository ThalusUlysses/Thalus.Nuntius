using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Thalus.Nuntius.Contracts;
using Thalus.Nuntius.Tracing.Contracts;

namespace Thalus.Nuntius.Tracing
{
    /// <summary>
    /// Grants statci access to tracing
    /// </summary>
    public static class STraceBook
    {
        private static List<ILeveledPusher<ILeveledEntry>> _writers = new List<ILeveledPusher<ILeveledEntry>>();
        private static IDictionary<string, ITraceBook> _books = new ConcurrentDictionary<string, ITraceBook>();

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
        /// <param name="text">Pass the text associated with the <see cref="IEntry"/>. Is "null" if not passed</param>
        /// <param name="obj">Pass teh additional extra data of the <see cref="IEntry"/>. Is "null" if not passed</param>
        /// <param name="caller">Do not pass anything. Is added by using <see cref="CallerMemberNameAttribute"/></param>
        /// <param name="filePath">Do not pass anything. Is added by using <see cref="CallerFilePathAttribute"/></param>
        /// <param name="line">Do not pass anything. Is added by using <see cref="CallerLineNumberAttribute"/></param>
        /// <returns></returns>
        public static ITraceEntryFacade Entry(Level level, string scope= "general", string text = null, object[] obj = null,
            [CallerMemberName] string caller = null, [CallerFilePath] string filePath = null,
            [CallerLineNumber] int line = -1)
        {
            var item = InternalEntry(level, scope, text, obj, DateTime.UtcNow, caller, filePath, line);

            return new TraceEntryFacade(item);
        }

        /// <summary>
        /// Creates a <see cref="IEntry"/> initialized with the passed parameters
        /// </summary>
        /// <param name="level">Pass the trace level of the <see cref="IEntry"/></param>
        /// <param name="scope">Pass the scope of the <see cref="IEntry"/>. Is "general" if not passed</param>
        /// <param name="text">Pass the text associated with the <see cref="IEntry"/>. Is "null" if not passed</param>
        /// <param name="obj">Pass the additional extra data of the <see cref="IEntry"/>. Is "null" if not passed</param>
        /// <param name="utc">Pass the <see cref="DateTime"/> the trace entry occured as UTC </param>
        /// <param name="caller">Pass the caller name</param>
        /// <param name="filePath">Pass the file name</param>
        /// <param name="line">pass the line number</param>
        internal static IEntry InternalEntry(Level level, string scope, string text = null, object[] obj = null, DateTime utc = default(DateTime),
            string caller = null, string filePath = null,
            int line = -1)
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
                    {"text", text},
                    {"utc-stamp", utc.ToString(CultureInfo.InvariantCulture)},
                    {"caller", caller},
                    {"file", filePath},
                    {"line", line.ToString(CultureInfo.InvariantCulture)},
                },
                Extra = obj
            };
        }

     

        /// <summary>
        /// Gets an instance of <see cref="ITraceBook"/> associated with the passed scope
        /// </summary>
        /// <param name="scope">Pass the scope of trace book you like</param>
        /// <returns>Returns an instance of <see cref="ITraceBook"/> for the passed <see cref="scope"/></returns>
        public static ITraceBook Get(string scope = "general")
        {
            ITraceBook book;

            List<ILeveledPusher<ILeveledEntry>> writers;
            lock (_writers)
            {
                writers = _writers.ToList();
            }

            lock (_books)
            {
                if (!_books.TryGetValue(scope, out book))
                {
                    book = new TraceBook(writers, scope);
                    _books[scope] = book;
                }
            }

            return book;
        }
    }
}