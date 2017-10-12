using System;
using System.IO;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Stringify;

namespace Thalus.Nuntius.Core.Writers
{
    /// <summary>
    /// Implements the <see cref="ILeveledPusher{T}"/> as rolling flat file writer
    /// </summary>
    public class RollingFilePusher<TType> : ILeveledPusher<TType> where  TType: ILeveledEntry
    {
        private int _maxLength;
        private SingleFilePusher<TType> _singleFilePusher;
        private Level _level;

        /// <summary>
        /// Creates an instance of <see cref="RollingFilePusher{TType}"/> with te passed parameters
        /// </summary>
        /// <param name="maxLength">Pass the maximum length of the file in bytes before rolling a new file</param>
        /// <param name="path">Pass the log file path where to put data</param>
        /// <remarks>Please note that all <see cref="Level"/> flags are set to true</remarks>
        /// <remarks>Please note that <see cref="JsonStringifier{T}"/> is used as <see cref="IStringifier{T}"/></remarks>
        /// <remarks>Please note that all<see cref="SingleFilePusher{TType}"/> is used</remarks>
        public RollingFilePusher(int maxLength, string path) : this(maxLength, path, new JsonStringifier<TType>(),
            Level.Debug | Level.Error | Level.Fatal | Level.Info |
            Level.Warning)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="RollingFilePusher{TType}"/> with te passed parameters
        /// </summary>
        /// <param name="maxLength">Pass the maximum length of the file in bytes before rolling a new file</param>
        /// <param name="path">Pass the log file path where to put data</param>
        /// <param name="stringifier">Pass the stirngifier that you like to use <see cref="JsonStringifier{T}"/></param>
        /// <remarks>Please note that all <see cref="Level"/> flags are set to true</remarks>
        /// <remarks>Please note that all<see cref="SingleFilePusher{TType}"/> is used</remarks>
        public RollingFilePusher(int maxLength, string path, IStringifier<TType> stringifier) : this(maxLength, path,
            stringifier,Level.Debug | Level.Error | Level.Fatal | Level.Info |
                                            Level.Warning)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="RollingFilePusher{TType}"/> with te passed parameters
        /// </summary>
        /// <param name="maxLength">Pass the maximum length of the file in bytes before rolling a new file</param>
        /// <param name="path">Pass the log file path where to put data</param>
        /// <param name="stringifier">Pass the stirngifier that you like to use <see cref="JsonStringifier{T}"/></param>
        /// <param name="level">Pass <see cref="Level"/> flags that you like to associate with the <see cref="ILeveledPusher{T}"/></param>
        /// <remarks>Please note that all <see cref="Level"/> flags are set to true</remarks>
        /// <remarks>Please note that all<see cref="SingleFilePusher{TType}"/> is used</remarks>
        public RollingFilePusher(int maxLength, string path, IStringifier<TType> stringifier, Level level) : this(maxLength,
            new SingleFilePusher<TType>(path, stringifier, level), level)
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="RollingFilePusher{TType}"/> with te passed parameters
        /// </summary>
        /// <param name="maxLength">Pass the maximum length of the file in bytes before rolling a new file</param>
        /// <param name="singleFilePusher">Pass the single file writer that you like to use</param>
        /// <param name="level">Pass <see cref="Level"/> flags that you like to associate with the <see cref="ILeveledPusher{T}"/></param>
        /// <remarks>Please note that all <see cref="Level"/> flags are set to true</remarks>
        public RollingFilePusher(int maxLength, SingleFilePusher<TType> singleFilePusher, Level level)
        {
            _maxLength = maxLength;
            _singleFilePusher = singleFilePusher;
            _level = level;
        }

        /// <summary>
        /// Writes a <see cref="IEntry"/> to <see cref="SingleFilePusher{TType}"/>
        /// </summary>
        /// <param name="entries">Pass the to be written <see>
        ///         <cref>TType</cref>
        ///     </see>
        /// </param>
        public void Push(TType entries)
        {
            if (entries == null || _singleFilePusher == null)
            {
                return;
            }

            lock (_singleFilePusher)
            {
                if (!SLevel.IsLog(_level, entries.Level))
                {
                    return;
                }
            }

            FileInfo fi = new FileInfo(_singleFilePusher.FullName);

            if (fi.Exists && fi.Length > _maxLength)
            {
                var pth = Path.Combine(fi.DirectoryName,
                    $"{Guid.NewGuid()}{fi.Extension}");

                fi.CopyTo(pth);
                fi.Delete();
            }

            _singleFilePusher.Push(entries);
        }

        public void SetLevels(Level cats)
        {
            if(_singleFilePusher == null)
            {
                return;
            }

            lock (_singleFilePusher)
            {
                _level = cats;
                _singleFilePusher.SetLevels(cats);
            }
        }
    }
}
