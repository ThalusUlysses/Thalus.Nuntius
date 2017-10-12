using System.Collections.Generic;

namespace Thalus.Nuntius.Core.Contracts
{

    /// <summary>
    /// Publishes the publicmmebers of <see cref="IEntry"/> such like <see cref="Tags"/>
    /// or <see cref="Extra"/>. Used as thin interchange format fo <see cref="Logging"/> or
    /// <see cref="Tracing"/>
    /// </summary>
    public interface IEntry : ILeveledEntry
    {
        /// <summary>
        /// Gets a set of <see cref="string"/> items referenced by key. A losely coupled
        /// <see cref="IDictionary{TKey,TValue}"/> of data
        /// </summary>
        IDictionary<string,string> Tags { get;}

        /// <summary>
        /// Gets the <see cref="Extra"/> data of the <see cref="IEntry"/>. Any unspecified but serializeable
        /// type is allowed here.
        /// </summary>
        object Extra { get;  }

        /// <summary>
        /// Gets data from underlying <see cref="IDictionary{TKey,TValue}"/>  safely by cast
        /// </summary>
        /// <typeparam name="TType">Pas sthe type to cast looked up data to</typeparam>
        /// <param name="key">Pass the key to look for in <see cref="IDictionary{TKey,TValue}"/></param>
        /// <returns>Returns looked up value as <see>
        ///         <cref>TType</cref>
        ///     </see>
        ///     if found, otherwise default of <see>
        ///         <cref>TType</cref>
        ///     </see>
        /// </returns>
        /// <exception cref="InvalidCastException"> Thrown when cast is not possible.</exception>
        TType GetData<TType>(string key);
    }
}