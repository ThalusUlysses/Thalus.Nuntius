using System;
using System.Collections.Generic;
using System.Text;
using TraceBook.Contracts;
using TraceBook.Writers;

namespace TraceBook.Stringify
{
    /// <summary>
    /// implements the <see cref="IStringifier"/> for CSV output
    /// </summary>
    public class CsvStringifier : IStringifier
    {
        string IStringifier.Stringify(object obj)
        {
            ITraceEntry e = (ITraceEntry) obj;
            
            StringBuilder b = new StringBuilder();

            b.Append($"{e.UtcStamp},");
            b.Append($"{e.Level},");
            b.Append($"{e.Scope},");
            b.Append($"{e.Text},");

            if (e.Tags != null)
            {
                b.Append($"{SwoshTags(e.Tags)}");
            }
            else
            {
                b.Append(",");
            }

            if (e.Data != null)
            {
                b.Append($"{SwoshData(e.Data)}");
            }
            else
            {
                b.Append(",");
            }
            return b.ToString();
        }

        private static string SwoshData(Object[] d)
        {
            StringBuilder b = new StringBuilder();

            foreach (var p in d)
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