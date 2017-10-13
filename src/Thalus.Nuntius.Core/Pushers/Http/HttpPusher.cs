using System;
using System.Net.Http;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Stringify;

namespace Thalus.Nuntius.Core.Pushers.Http
{
    public class HttpPusher<TType> : ILeveledPusher<TType> where TType:ILeveledEntry
    {
        private Level _levels;
        private StringifiedContent<TType> _content;
        private HttpClient _client;
        private Uri _endPoint;

        /// <summary>
        /// Creates an instance of <see cref="HttpPusher{TType}"/> initialized with the passed parameters
        /// </summary>
        /// <param name="client">Pass a valid http client</param>
        /// <param name="endPoint">Pass the uri to push the data to</param>
        public HttpPusher(HttpClient client, Uri endPoint) : this(
            Level.Debug | Level.Error | Level.Error | Level.Info | Level.Warning, client, endPoint,
            new StringifiedContent<TType>(default(TType), "application/json", new JsonStringifier<TType>()))
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="HttpPusher{TType}"/> initialized with the passed parameters
        /// </summary>
        /// <param name="client">Pass a valid http client</param>
        /// <param name="endPoint">Pass the uri to push the data to</param>
        /// <param name="contentType">Pass content type such like application/json</param>
        /// 
        public HttpPusher(HttpClient client, Uri endPoint, StringifiedContent<TType> contentType) :this(Level.Debug|Level.Error|Level.Error|Level.Info|Level.Warning,client, endPoint, contentType)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="HttpPusher{TType}"/> initialized with the passed parameters
        /// </summary>
        /// <param name="levels">Pass the log level the pusher is suppose to handle</param>
        /// <param name="client">Pass a valid http client</param>
        /// <param name="endPoint">Pass the uri to push the data to</param>
        /// <param name="contentType">Pass content type such like application/json</param>
        /// 
        public HttpPusher(Level levels, HttpClient client, Uri endPoint, StringifiedContent<TType> contentType)
        {
            _levels = levels;
            _client = client;
            _endPoint = endPoint;
            _content = contentType;
        }

        /// <inheritdoc>
        ///     <cref>ILeveledPusher{TType}.Push</cref>
        /// </inheritdoc>
        public void Push(TType entries)
        {
            lock (_endPoint)
            {
                if (!SLevel.IsLog(_levels, entries.Level))
                {
                    return;
                }
            }
            var content =  _content.Get(entries);
            var result = _client.PostAsync(_endPoint, content).Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }
        }

        ///<inheritdoc cref="ILeveledPusher{TType}.SetLevels"/>
        public void SetLevels(Level cats)
        {
            lock (_endPoint)
            {
                _levels = cats;
            }
        }
    }
}