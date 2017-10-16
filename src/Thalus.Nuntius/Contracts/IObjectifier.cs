namespace Thalus.Nuntius.Contracts
{
    /// <summary>
    ///  Publishes the public memebers of <see cref="IObjectifier{TType}"/> such like <see cref="Objectify"/>
    /// Converts a <see cref="string"/> to a discrete <see cref="object"/> of <see>
    ///         <cref>TType</cref>
    ///     </see>
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public interface IObjectifier<out TType>
    {
        /// <summary>
        /// Gets a stirng representation of a passed anonymus object
        /// </summary>
        /// <param name="obj">Pass the to be stringified object</param>
        /// <returns>Returns a string representation of the passed object</returns>
        TType Objectify(string obj);
    }
}