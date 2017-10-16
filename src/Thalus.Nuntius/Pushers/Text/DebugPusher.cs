using System.Diagnostics;
using Thalus.Nuntius.Contracts;
using Thalus.Nuntius.Stringify;

namespace Thalus.Nuntius.Pushers.Text
{
    /// <summary>
    /// Implements a <see cref="ILeveledPusher{T}"/> functionality as Debug trace writer
    /// using unerdlying <see cref="UnspecifiedPusher{TType}"/>
    /// </summary>
    public class DebugPusher<TType> : UnspecifiedPusher<TType>  where TType: ILeveledEntry
    {
        /// <summary>
        /// Creates an instance of <see cref="DebugPusher{TType}"/> initalized with the
        /// passed parameters
        /// </summary>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ILeveledPusher{T}"/></param>
        /// <remarks>Please note <see cref="JsonStringifier{T}"/> is used</remarks>
        public DebugPusher(Level level) : this(new JsonStringifier<TType>(),level)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="DebugPusher{TType}"/> initialized with the
        /// passed parameters
        /// </summary>
        /// <param name="stringifier">Pass a stringgifier that you like to use <see cref="JsonStringifier{T}"/></param>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ILeveledPusher{T}"/></param>
        public DebugPusher(IStringifier<TType> stringifier, Level level) : base(
            o => { Trace.WriteLine(stringifier.Stringify(o)); }, level)
        {

        }
    }
}