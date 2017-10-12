using System.Diagnostics;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Stringify;

namespace Thalus.Nuntius.Core.Pushers
{
    /// <summary>
    /// Implements a <see cref="ILeveledPusher"/> functionality as Debug trace writer
    /// using unerdlying <see cref="UnspecifiedPusher{TType}"/>
    /// </summary>
    public class DebugPusher<TType> : UnspecifiedPusher<TType>  where TType: ILeveledEntry
    {
        /// <summary>
        /// Creates an instance of <see cref="DebugPusher{TType}"/> initalized with the
        /// passed parameters
        /// </summary>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ILeveledPusher"/></param>
        /// <remarks>Please note <see cref="JsonStringifier"/> is used</remarks>
        public DebugPusher(Level level) : this(new JsonStringifier<TType>(),level)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="DebugPusher{TType}"/> initialized with the
        /// passed parameters
        /// </summary>
        /// <param name="stringifier">Pass a stringgifier that you like to use <see cref="JsonStringifier"/></param>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ILeveledPusher"/></param>
        public DebugPusher(IStringifier<TType> stringifier, Level level) : base(
            o => { Trace.WriteLine(stringifier.Stringify(o)); }, level)
        {

        }
    }
}