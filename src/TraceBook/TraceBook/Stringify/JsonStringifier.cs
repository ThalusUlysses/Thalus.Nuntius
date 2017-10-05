using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using TraceBook.Contracts;
using TraceBook.Writers;

namespace TraceBook.Stringify
{
    /// <summary>
    /// Implements the <see cref="IStringifier"/> functionality for JSON 
    /// output using <see cref="DataContractJsonSerializer"/> fo <see cref="TraceEntry"/>
    /// </summary>
    public class JsonStringifier : IStringifier
    {
        string IStringifier.Stringify(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
