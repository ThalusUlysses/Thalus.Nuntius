using System.Globalization;
using Thalus.Nuntius.Core.Contracts;

namespace Thalus.Nuntius.Core.Logging.Contracts
{
    /// <summary>
    /// Publishes the public memebers of <see cref="ILogBook"/> such like <see cref="Errors"/>
    /// used for logging user relevant data
    /// </summary>
    public interface ILogBook
    {
        /// <summary>
        /// Logs an <see cref="Level.Error"/> event that contains user relevant data
        /// </summary>
        /// <param name="invariantText">Pass the <see cref="CultureInfo.InvariantCulture"/> text representation</param>
        /// <param name="text">Pass the <see cref="CultureInfo.CurrentUICulture"/> text representation. If not passed null is used</param>
        /// <param name="obj">Pass additional info as <see cref="object"/>. Null is used when not set</param>
        void Errors(string invariantText,string text=null, object[] obj = null);

        /// <summary>
        /// Logs an <see cref="Level.Warning"/> event that contains user relevant data
        /// </summary>
        /// <param name="invariantText">Pass the <see cref="CultureInfo.InvariantCulture"/> text representation</param>
        /// <param name="text">Pass the <see cref="CultureInfo.CurrentUICulture"/> text representation. If not passed null is used</param>
        /// <param name="obj">Pass additional info as <see cref="object"/>. Null is used when not set</param>
        void Warning(string invariantText, string text = null, object[] obj = null);

        /// <summary>
        /// Logs an <see cref="Level.Debug"/> event that contains user relevant data
        /// </summary>
        /// <param name="invariantText">Pass the <see cref="CultureInfo.InvariantCulture"/> text representation</param>
        /// <param name="text">Pass the <see cref="CultureInfo.CurrentUICulture"/> text representation. If not passed null is used</param>
        /// <param name="obj">Pass additional info as <see cref="object"/>. Null is used when not set</param>
        void Debug(string invariantText, string text = null, object[] obj = null);

        /// <summary>
        /// Logs an <see cref="Level.Info"/> event that contains user relevant data
        /// </summary>
        /// <param name="invariantText">Pass the <see cref="CultureInfo.InvariantCulture"/> text representation</param>
        /// <param name="text">Pass the <see cref="CultureInfo.CurrentUICulture"/> text representation. If not passed null is used</param>
        /// <param name="obj">Pass additional info as <see cref="object"/>. Null is used when not set</param>
        void Info(string invariantText, string text = null, object[] obj = null);

        /// <summary>
        /// Logs an <see cref="Level.Fatal"/> event that contains user relevant data
        /// </summary>
        /// <param name="invariantText">Pass the <see cref="CultureInfo.InvariantCulture"/> text representation</param>
        /// <param name="text">Pass the <see cref="CultureInfo.CurrentUICulture"/> text representation. If not passed null is used</param>
        /// <param name="obj">Pass additional info as <see cref="object"/>. Null is used when not set</param>
        void Fatal(string invariantText, string text = null, object[] obj = null);
    }
}