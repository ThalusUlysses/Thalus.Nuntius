using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Thalus.Nuntius.Contracts;
using Thalus.Nuntius.Tracing;

namespace Thalus.Nuntius.Stringify
{
    /// <summary>
    /// Implements the <see cref="IStringifier{TType}"/> functionality for JSON 
    /// output using <see cref="DataContractJsonSerializer"/> fo <see cref="TraceEntryFacade"/>
    /// </summary>
    public class JsonObjectifier<TType, TUnderlyingType> : IObjectifier<TType> where TUnderlyingType : class
    {
        TType IObjectifier<TType>.Objectify(string obj)
        {
            object item = JsonConvert.DeserializeObject<TUnderlyingType>(obj);
            return (TType) item;
        }
    }
}