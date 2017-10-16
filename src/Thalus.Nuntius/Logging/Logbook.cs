using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thalus.Nuntius.Contracts;
using Thalus.Nuntius.Logging.Contracts;

namespace Thalus.Nuntius.Logging
{
    class LogBook : ILogBook
    {
        private readonly List<ILeveledPusher<ILeveledEntry>> _writers;
        private readonly string _scope;

        public LogBook(List<ILeveledPusher<ILeveledEntry>> writers, string scope)
        {
            _writers = writers;
            _scope = scope;
        }

        public void Errors(string invariantText,string text=null, object[] obj = null)
        {
            Write(Level.Error, invariantText, text, obj);
        }

        public void Warning(string invariantText, string text = null, object[] obj = null)
        {
            Write(Level.Warning, invariantText, text, obj);
        }

        public void Debug(string invariantText, string text = null, object[] obj = null)
        {
            Write(Level.Debug, invariantText, text, obj);
        }

        public void Info(string invariantText, string text = null, object[] obj = null)
        {
            Write(Level.Info, invariantText, text, obj);
        }

        public void Fatal(string invariantText, string text = null, object[] obj = null)
        {
            Write(Level.Fatal, invariantText, text, obj);
        }


        private void Write(Level level, string invariantText, string text = null, object[] obj = null)
        {
            IEntry entry = SLogBook.InternalEntry(level, _scope, invariantText, text, obj, DateTime.UtcNow);

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
    }
}