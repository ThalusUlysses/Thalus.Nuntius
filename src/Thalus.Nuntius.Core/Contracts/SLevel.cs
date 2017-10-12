namespace Thalus.Nuntius.Core.Contracts
{
    /// <summary>
    /// Implements support for evaluating trace level
    /// </summary>
    public static class SLevel
    {
        /// <summary>
        /// Checks if passed log categories match the passed level
        /// </summary>
        /// <param name="level">Level supported </param>
        /// <param name="cat">level of trace entry</param>
        /// <returns>Returns true if matches, othewise false</returns>
        public static bool IsLog(Level level, Level cat)
        {
            return (level & cat) > 0;
        }

        /// <summary>
        /// Checks of the passed <see cref="Level"/> is active
        /// </summary>
        /// <param name="cat">Pass teh to be checked <see cref="Level"/></param>
        /// <returns>Returns true if <see cref="Level"/> is active otherwise false</returns>
        public static bool IsLogFatal(Level cat)
        {
            return ((int)cat & 16) > 0;

        }

        /// <summary>
        /// Checks of the passed <see cref="Level"/> is active
        /// </summary>
        /// <param name="cat">Pass teh to be checked <see cref="Level"/></param>
        /// <returns>Returns true if <see cref="Level"/> is active otherwise false</returns>
        public static bool IsLogWarning(Level cat)
        {
            return ((int)cat & 4) > 0;

        }

        /// <summary>
        /// Checks of the passed <see cref="Level"/> is active
        /// </summary>
        /// <param name="cat">Pass teh to be checked <see cref="Level"/></param>
        /// <returns>Returns true if <see cref="Level"/> is active otherwise false</returns>
        public static bool IsLogErrors(Level cat)
        {
            return ((int)cat & 8) > 0;

        }

        /// <summary>
        /// Checks of the passed <see cref="Level"/> is active
        /// </summary>
        /// <param name="cat">Pass teh to be checked <see cref="Level"/></param>
        /// <returns>Returns true if <see cref="Level"/> is active otherwise false</returns>
        public static bool IsLogInfo(Level cat)
        {
            return ((int)cat & 2) > 0;
        }

        /// <summary>
        /// Checks of the passed <see cref="Level"/> is active
        /// </summary>
        /// <param name="cat">Pass teh to be checked <see cref="Level"/></param>
        /// <returns>Returns true if <see cref="Level"/> is active otherwise false</returns>
        public static bool IsLogDebug(Level cat)
        {
            return ((int)cat & 1) > 0;
        }
    }
}