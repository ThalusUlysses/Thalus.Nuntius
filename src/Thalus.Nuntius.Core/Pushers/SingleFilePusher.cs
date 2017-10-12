using System;
using System.IO;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Stringify;

namespace Thalus.Nuntius.Core.Pushers
{
    /// <summary>
    /// Implements the <see cref="ILeveledPusher{T}"/> functionality as single file
    /// writer
    /// </summary>
    public class SingleFilePusher<TType> : ILeveledPusher<TType> where TType: ILeveledEntry
    {
        private readonly IStringifier<TType> _stringifier;
        private Level _level;
        
        /// <summary>
        /// Gets the <see cref="FullName"/> if the <see cref="SingleFilePusher{TType}"/>
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// Creates an instance of <see cref="SingleFilePusher{TType}"/> initialized with
        /// the passed parameters
        /// </summary>
        /// <param name="file">Pass the file name to write items to</param>
        /// <remarks>Please note <see cref="JsonStringifier{T}"/> is used as <see cref="IStringifier{T}"/></remarks>
        public SingleFilePusher(string file ) : this(new FileInfo(file), new JsonStringifier<TType>(),
            Level.Debug | Level.Error | Level.Fatal | Level.Info |
            Level.Warning)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="SingleFilePusher{TType}"/> initialized with
        /// the passed parameters
        /// </summary>
        /// <param name="file">Pass the file name to write items to</param>
        /// <param name="stringifier">Pass the stringifie rto be used <see cref="JsonStringifier{T}"/></param>
        public SingleFilePusher(string file, IStringifier<TType> stringifier) : this(new FileInfo(file), stringifier,
            Level.Debug | Level.Error | Level.Fatal | Level.Info |
            Level.Warning)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="SingleFilePusher{TType}"/> initialized with
        /// the passed parameters
        /// </summary>
        /// <param name="file">Pass the file name to write items to</param>
        /// <param name="stringifier">Pass the stringifie rto be used <see cref="JsonStringifier{T}"/></param>
        /// <param name="level">Pass teh <see cref="Level"/> flags associated with the <see cref="ILeveledPusher{T}"/></param>
        public SingleFilePusher(string file, IStringifier<TType> stringifier, Level level) : this(new FileInfo(file), stringifier, level)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="SingleFilePusher{TType}"/> initialized with
        /// the passed parameters
        /// </summary>
        /// <param name="file">Pass the file info to write items to</param>
        public SingleFilePusher(FileInfo file) : this(file, new JsonStringifier<TType>(),
            Level.Debug | Level.Error | Level.Fatal | Level.Info |
            Level.Warning)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="SingleFilePusher{TType}"/> initialized with
        /// the passed parameters
        /// </summary>
        /// <param name="file">Pass the file name to write items to</param>
        /// <param name="stringifier">Pass the stringifie rto be used <see cref="JsonStringifier{T}"/></param>
        public SingleFilePusher(FileInfo file, IStringifier<TType> stringifier) : this(file, stringifier,
            Level.Debug | Level.Error | Level.Fatal | Level.Info |
            Level.Warning)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="SingleFilePusher{TType}"/> initialized with
        /// the passed parameters
        /// </summary>
        /// <param name="file">Pass the file name to write items to</param>
        /// <param name="stringifier">Pass the stringifie rto be used <see cref="JsonStringifier{T}"/></param>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ILeveledPusher{T}"/></param>
        public SingleFilePusher(FileInfo file, IStringifier<TType> stringifier, Level level)
        {
            FullName = file.FullName;
            _stringifier = stringifier;
            _level = level;
        }

        /// <summary>
        /// Writes a <see cref="IEntry"/> to the underlying target
        /// </summary>
        /// <param name="entry">Pass the the <see cref="IEntry"/> to write</param>
        public void Push(TType entry)

        {
            lock (FullName)
            {
                if (!SLevel.IsLog(_level, entry.Level))
                {
                    return;
                }
            }

            InternalWrite(entry);

            //Task.Run(() => { InternalWrite(entry); });
        }

        /// <summary>
        /// Sets the <see cref="Level"/> flags
        /// </summary>
        /// <param name="cats">Pass the trace level flags</param>
        public void SetLevels(Level cats)
        {
            lock (FullName)
            {
                _level = cats;
            }
        }

        private void InternalWrite(TType entry)
        {
            lock (FullName)
            {
                try
                {
                    if (!File.Exists(FullName))
                    {
                        FileInfo fi = new FileInfo(FullName);

                        if (!fi.Directory.Exists)
                        {
                            fi.Directory.Create();
                        }

                        fi.Create().Close();
                    }

                    string st = _stringifier.Stringify(entry);
                    File.AppendAllLines(FullName, new[] {st});
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}