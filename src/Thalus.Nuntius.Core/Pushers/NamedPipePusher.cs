using System.IO;
using System.IO.Pipes;
using System.Threading;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Stringify;

namespace Thalus.Nuntius.Core.Pushers
{
    /// <summary>
    ///Implements the <see cref="NamedPipePusher{T}"/> functionality such like <see cref="Push"/>
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class NamedPipePusher<TType> : ILeveledPusher<TType> where TType : ILeveledEntry
    {
        private readonly NamedPipeClientStream _stm;
        private readonly StreamWriter _writer;
        private readonly IStringifier<TType> _stringifier;
        private CancellationTokenSource _cancellationToken;
        private Level _level;

        /// <summary>
        /// Creates an isntance of <see cref="NamedPipePusher{T}"/> initialized with the passed parameters
        /// </summary>
        /// <param name="id">Pass the id of the pipe to push to</param>
        public NamedPipePusher(string id) : this(id, new JsonStringifier<TType>(), Level.Debug | Level.Error | Level.Fatal | Level.Info |
                                                                          Level.Warning)
        {
        }

        /// <summary>
        /// Creates an isntance of <see cref="NamedPipePusher{T}"/> initialized with the passed parameters
        /// </summary>
        /// <param name="id">Pass the id of the pipe to push to</param>
        /// <param name="level">Pass the levels to act on <see cref="Level.Error"/>,...</param>
        public NamedPipePusher(string id, Level level) : this(id,new JsonStringifier<TType>(),level)
        {
        }

        /// <summary>
        /// Creates an isntance of <see cref="NamedPipePusher{T}"/> initialized with the passed parameters
        /// </summary>
        /// <param name="id">Pass the id of the pipe to push to</param>
        /// <param name="stringifier">Pass stringifier / object to string converter</param>
        /// <param name="level">Pass the levels to act on <see cref="Level.Error"/>,...</param>
        public NamedPipePusher(string id, IStringifier<TType> stringifier, Level level) :this(stringifier,level, new NamedPipeClientStream(".", id, PipeDirection.Out, PipeOptions.WriteThrough))
        {
        }

        private NamedPipePusher(IStringifier<TType> stringifier, Level level, NamedPipeClientStream stm )
        {
            _stringifier = stringifier;
            _stm = stm;
            _writer = new StreamWriter(_stm);
            _level = level;
        }

        /// <inheritdoc />
        public void Push(TType entries)
        {
            if (!SLevel.IsLog(_level, entries.Level))
            {
                return;
            }

            if (!_stm.IsConnected)
            {
                _cancellationToken?.Cancel();

                _cancellationToken = new CancellationTokenSource();
                _stm.ConnectAsync(_cancellationToken.Token);
            }

            var st = _stringifier.Stringify(entries);
            _writer.WriteLine(st);
            _writer.Flush();
        }

        /// <inheritdoc />
        public void SetLevels(Level cats)
        {
            lock (_stm)
            {
                _level = cats;
            }
        }

        /// <summary>
        /// Disposes the passed stream
        /// </summary>
        public void Dispose()
        {
            _cancellationToken.Cancel();

            _writer?.Close();
            _writer?.Dispose();
            _stm?.Close();
            _stm?.Dispose();
        }
    }
}
