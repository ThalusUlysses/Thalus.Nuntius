using System.Collections;
using System.Collections.Generic;
using System.Text;
using Thalus.Nuntius.Core.Contracts;

namespace Thalus.Nuntius.Core.Stringify
{
    /// <summary>
    /// implements the <see cref="IStringifier{T}"/> for CSV output
    /// </summary>
    public class CsvStringifier : IStringifier<IEntry>
    {
        string IStringifier<IEntry>.Stringify(IEntry e)
        {
            StringBuilder b = new StringBuilder();

            b.Append($"{e.Tags["utc-stamp"]},");
            b.Append($"{e.Level},");
            b.Append($"{e.Tags["scope"]},");
            b.Append($"{e.Tags["text"]},");

            b.Append(e.Tags != null ? $"{SwoshTags(e.Tags)}" : ",");
            b.Append(e.Extra != null ? $"{SwoshData(e.Extra)}" : ",");

            return b.ToString();
        }

        private static string SwoshData(object d)
        {
            var enumerable = d as IEnumerable;

            var arr = enumerable ?? new [] {d};

            StringBuilder b = new StringBuilder();

            foreach (var p in arr)
            {
                if (b.Length > 0)
                {
                    b.Append(",");
                }
                b.Append(p);
            }

            return b.ToString();
        }

        private static string SwoshTags(IDictionary<string, string> d)
        {
            StringBuilder b = new StringBuilder();

            foreach (KeyValuePair<string, string> pair in d)
            {
                if (b.Length > 0)
                {
                    b.Append(",");
                }
                b.Append($"\"{pair.Key}\":\"{pair.Value}\"");
            }

            return b.ToString();
        }
    }
}