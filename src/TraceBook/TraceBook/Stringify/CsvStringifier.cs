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

            if (e.Tags != null)
            {
                b.Append($"{SwoshTags(e.Tags)}");
            }
            else
            {
                b.Append(",");
            }

            if (e.Extra != null)
            {
                b.Append($"{SwoshData(e.Extra)}");
            }
            else
            {
                b.Append(",");
            }
            return b.ToString();
        }

        private static string SwoshData(object d)
        {
            IEnumerable arr;
            if (d is IEnumerable)
            {
                arr = (IEnumerable) d;
            }
            else
            {
                arr = new [] {d};
            }

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