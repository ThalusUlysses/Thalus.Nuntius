using System.Net.Http;
using System.Text;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Stringify;

namespace Thalus.Nuntius.Core.Pushers.Http
{
    /// <summary>
    /// Implements the <see cref="StringContent"/> functionality for objcet to string conversion
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class StringifiedContent<TType> : StringContent
    {
        private IStringifier<TType> _stringifier;
        private Encoding _encoding;

        /// <summary>
        /// Creates an instance of <see cref="StringifiedContent{TType}"/> initialized with the
        /// passed parameters
        /// </summary>
        /// <param name="contentType">Pass the content type</param>
        public StringifiedContent(string contentType) : this(default(TType), contentType, (IStringifier<TType>) new JsonStringifier<TType>())
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="StringifiedContent{TType}"/> initialized with the
        /// passed parameters
        /// </summary>
        /// <param name="content">Pass the content object</param>
        /// <param name="contentType">Pass the content type</param>
        public StringifiedContent(TType content, string contentType) : this(content, contentType,(IStringifier<TType>) new JsonStringifier<TType>())
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="StringifiedContent{TType}"/> initialized with the
        /// passed parameters
        /// </summary>
        /// <param name="content">Pass the content object</param>
        /// <param name="contentType">Pass the content type</param>
        /// <param name="stringifier">Pass the object to string handler</param>
        public StringifiedContent(TType content, string contentType,IStringifier<TType> stringifier) : this(content,contentType, Encoding.UTF8, stringifier)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="StringifiedContent{TType}"/> initialized with the
        /// passed parameters
        /// </summary>
        /// <param name="content">Pass the content object</param>
        /// <param name="contentType">Pass the content type</param>
        /// <param name="encoding">Pass the encoding of the <see cref="string"/></param>
        /// <param name="stringifier">Pass the object to string handler</param>
        public StringifiedContent(TType content, string contentType, Encoding encoding, IStringifier<TType> stringifier)
            : base(stringifier.Stringify(content) ?? string.Empty, encoding, contentType)
        {
            ContentType = contentType;
            _stringifier = stringifier;
            _encoding = encoding;
        }

        /// <summary>
        /// Gets the content type of the stringified data
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// Gets a new content initiialized with the passed data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StringifiedContent<TType> Get(TType data)
        {
            return new StringifiedContent<TType>(data, ContentType, _encoding, _stringifier);
        }
    }
}