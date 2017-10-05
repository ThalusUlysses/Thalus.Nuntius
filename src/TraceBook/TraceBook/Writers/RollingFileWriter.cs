using System;
using System.IO;
using TraceBook.Contracts;
using TraceBook.Stringify;

namespace TraceBook.Writers
{
    /// <summary>
    /// Implements the <see cref="ITraceWriter"/> as rolling flat file writer
    /// </summary>
    public class RollingFileWriter : ITraceWriter
    {
        private int _maxLength;
        private SingleFileWriter _singleFileWriter;
        private Level _level;

        /// <summary>
        /// Creates an instance of <see cref="RollingFileWriter"/> with te passed parameters
        /// </summary>
        /// <param name="maxLength">Pass the maximum length of the file in bytes before rolling a new file</param>
        /// <param name="path">Pass the log file path where to put data</param>
        /// <remarks>Please note that all <see cref="Level"/> flags are set to true</remarks>
        /// <remarks>Please note that <see cref="JsonStringifier"/> is used as <see cref="IStringifier"/></remarks>
        /// <remarks>Please note that all<see cref="SingleFileWriter"/> is used</remarks>
        public RollingFileWriter(int maxLength, string path) : this(maxLength, path, new JsonStringifier(),
            Level.Debug | Level.Error | Level.Fatal | Level.Info |
            Level.Warning)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="RollingFileWriter"/> with te passed parameters
        /// </summary>
        /// <param name="maxLength">Pass the maximum length of the file in bytes before rolling a new file</param>
        /// <param name="path">Pass the log file path where to put data</param>
        /// <param name="stringifier">Pass the stirngifier that you like to use <see cref="JsonStringifier"/></param>
        /// <remarks>Please note that all <see cref="Level"/> flags are set to true</remarks>
        /// <remarks>Please note that all<see cref="SingleFileWriter"/> is used</remarks>
        public RollingFileWriter(int maxLength, string path, IStringifier stringifier) : this(maxLength, path,
            stringifier,Level.Debug | Level.Error | Level.Fatal | Level.Info |
                                            Level.Warning)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="RollingFileWriter"/> with te passed parameters
        /// </summary>
        /// <param name="maxLength">Pass the maximum length of the file in bytes before rolling a new file</param>
        /// <param name="path">Pass the log file path where to put data</param>
        /// <param name="stringifier">Pass the stirngifier that you like to use <see cref="JsonStringifier"/></param>
        /// <param name="level">Pass <see cref="Level"/> flags that you like to associate with the <see cref="ITraceWriter"/></param>
        /// <remarks>Please note that all <see cref="Level"/> flags are set to true</remarks>
        /// <remarks>Please note that all<see cref="SingleFileWriter"/> is used</remarks>
        public RollingFileWriter(int maxLength, string path, IStringifier stringifier, Level level) : this(maxLength,
            new SingleFileWriter(path, stringifier, level), level)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="RollingFileWriter"/> with te passed parameters
        /// </summary>
        /// <param name="maxLength">Pass the maximum length of the file in bytes before rolling a new file</param>
        /// <param name="singleFileWriter">Pass the single file writer that you like to use</param>
        /// <param name="level">Pass <see cref="Level"/> flags that you like to associate with the <see cref="ITraceWriter"/></param>
        /// <remarks>Please note that all <see cref="Level"/> flags are set to true</remarks>
        public RollingFileWriter(int maxLength, SingleFileWriter singleFileWriter, Level level)
        {
            _maxLength = maxLength;
            _singleFileWriter = singleFileWriter;
            _level = level;
        }

        /// <summary>
        /// Writes a <see cref="ITraceEntry"/> to <see cref="SingleFileWriter"/>
        /// </summary>
        /// <param name="entries">Pass the to be written <see cref="ITraceEntry"/></param>
        public void Write(ITraceEntry entries)
        {
            if (entries == null || _singleFileWriter == null)
            {
                return;
            }

            lock (_singleFileWriter)
            {
                if (!STrace.IsLog(_level, entries.Level))
                {
                    return;
                }
            }

            FileInfo fi = new FileInfo(_singleFileWriter.FullName);

            if (fi.Exists && fi.Length > _maxLength)
            {
                var pth = Path.Combine(fi.DirectoryName,
                    $"{Guid.NewGuid()}{fi.Extension}");

                fi.CopyTo(pth);
                fi.Delete();
            }

            _singleFileWriter.Write(entries);
        }

        public void SetLevels(Level cats)
        {
            if(_singleFileWriter == null)
            {
                return;
            }

            lock (_singleFileWriter)
            {
                _level = cats;
                _singleFileWriter.SetLevels(cats);
            }
        }
    }
}
