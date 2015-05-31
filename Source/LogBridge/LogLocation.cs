using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// Describes the location of a Log.XXXX statement.
    /// </summary>
    public struct LogLocation
    {
        /// <summary>
        /// The type of the class containing the Log.XXXX statement.
        /// </summary>
        public Type LoggingClassType;

        /// <summary>
        /// The method name of the method containing the Log.XXXX statement.
        /// </summary>
        public string MethodName;

        /// <summary>
        /// The filename of the source file containing the Log.XXXX statement.
        /// </summary>
        public string FileName;

        /// <summary>
        /// The line number containing the Log.XXXX statement.
        /// </summary>
        public string LineNumber;

        /// <summary>
        /// Manually creates a LogLocation from the current location.
        /// </summary>
        /// <param name="callerClassType">Type of the caller class.</param>
        /// <param name="callerFilePath">The caller file path.</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <param name="callerLineNumber">The caller line number.</param>
        /// <returns>LogLocation.</returns>
        public static LogLocation Here(
            Type callerClassType,
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "", 
            [CallerLineNumber] int callerLineNumber = 0)
        {
            return new LogLocation()
            {
                LoggingClassType = callerClassType,
                MethodName = callerMemberName,
                FileName = callerFilePath,
                LineNumber = callerLineNumber.ToString(CultureInfo.InvariantCulture)
            };
        }
    }
}