using TraceBook.Writers;

namespace TraceBook.Contracts
{
    /// <summary>
    /// Publishes the public memebers of <see cref="ITraceWriter"/> such 
    /// like <see cref="Write"/>. Used as thin interface for teh underlying writers
    /// such like <see cref="SingleFileWriter"/>
    /// </summary>
    public interface ITraceWriter
    {
        /// <summary>
        /// Writes a passed trace entry to  the underlying trace target
        /// </summary>
        /// <param name="entries">Pass the to be written <see cref="ITraceEntry"/></param>
        void Write(ITraceEntry entries);

        /// <summary>
        ///  Set the <see cref="Level"/> that are associated with the <see cref="ITraceWriter"/>
        /// </summary>
        /// <param name="cats">Pass the to be logged flags</param>
        void SetLevels(Level cats);
    }
}