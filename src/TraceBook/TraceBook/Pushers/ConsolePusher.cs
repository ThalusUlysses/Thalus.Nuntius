using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Pushers;

namespace Thalus.Nuntius.Core.Writers
{
    /// <summary>
    /// Implements <see cref="ITraceWriter"/> functionality as Console writer.
    /// Uses underlying <see cref="UnspecifiedPusher{TType}"/>
    /// </summary>
    public class ConsolePusher<TType> : UnspecifiedPusher<TType> where TType: IEntry
    {
        /// <summary>
        /// Creates an instance of <see cref="ConsolePusher{TType}"/>with defaults
        /// </summary>
        public ConsolePusher() : base(Handle, Level.Debug | Level.Error | Level.Fatal | Level.Info |
                                                  Level.Warning)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="ConsolePusher{TType}"/> with teh passed parameters
        /// </summary>
        /// <param name="level">Pass trace level flags that are associated with the <see cref="ITraceWriter"/></param>
        public ConsolePusher(Level level) : base(Handle, level)
        {
        }

        private static void Handle(TType e)
        {
            if (SLevel.IsLogErrors(e.Level) || SLevel.IsLogFatal(e.Level))
            {
                WriteLine(e, ConsoleColor.DarkRed);
            }
            else if (SLevel.IsLogWarning(e.Level))
            {
                WriteLine(e, ConsoleColor.DarkYellow);
            }
            else
            {
                WriteLine(e, Console.ForegroundColor);
            }
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


        private static string SwoshData(object d)
        {
            IEnumerable arr;
            if (!(d is IEnumerable))
            {
                arr = new[] {d};
            }
            else
            {
                arr = (IEnumerable) d;
            }

            StringBuilder b = new StringBuilder();

            foreach (object p in arr)
            {
                if (b.Length > 0)
                {
                    b.Append(",");
                }

                b.Append(p);
            }

            return b.ToString();
        }


        private static void WriteLine(TType text, ConsoleColor c)
        {
            StringBuilder  b = new StringBuilder();
            
            b.Append($"[{text.Level}] {text.Tags["scope"]} {text.Tags["text"]} {text.Tags["utc-stamp"]}");

            if (text.Tags != null)
            {
                b.Append($" {SwoshTags(text.Tags)}");
            }
            if (text.Extra != null)
            {
                b.Append($" {SwoshData(text.Extra)}");
            }

            ConsoleColor colorRestore = Console.ForegroundColor;

            Console.ForegroundColor = c;

            Console.WriteLine(b);

            Console.ForegroundColor = colorRestore;
        }
    }
}