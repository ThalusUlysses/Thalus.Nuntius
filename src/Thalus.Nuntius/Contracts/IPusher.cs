using System;
using Thalus.Nuntius.Pushers.File;

namespace Thalus.Nuntius.Contracts
{
    /// <summary>
    /// Publishes the public memebers of <see cref="ILeveledPusher{TType}"/> such 
    /// like <see cref="Push"/>. Used as thin interface for teh underlying writers
    /// such like <see cref="SingleFilePusher{TType}"/>
    /// </summary>
    public interface IPusher<in TType>
    {

        /// <summary>
        /// Writes a passed trace entry to  the underlying trace target
        /// </summary>
        /// <param name="entries">Pass the to be written <see cref="TType"/></param>
        void Push(TType entries);
    }

    /// <summary>
    /// Publishes the public members of <see cref="IReceiver{TType}"/> such like <see cref="EntryReceived"/>
    /// </summary>
    /// <typeparam name="TType">pass the expected type to be received</typeparam>
    public interface IReceiver<out TType>
    {
        /// <summary>
        /// Event is raised when a data item of type <see>
        ///         <cref>TType</cref>
        ///     </see>
        ///     has been received
        /// </summary>
        event Action<TType> EntryReceived;
    }
}