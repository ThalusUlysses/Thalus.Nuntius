namespace Thalus.Nuntius.Contracts
{
    /// <summary>
    /// Publishes the public memebrs of <see cref="IStringifier{TType}"/> such like
    /// <see cref="Stringify"/> used to serialize or make a string equivalent
    /// of an anonymos object.
    /// </summary>
    public interface IStringifier<in TType>
    {
        /// <summary>
        /// Gets a stirng representation of a passed anonymus object
        /// </summary>
        /// <param name="obj">Pass the to be stringified object</param>
        /// <returns>Returns a string representation of the passed object</returns>
        string Stringify(TType obj);
    }
}