using System.Diagnostics;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Stringify;
using Thalus.Nuntius.Core.Pushers;

namespace Thalus.Nuntius.Core.Writers
{
    /// <summary>
    /// Implements the <see cref="ILeveledPusher{T}"/> functionality as Trace writer
    /// using <see cref="UnspecifiedPusher{TType}"/>
    /// </summary>
    public class TracePusher<TType> : UnspecifiedPusher<TType> where TType : ILeveledEntry
    {

        /// <summary>
        /// Creates an instance of <see cref="TracePusher{TType}"/> initialized with the
        /// passed parameters
        /// </summary>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ILeveledPusher{T}"/></param>
        /// <remarks>Please note that the <see cref="JsonStringifier{T}"/> is used</remarks>
        public TracePusher(Level level) : this(new JsonStringifier<TType>(), level)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="TracePusher{TType}"/> initialized with the
        /// passed parameters
        /// </summary>
        /// <param name="stringifier">Pass the <see cref="IStringifier{T}"/> to be used</param>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ILeveledPusher{T}"/></param>
        public TracePusher(IStringifier<TType> stringifier, Level level) : base(i =>
        {
            Trace.WriteLine(stringifier.Stringify(i));
        }, level)
        {
        }
    }
}