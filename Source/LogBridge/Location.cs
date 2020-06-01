using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// Describes the location of a Log.XXXX statement.
    /// </summary>
    public struct Location
    {
        /// <summary>
        /// The method name of the method containing the Log.XXXX statement.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// The filename of the source file containing the Log.XXXX statement.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The line number containing the Log.XXXX statement.
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// Manually creates a Location from the current location.
        /// This is way(!) faster than relying on the automatic discovery mechanism.
        /// </summary>
        /// <param name="callerFileName">The caller file path.</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <param name="callerLineNumber">The caller line number.</param>
        /// <returns>Location.</returns>
        public static Location Here(
            [CallerFilePath] string callerFileName = "",
            [CallerMemberName] string callerMemberName = "", 
            [CallerLineNumber] int callerLineNumber = 0)
        {
            return new Location()
            {
                MethodName = callerMemberName,
                FileName = callerFileName,
                LineNumber = callerLineNumber
            };
        }
    }
}