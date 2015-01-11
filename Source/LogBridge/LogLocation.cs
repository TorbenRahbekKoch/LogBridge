using System;
using System.Diagnostics;

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
        /// The method name of the menthod containing the Log.XXXX statement.
        /// </summary>
        public string MethodName;

        /// <summary>
        /// The filenam of the source file containing the Log.XXXX statement.
        /// </summary>
        public string FileName;

        /// <summary>
        /// The line number containing the Log.XXXX statement.
        /// </summary>
        public string LineNumber;

        /// <summary>
        /// The stack frame for the method containing the Log.XXXX statement.
        /// </summary>
        public StackFrame StackFrame;
    }
}