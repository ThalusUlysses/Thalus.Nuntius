using System;
using System.Collections;
using System.Collections.Generic;
using SharpRaven;
using SharpRaven.Data;
using Thalus.Nuntius.Contracts;

namespace Thalus.Nuntius.Externals.Pushers.Sentry
{
    public class SentryPusher<TType> : ILeveledPusher<TType> where TType: IEntry
    {
        private Level _level;

        private readonly IRavenClient _client;

        public SentryPusher(string dsn, Level level) : this(new RavenClient(dsn), level)
        {

        }

        public SentryPusher(IRavenClient client, Level level)
        {
            _client = client;
            _level = level;
        }


        public void Push(TType entries)
        {
            if (entries == null || _client == null)
            {
                return;
            }
            
            lock (_client)
            {
                if (!SLevel.IsLog(_level, entries.Level))
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

        private SentryEvent GetEventByType(TType entries)
        {
            var t = entries.Extra as Exception;

            if (t == null)
            {
                IEnumerable en = entries.Extra as IEnumerable;
                if (en !=null)
                {
                    foreach (object o in en)
                    {
                        t = o as Exception;
                        if (t != null)
                        {
                            break;
                        }
                    }
                }
            }

            var msg = new SentryMessage(entries.Tags["text"]);
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

            ev.Extra = entries.Extra;

            return ev;
        }

        private void SetTags(SentryEvent ev, IEntry entries)
        {
            ev.Tags["scope"] = entries.Tags["scope"];
            ev.Tags["utc-stamp"] = entries.Tags["utc-stamp"];

            foreach (KeyValuePair<string, string> pair in entries.Tags)
            {
                ev.Tags[pair.Key] = pair.Value;
            }
        }

        private void SetErrolLevel(SentryEvent ev, ILeveledEntry entries)
        {
            if (SLevel.IsLogDebug(entries.Level))
            {
                ev.Level = ErrorLevel.Debug;
            }

            if (SLevel.IsLogInfo(entries.Level))
            {
                ev.Level = ErrorLevel.Info;
            }

            if (SLevel.IsLogWarning(entries.Level))
            {
                ev.Level = ErrorLevel.Warning;
            }

            if (SLevel.IsLogErrors(entries.Level))
            {
                ev.Level = ErrorLevel.Error;
            }

            if (SLevel.IsLogFatal(entries.Level))
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