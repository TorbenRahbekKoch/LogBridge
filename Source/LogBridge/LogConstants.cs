using System;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// Contains names of properties used for storing data in 
    /// Properties property of a <see cref="LogData"/>.
    /// </summary>
    public static class LogConstants
    {
        /// <summary>
        /// The key used when storing a correlation identifier in the 
        /// Properties property of a <see cref="LogData"/>.
        /// </summary>
        public const string CorrelationIdKey = "correlationId";

        /// <summary>
        /// The key used when storing an event id in the 
        /// Properties property of a <see cref="LogData"/>.
        /// </summary>
        public const string EventIdKey = "eventId";

        /// <summary>
        /// The key used when storing a provided <see cref="Exception"/> in the
        /// Properties property of a <see cref="LogData"/>.
        /// </summary>
        public const string ExceptionKey = "exception";

        /// <summary>
        /// The key used when storing a machine name in the
        /// Properties property of a <see cref="LogData"/>.
        /// </summary>
        public const string MachineNameKey = "machineName";

        /// <summary>
        /// The key used when storing an application name in the
        /// Properties property of a <see cref="LogData"/>.
        /// </summary>
        public const string ApplicationNameKey = "applicationName";
        
        /// <summary>
        /// The key used when storing a namespace name in the
        /// Properties property of a <see cref="LogData"/>.
        /// </summary>
        public const string NamespaceKey = "namespace";

        /// <summary>
        /// The key used when storing a class name in the
        /// Properties property of a <see cref="LogData"/>.
        /// </summary>
        public const string ClassNameKey = "class";

        /// <summary>
        /// The key used when storing a line number in the
        /// Properties property of a <see cref="LogData"/>.
        /// </summary>
        public const string LineNumberKey = "linenumber";

        /// <summary>
        /// The key used when storing a process name in the
        /// Properties property of a <see cref="LogData"/>.
        /// </summary>
        public const string ProcessNameKey = "processName";

        /// <summary>
        /// The key used when storing filename in the 
        /// Properties property of a <see cref="LogData"/>.
        /// </summary>
        public const string FilenameKey = "filename";

        /// <summary>
        /// The key used when storing a method name in the
        /// Properties property of a <see cref="LogData"/>.
        /// </summary>
        public const string MethodNameKey = "methodName";

        /// <summary>
        /// The key used when storing a username in the
        /// Properties property of a <see cref="LogData"/>.
        /// </summary>
        public const string UsernameKey = "usernameKey";
    }
}