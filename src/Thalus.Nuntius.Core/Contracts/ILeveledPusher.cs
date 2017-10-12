namespace Thalus.Nuntius.Core.Contracts
{
    /// <summary>
    /// Publishes the public memebers of <see cref="ILeveledPusher{Type}"/> such 
    /// like <see cref="SetLevels"/>. Used as thin interface for teh underlying writers
    /// such like <see>
    ///         <cref>SingleFileWriter</cref>
    ///     </see>
    /// </summary>
    public interface ILeveledPusher<in TType> : IPusher<TType> where TType : ILeveledEntry
    {

        /// <summary>
        ///  Set the <see cref="Level"/> that are associated with the <see cref="ILeveledPusher{TType}"/>
        /// </summary>
        /// <param name="cats">Pass the to be logged flags</param>
        void SetLevels(Level cats);
    }
}