using System.Diagnostics;
using TraceBook.Contracts;
using TraceBook.Stringify;

namespace TraceBook.Writers
{
    /// <summary>
    /// Implements the <see cref="ITraceWriter"/> functionality as Trace writer
    /// using <see cref="UnspecifiedTextWriter"/>
    /// </summary>
    public class TraceTextWriter : UnspecifiedTextWriter
    {

        /// <summary>
        /// Creates an instance of <see cref="TraceTextWriter"/> initialized with the
        /// passed parameters
        /// </summary>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ITraceWriter"/></param>
        /// <remarks>Please note that the <see cref="JsonStringifier"/> is used</remarks>
        public TraceTextWriter(Level level) : this(new JsonStringifier(), level)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="TraceTextWriter"/> initialized with the
        /// passed parameters
        /// </summary>
        /// <param name="stringifier">Pass the <see cref="IStringifier"/> to be used</param>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ITraceWriter"/></param>
        public TraceTextWriter(IStringifier stringifier, Level level) : base(i =>
        {
            Trace.WriteLine(stringifier.Stringify(i));
        }, level)
        {
        }
    }
}