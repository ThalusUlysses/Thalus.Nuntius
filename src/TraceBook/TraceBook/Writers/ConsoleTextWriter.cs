using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TraceBook.Contracts;

namespace TraceBook.Writers
{
    /// <summary>
    /// Implements <see cref="ITraceWriter"/> functionality as Console writer.
    /// Uses underlying <see cref="UnspecifiedTextWriter"/>
    /// </summary>
    public class ConsoleTextWriter : UnspecifiedTextWriter
    {
        /// <summary>
        /// Creates an instance of <see cref="ConsoleTextWriter"/>with defaults
        /// </summary>
        public ConsoleTextWriter() : base(Handle, Level.Debug | Level.Error | Level.Fatal | Level.Info |
                                                  Level.Warning)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="ConsoleTextWriter"/> with teh passed parameters
        /// </summary>
        /// <param name="level">Pass trace level flags that are associated with the <see cref="ITraceWriter"/></param>
        public ConsoleTextWriter(Level level) : base(Handle, level)
        {
        }

        private static void Handle(ITraceEntry e)
        {
            if (STrace.IsLogErrors(e.Level) || STrace.IsLogFatal(e.Level))
            {
                WriteLine(e, ConsoleColor.DarkRed);
            }
            else if (STrace.IsLogWarning(e.Level))
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


        private static string SwoshData(object[] d)
        {
            StringBuilder b = new StringBuilder();

            foreach (object p in d)
            {
                if (b.Length > 0)
                {
                    b.Append(",");
                }

                b.Append(p);
            }

            return b.ToString();
        }


        private static void WriteLine(ITraceEntry text, ConsoleColor c)
        {
            StringBuilder  b = new StringBuilder();

            b.Append($"[{text.Level}] {text.Scope} {text.Text} {text.UtcStamp}");

            if (text.Tags != null)
            {
                b.Append($" {SwoshTags(text.Tags)}");
            }
            if (text.Data != null)
            {
                b.Append($" {SwoshData(text.Data)}");
            }

            ConsoleColor colorRestore = Console.ForegroundColor;

            Console.ForegroundColor = c;

            Console.WriteLine(b);

            Console.ForegroundColor = colorRestore;
        }
    }
}