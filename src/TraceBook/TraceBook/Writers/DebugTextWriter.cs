using System.Diagnostics;
using TraceBook.Contracts;
using TraceBook.Stringify;

namespace TraceBook.Writers
{
    /// <summary>
    /// Implements a <see cref="ITraceWriter"/> functionality as Debug trace writer
    /// using unerdlying <see cref="UnspecifiedTextWriter"/>
    /// </summary>
    public class DebugTextWriter : UnspecifiedTextWriter
    {
        /// <summary>
        /// Creates an instance of <see cref="DebugTextWriter"/> initalized with the
        /// passed parameters
        /// </summary>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ITraceWriter"/></param>
        /// <remarks>Please note <see cref="JsonStringifier"/> is used</remarks>
        public DebugTextWriter(Level level) : this(new JsonStringifier(),level)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="DebugTextWriter"/> initialized with the
        /// passed parameters
        /// </summary>
        /// <param name="stringifier">Pass a stringgifier that you like to use <see cref="JsonStringifier"/></param>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ITraceWriter"/></param>
        public DebugTextWriter(IStringifier stringifier, Level level) : base(
            o => { Trace.WriteLine(stringifier.Stringify(o)); }, level)
        {

        }
    }
}