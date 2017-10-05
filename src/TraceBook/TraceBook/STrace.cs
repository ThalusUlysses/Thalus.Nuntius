using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TraceBook.Contracts;

namespace TraceBook
{
    /// <summary>
    /// Grants statci access to tracing
    /// </summary>
    public static class STrace
    {
        private static List<ITraceWriter> _writers = new List<ITraceWriter>();
        private static IDictionary<string, ITraceBook> _books = new ConcurrentDictionary<string, ITraceBook>();

        /// <summary>
        /// Registers a trace write to underlying <see cref="ITraceWriter"/> colleciton
        /// </summary>
        /// <param name="writer">Pass the tobe registered <see cref="ITraceWriter"/></param>
        public static void Register(ITraceWriter writer)
        {
            lock (_writers)
            {
                _writers.Add(writer);
            }
        }

        /// <summary>
        /// Cleans up all stataic resources used by <see cref="STrace"/>
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
        /// Creates a <see cref="ITraceEntry"/> initialized with the passed parameters
        /// </summary>
        /// <param name="level">Pass the trace level of the <see cref="ITraceEntry"/></param>
        /// <param name="scope">Pass the scope of the <see cref="ITraceEntry"/>. Is "general" if not passed</param>
        /// <param name="text">Pass the text associated with the <see cref="ITraceEntry"/>. Is "null" if not passed</param>
        /// <param name="obj">Pass teh additional extra data of the <see cref="ITraceEntry"/>. Is "null" if not passed</param>
        /// <param name="caller">Do not pass anything. Is added by using <see cref="CallerMemberNameAttribute"/></param>
        /// <param name="filePath">Do not pass anything. Is added by using <see cref="CallerFilePathAttribute"/></param>
        /// <param name="line">Do not pass anything. Is added by using <see cref="CallerLineNumberAttribute"/></param>
        /// <returns></returns>
        public static ITraceEntry Entry(Level level, string scope= "general", string text = null, object[] obj = null,
            [CallerMemberName] string caller = null, [CallerFilePath] string filePath = null,
            [CallerLineNumber] int line = -1)
        {
            return InternalEntry(level, scope, text, obj, DateTime.UtcNow, caller, filePath, line);
        }

        /// <summary>
        /// Creates a <see cref="ITraceEntry"/> initialized with the passed parameters
        /// </summary>
        /// <param name="level">Pass the trace level of the <see cref="ITraceEntry"/></param>
        /// <param name="scope">Pass the scope of the <see cref="ITraceEntry"/>. Is "general" if not passed</param>
        /// <param name="text">Pass the text associated with the <see cref="ITraceEntry"/>. Is "null" if not passed</param>
        /// <param name="obj">Pass the additional extra data of the <see cref="ITraceEntry"/>. Is "null" if not passed</param>
        /// <param name="utc">Pass the <see cref="DateTime"/> the trace entry occured as UTC </param>
        /// <param name="caller">Pass the caller name</param>
        /// <param name="filePath">Pass the file name</param>
        /// <param name="line">pass the line number</param>
        internal static ITraceEntry InternalEntry(Level level, string scope, string text = null, object[] obj = null, DateTime utc = default(DateTime),
            string caller = null, string filePath = null,
            int line = -1)
        {
            if (utc == default(DateTime))
            {
                utc = DateTime.UtcNow;
            }

            return new TraceEntry
            {
                UtcStamp = utc,
                Text = text,
                Data = obj,
                Scope = scope,
                Level = level,
                Tags = new Dictionary<string, string>
                {
                    {"Caller", caller},
                    {"File", filePath},
                    {"line", line.ToString()}
                }
            };
        }

        /// <summary>
        /// Checks if passed log categories match the passed level
        /// </summary>
        /// <param name="level">Level supported </param>
        /// <param name="cat">level of trace entry</param>
        /// <returns>Returns true if matches, othewise false</returns>
        public static bool IsLog(Level level, Level cat)
        {
            return (level & cat) > 0;
        }

        /// <summary>
        /// Checks of the passed <see cref="Level"/> is active
        /// </summary>
        /// <param name="cat">Pass teh to be checked <see cref="Level"/></param>
        /// <returns>Returns true if <see cref="Level"/> is active otherwise false</returns>
        public static bool IsLogFatal(Level cat)
        {
            return ((int)cat & 16) > 0;

        }

        /// <summary>
        /// Checks of the passed <see cref="Level"/> is active
        /// </summary>
        /// <param name="cat">Pass teh to be checked <see cref="Level"/></param>
        /// <returns>Returns true if <see cref="Level"/> is active otherwise false</returns>
        public static bool IsLogWarning(Level cat)
        {
            return ((int)cat & 4) > 0;

        }

        /// <summary>
        /// Checks of the passed <see cref="Level"/> is active
        /// </summary>
        /// <param name="cat">Pass teh to be checked <see cref="Level"/></param>
        /// <returns>Returns true if <see cref="Level"/> is active otherwise false</returns>
        public static bool IsLogErrors(Level cat)
        {
            return ((int)cat & 8) > 0;

        }

        /// <summary>
        /// Checks of the passed <see cref="Level"/> is active
        /// </summary>
        /// <param name="cat">Pass teh to be checked <see cref="Level"/></param>
        /// <returns>Returns true if <see cref="Level"/> is active otherwise false</returns>
        public static bool IsLogInfo(Level cat)
        {
            return ((int)cat & 2) > 0;
        }

        /// <summary>
        /// Checks of the passed <see cref="Level"/> is active
        /// </summary>
        /// <param name="cat">Pass teh to be checked <see cref="Level"/></param>
        /// <returns>Returns true if <see cref="Level"/> is active otherwise false</returns>
        public static bool IsLogDebug(Level cat)
        {
            return ((int) cat & 1) > 0;
        }

        /// <summary>
        /// Gets an instance of <see cref="ITraceBook"/> associated with the passed scope
        /// </summary>
        /// <param name="scope">Pass the scope of trace book you like</param>
        /// <returns>Returns an instance of <see cref="ITraceBook"/> for the passed <see cref="scope"/></returns>
        public static ITraceBook Get(string scope = "general")
        {
            ITraceBook book;

            List<ITraceWriter> writers;
            lock (_writers)
            {
                writers = _writers.ToList();
            }

            lock (_books)
            {
                if (!_books.TryGetValue(scope, out book))
                {
                    book = new Tracebook(writers, scope);
                    _books[scope] = book;
                }
            }

            return book;
        }
    }
}