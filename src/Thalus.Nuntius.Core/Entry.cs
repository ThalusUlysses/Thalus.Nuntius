using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Thalus.Nuntius.Core.Contracts;

namespace Thalus.Nuntius.Core
{
    /// <summary>
    /// Implements a serializeable <see cref="IEntry"/> functionality 
    /// using <see cref="ILeveledEntry"/> as base.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Entry : IEntry
    {
        ///<inheritdoc cref="ILeveledEntry.Level"/>
        [DataMember(Name = "level")]
        public Level Level { get; set; }

        ///<inheritdoc cref="IEntry.Tags"/>
        [DataMember(Name = "tags")]
        public IDictionary<string, string> Tags { get; set; }

        ///<inheritdoc cref="IEntry.Extra"/>
        [DataMember(Name = "extra")]
        public object Extra { get; set; }

        ///<inheritdoc cref="IEntry.GetData{TType}"/>
        public TType GetData<TType>(string key)
        {
            string s;
            if (Tags.TryGetValue(key, out s))
            {
                if (typeof(TType) == typeof(int))
                {
                    object obj = int.Parse(s);
                    return (TType) obj;
                }

                if (typeof(TType) == typeof(double))
                {
                    object obj = double.Parse(s);
                    return (TType) obj;
                }

                if (typeof(TType) == typeof(char))
                {
                    object obj = char.Parse(s);
                    return (TType) obj;
                }

                if (typeof(TType) == typeof(byte))
                {
                    object obj = byte.Parse(s);
                    return (TType) obj;
                }

                if (typeof(TType) == typeof(short))
                {
                    object obj = short.Parse(s);
                    return (TType) obj;
                }

                if (typeof(TType) == typeof(long))
                {
                    object obj = long.Parse(s);
                    return (TType) obj;
                }

                if (typeof(TType) == typeof(DateTime))
                {
                    object obj = DateTime.Parse(s);
                    return (TType) obj;
                }

                object o = s;
                return (TType) o;
            }

            return default(TType);

        }
    }
}
