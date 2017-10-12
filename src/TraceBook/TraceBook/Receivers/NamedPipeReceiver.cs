using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Stringify;

namespace Thalus.Nuntius.Core.Receivers
{
    /// <summary>
    /// Implements the <see cref="NamedPipeReceiver{T}"/> and the <see cref="IReceiver{T}"/>
    /// functionality.
    /// </summary>
    /// <typeparam name="TType">Pass the type of data to act on</typeparam>
    public class NamedPipeReceiver<TType> where TType: ILeveledEntry, IReceiver<TType>
    {
        private NamedPipeServerStream _stm;
        private IObjectifier<TType> _strignifier;
        private CancellationTokenSource _cancellationToken;
        private Thread _readThread;

        /// <summary>
        /// Creates an instance of <see cref="NamedPipeReceiver{T}"/> initialized with the
        /// passed parameters
        /// </summary>
        /// <param name="id">Pass the name of the pipe to write to</param>
        public NamedPipeReceiver(string id) : this(new JsonObjectifier<TType, Entry>(), new NamedPipeServerStream(id, PipeDirection.In))
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedPipeReceiver{T}"/> initialized with the
        /// passed parameters
        /// </summary>
        /// <param name="id">Pass the name of the pipe to write to</param>
        /// <param name="strignifier">Pass the string to object converter</param>
        public NamedPipeReceiver(string id,IObjectifier<TType> strignifier) : this(strignifier,new NamedPipeServerStream(id,PipeDirection.In))
        {
        }

        private NamedPipeReceiver(IObjectifier<TType> strignifier, NamedPipeServerStream stm)
        {
            _strignifier = strignifier;
            _stm = stm;
        }

        /// <summary>
        ///  Closes the named pipe and stops waiting for data
        /// </summary>
        public void Close()
        {
            if (_readThread!=null && _readThread.IsAlive)
            {
                _cancellationToken.Cancel();

                if (_readThread.IsAlive)
                {
                    try
                    {
                        _readThread.Abort();
                    }
                    catch (Exception)
                    {
                        // ok cause no one cares
                    }
                }
                _readThread = null;
                _cancellationToken = null;
            }
        }

        /// <summary>
        /// Connects the pipe to stream and awaits data
        /// </summary>
        public void Connect()
        {
            Close();
            _readThread = new Thread(ReadThread);
            _cancellationToken   = new CancellationTokenSource();

            _readThread.Start();
        }

        private void ReadThread()
        {
            using (StreamReader reader = new StreamReader(_stm))
            {
                while (!_cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        if (!_stm.IsConnected)
                        {
                            _stm.WaitForConnectionAsync(_cancellationToken.Token);

                            if (_cancellationToken.IsCancellationRequested)
                            {
                                continue;
                            }
                        }

                        var item = reader.ReadLine();
                        if (item == null)
                        {
                            continue;
                        }
                        var o = _strignifier.Objectify(item);
                        EntryReceivedEvent?.Invoke(o);
                    }
                    catch (Exception)
                    {
                      
                    }
                }
            }
        }
        /// <summary>
        /// Raised when data of type <see cref="TType"/> has been received
        /// </summary>
        public event Action<TType> EntryReceivedEvent;
    }
}