using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Thalus.Nuntius.Contracts;
using Thalus.Nuntius.Tracing;

namespace Thalus.Nuntius.Stringify
{
    /// <summary>
    /// Implements the <see cref="IStringifier{T}"/> functionality for JSON 
    /// output using <see cref="DataContractJsonSerializer"/> fo <see cref="TraceEntryFacade"/>
    /// </summary>
    public class JsonStringifier<TType> : IStringifier<TType>
    {
        string IStringifier<TType>.Stringify(TType obj)
        {
            if (obj == null)
            {
                return null;
            }

            return JsonConvert.SerializeObject(obj);
        }
    }
}
