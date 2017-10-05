using System;
using System.IO;
using System.Threading.Tasks;
using TraceBook.Contracts;
using TraceBook.Stringify;

namespace TraceBook.Writers
{
    /// <summary>
    /// Implements the <see cref="ITraceWriter"/> functionality as single file
    /// writer
    /// </summary>
    public class SingleFileWriter : ITraceWriter
    {
        private IStringifier _stringifier;
        private Level _level;
        
        public string FullName { get; }

        /// <summary>
        /// Creates an instance of <see cref="SingleFileWriter"/> initialized with
        /// the passed parameters
        /// </summary>
        /// <param name="file">Pass the file name to write items to</param>
        /// <remarks>Please note <see cref="JsonStringifier"/> is used as <see cref="IStringifier"/></remarks>
        public SingleFileWriter(string file ) : this(new FileInfo(file), new JsonStringifier(),
            Level.Debug | Level.Error | Level.Fatal | Level.Info |
            Level.Warning)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="SingleFileWriter"/> initialized with
        /// the passed parameters
        /// </summary>
        /// <param name="file">Pass the file name to write items to</param>
        /// <param name="stringifier">Pass the stringifie rto be used <see cref="JsonStringifier"/></param>
        public SingleFileWriter(string file, IStringifier stringifier) : this(new FileInfo(file), stringifier,
            Level.Debug | Level.Error | Level.Fatal | Level.Info |
            Level.Warning)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="SingleFileWriter"/> initialized with
        /// the passed parameters
        /// </summary>
        /// <param name="file">Pass the file name to write items to</param>
        /// <param name="stringifier">Pass the stringifie rto be used <see cref="JsonStringifier"/></param>
        /// <param name="level">Pass teh <see cref="Level"/> flags associated with the <see cref="ITraceWriter"/></param>
        public SingleFileWriter(string file, IStringifier stringifier, Level level) : this(new FileInfo(file), stringifier, level)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="SingleFileWriter"/> initialized with
        /// the passed parameters
        /// </summary>
        /// <param name="file">Pass the file info to write items to</param>
        public SingleFileWriter(FileInfo file) : this(file, new JsonStringifier(),
            Level.Debug | Level.Error | Level.Fatal | Level.Info |
            Level.Warning)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="SingleFileWriter"/> initialized with
        /// the passed parameters
        /// </summary>
        /// <param name="file">Pass the file name to write items to</param>
        /// <param name="stringifier">Pass the stringifie rto be used <see cref="JsonStringifier"/></param>
        public SingleFileWriter(FileInfo file, IStringifier stringifier) : this(file, stringifier,
            Level.Debug | Level.Error | Level.Fatal | Level.Info |
            Level.Warning)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="SingleFileWriter"/> initialized with
        /// the passed parameters
        /// </summary>
        /// <param name="file">Pass the file name to write items to</param>
        /// <param name="stringifier">Pass the stringifie rto be used <see cref="JsonStringifier"/></param>
        /// <param name="level">Pass the <see cref="Level"/> flags associated with the <see cref="ITraceWriter"/></param>
        public SingleFileWriter(FileInfo file, IStringifier stringifier, Level level)
        {
            FullName = file.FullName;
            _stringifier = stringifier;
            _level = level;
        }

        /// <summary>
        /// Writes a <see cref="ITraceEntry"/> to the underlying target
        /// </summary>
        /// <param name="entry">Pass the the <see cref="ITraceEntry"/> to write</param>
        public void Write(ITraceEntry entry)

        {
            lock (FullName)
            {
                if (!STrace.IsLog(_level, entry.Level))
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

        private void InternalWrite(ITraceEntry entry)
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