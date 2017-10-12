using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Tracing;

namespace Thalus.Nuntius.Core.Stringify
{
    /// <summary>
    /// Implements the <see cref="IStringifier{T}"/> functionality for JSON 
    /// output using <see cref="DataContractJsonSerializer"/> fo <see cref="TraceEntryFacade"/>
    /// </summary>
    public class JsonStringifier<TType> : IStringifier<TType>
    {
        string IStringifier<TType>.Stringify(TType obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
