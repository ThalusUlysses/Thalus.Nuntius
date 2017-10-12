using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Logging.Contracts;

namespace Thalus.Nuntius.Core.Logging
{
    class LogBook : ILogBook
    {
        private List<ILeveledPusher<ILeveledEntry>> _writers;
        private string _scope;

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