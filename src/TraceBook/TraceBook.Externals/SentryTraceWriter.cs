using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SharpRaven;
using SharpRaven.Data;
using TraceBook.Contracts;

namespace TraceBook.Externals
{
    public class SentryTraceWriter : ITraceWriter
    {
        private Level _level;

        private IRavenClient _client;

        public SentryTraceWriter(string dsn, Level level) : this(new RavenClient(dsn), level)
        {

        }

        public SentryTraceWriter(IRavenClient client, Level level)
        {
            _client = client;
            _level = level;
        }

        public void Write(ITraceEntry entries)
        {
            if (entries == null || _client == null)
            {
                return;
            }
            
            lock (_client)
            {
                if (!STrace.IsLog(_level, entries.Level))
                {
                    return;
                }
            }

            var ev = GetEventByType(entries);
            
            lock (_client)
            {
                _client.Capture(ev);
            }
        }

        private SentryEvent GetEventByType(ITraceEntry entries)
        {
            var t = entries.Data.FirstOrDefault(i => i is Exception) as Exception;

            var msg = new SentryMessage(entries.Text);
            SentryEvent ev;
            if (t != null)
            {
                ev =  new SentryEvent(t)
                {
                    Message = msg
                };
            }
            else
            {
                ev = new SentryEvent(msg);
            }

            SetTags(ev, entries);
            SetErrolLevel(ev, entries);

            ev.Extra = entries.Data;

            return ev;
        }

        private void SetTags(SentryEvent ev, ITraceEntry entries)
        {
            ev.Tags["scope"] = entries.Scope;
            ev.Tags["utc-stamp"] = entries.UtcStamp.ToString(CultureInfo.CurrentUICulture);

            foreach (KeyValuePair<string, string> pair in entries.Tags)
            {
                ev.Tags[pair.Key] = pair.Value;
            }
        }

        private void SetErrolLevel(SentryEvent ev, ITraceEntry entries)
        {
            if (STrace.IsLogDebug(entries.Level))
            {
                ev.Level = ErrorLevel.Debug;
            }

            if (STrace.IsLogInfo(entries.Level))
            {
                ev.Level = ErrorLevel.Info;
            }

            if (STrace.IsLogWarning(entries.Level))
            {
                ev.Level = ErrorLevel.Warning;
            }

            if (STrace.IsLogErrors(entries.Level))
            {
                ev.Level = ErrorLevel.Error;
            }

            if (STrace.IsLogFatal(entries.Level))
            {
                ev.Level = ErrorLevel.Fatal;
            }
        }

        public void SetLevels(Level cats)
        {
            lock (_client)
            {
                _level = cats;
            }
        }
    }
}