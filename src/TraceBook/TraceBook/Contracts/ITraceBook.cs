using System.Runtime.CompilerServices;

namespace TraceBook.Contracts
{
    /// <summary>
    /// Publises the public members of <see cref="ITraceBook"/> such like <see cref="Errors"/>
    /// or <see cref="Warning"/> . Front facade of logging at all.
    /// </summary>
    public interface ITraceBook
    {
        /// <summary>
        /// Writes an error log entry to registered or underlying <see cref="ITraceWriter"/>s.
        /// </summary>
        /// <param name="text">Pass tex that is associated with the error occured</param>
        /// <param name="obj">Pass a set of object that you want to be logged additionally</param>
        /// <param name="caller">Do not pass anything. Caller name is added via <see cref="CallerMemberNameAttribute"/></param>
        /// <param name="filePath">Do not pass anything. Caller file path is added via <see cref="CallerFilePathAttribute"/></param>
        /// <param name="line">Do not pass anything. Caller line number is added via <see cref="CallerLineNumberAttribute"/></param>
        void Errors(string text, object[] obj = null, [CallerMemberName] string caller = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int line = -1);

        /// <summary>
        /// Writes a warning log entry to registered or underlying <see cref="ITraceWriter"/>s.
        /// </summary>
        /// <param name="text">Pass text that is associated with the warning occured</param>
        /// <param name="obj">Pass a set of object that you want to be logged additionally</param>
        /// <param name="caller">Do not pass anything. Caller name is added via <see cref="CallerMemberNameAttribute"/></param>
        /// <param name="filePath">Do not pass anything. Caller file path is added via <see cref="CallerFilePathAttribute"/></param>
        /// <param name="line">Do not pass anything. Caller line number is added via <see cref="CallerLineNumberAttribute"/></param>
        void Warning(string text, object[] obj = null, [CallerMemberName] string caller = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int line = -1);

        /// <summary>
        /// Writes a debug log entry to registered or underlying <see cref="ITraceWriter"/>s.
        /// </summary>
        /// <param name="text">Pass text that is associated with the debug message occured</param>
        /// <param name="obj">Pass a set of object that you want to be logged additionally</param>
        /// <param name="caller">Do not pass anything. Caller name is added via <see cref="CallerMemberNameAttribute"/></param>
        /// <param name="filePath">Do not pass anything. Caller file path is added via <see cref="CallerFilePathAttribute"/></param>
        /// <param name="line">Do not pass anything. Caller line number is added via <see cref="CallerLineNumberAttribute"/></param>
        void Debug(string text, object[] obj = null, [CallerMemberName] string caller = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int line = -1);

        /// <summary>
        /// Writes a info log entry to registered or underlying <see cref="ITraceWriter"/>s.
        /// </summary>
        /// <param name="text">Pass text that is associated with the info message occured</param>
        /// <param name="obj">Pass a set of object that you want to be logged additionally</param>
        /// <param name="caller">Do not pass anything. Caller name is added via <see cref="CallerMemberNameAttribute"/></param>
        /// <param name="filePath">Do not pass anything. Caller file path is added via <see cref="CallerFilePathAttribute"/></param>
        /// <param name="line">Do not pass anything. Caller line number is added via <see cref="CallerLineNumberAttribute"/></param>
        void Info(string text, object[] obj = null, [CallerMemberName] string caller = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int line = -1);

        /// <summary>
        /// Writes a fatal log entry to registered or underlying <see cref="ITraceWriter"/>s.
        /// </summary>
        /// <param name="text">Pass text that is associated with the fatal message occured</param>
        /// <param name="obj">Pass a set of object that you want to be logged additionally</param>
        /// <param name="caller">Do not pass anything. Caller name is added via <see cref="CallerMemberNameAttribute"/></param>
        /// <param name="filePath">Do not pass anything. Caller file path is added via <see cref="CallerFilePathAttribute"/></param>
        /// <param name="line">Do not pass anything. Caller line number is added via <see cref="CallerLineNumberAttribute"/></param>
        void Fatal(string text, object[] obj = null, [CallerMemberName] string caller = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int line = -1);
    }
}
