using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TraceBook.Contracts;
using TraceBook.Writers;

namespace TraceBook.Stringify
{
    /// <summary>
    /// implements the <see cref="IStringifier"/> functionality for XML
    /// using the <see cref="DataContractSerializer"/> for <see cref="TraceEntry"/>
    /// </summary>
    public class XmlStringifier : IStringifier
    {
        string IStringifier.Stringify(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var xml =  JsonConvert.DeserializeXmlNode(json, "trace");

            return xml.InnerXml;
        }
    }
}