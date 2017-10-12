namespace Thalus.Nuntius.Core.Contracts
{
    /// <summary>
    /// Publishes the public members of <see cref="ILeveledEntry"/> such like <see cref="Level"/>
    /// </summary>
    public interface ILeveledEntry
    {
        /// <summary>
        /// Gets the <see cref="Level"/> of <see cref="Thalus.Nuntius.Core.Logging"/> or <see cref="Thalus.Nuntius.Core.Tracing"/>
        /// as flags
        /// </summary>
        Level Level { get; }
    }
}