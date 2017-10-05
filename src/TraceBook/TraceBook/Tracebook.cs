using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TraceBook.Contracts;

namespace TraceBook
{
    class  Tracebook : ITraceBook
    {
        private List<ITraceWriter> _writers;
        private string _scope;

        public Tracebook(List<ITraceWriter> writers, string scope)
        {
            _writers = writers;
            _scope = scope;
        }

        private void Write(string text,object[] obj, Level level, string caller, string filePath, int line )
        {
            ITraceEntry entry = STrace.InternalEntry(level, _scope, text, obj, DateTime.UtcNow,caller, filePath, line);

            Parallel.ForEach(_writers, (i) =>
            {
                try
                {
                    i.Write(entry);
                }
                catch (Exception)
                {
                    // Ok to do so cause no one cares
                }
            });
        }
        public void Errors(string text, object[] obj = null, [CallerMemberName]string caller = null, [CallerFilePath]  string filePath = null, [CallerLineNumber] int line = -1)
        {
            Write(text, obj, Level.Error, caller, filePath, line);
        }

        public void Warning(string text, object[] obj = null, [CallerMemberName] string caller = null, [CallerFilePath]  string filePath = null, [CallerLineNumber] int line = -1)
        {
            Write(text, obj, Level.Warning, caller, filePath, line);
        }

        public void Debug(string text, object[] obj = null, [CallerMemberName] string caller = null, [CallerFilePath]  string filePath = null, [CallerLineNumber] int line = -1)
        {
            Write(text, obj, Level.Debug, caller, filePath, line);
        }

        public void Info(string text, object[] obj = null, [CallerMemberName] string caller = null, [CallerFilePath]  string filePath = null, [CallerLineNumber]  int line = -1)
        {
            Write(text, obj, Level.Info, caller, filePath, line);
        }

        public void Fatal(string text, object[] obj = null, [CallerMemberName]string caller = null, [CallerFilePath] string filePath = null, [CallerLineNumber] int line = -1)
        {
            Write(text, obj, Level.Fatal, caller, filePath, line);
        }
    }
}