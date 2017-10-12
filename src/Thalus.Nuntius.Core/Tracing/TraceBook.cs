using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Tracing.Contracts;

namespace Thalus.Nuntius.Core.Tracing
{
    class  TraceBook : ITraceBook
    {
        private readonly List<ILeveledPusher<ILeveledEntry>> _writers;
        private readonly string _scope;

        public TraceBook(List<ILeveledPusher<ILeveledEntry>> writers, string scope)
        {
            _writers = writers;
            _scope = scope;
        }

        private void Write(string text,object[] obj, Level level, string caller, string filePath, int line )
        {
            IEntry entry = STraceBook.InternalEntry(level, _scope, text, obj, DateTime.UtcNow,caller, filePath, line);

            Parallel.ForEach(_writers, (i) =>
            {
                try
                {
                    i.Push(entry);
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