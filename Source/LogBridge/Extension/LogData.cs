using System;
using System.Collections.Generic;
using System.Globalization;

namespace SoftwarePassion.LogBridge.Extension
{
    /// <summary>
    /// The data being logged and collected when using the Log.XXXX methods.
    /// </summary>
    public class LogData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogData"/> class.
        /// </summary>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="sequenceNumber"></param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="username">The username.</param>
        /// <param name="machineName">Name of the machine.</param>
        /// <param name="processId">The process identifier.</param>
        /// <param name="processName">Name of the process.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="methodName">The method</param>
        /// <param name="filePath">The file with the method</param>
        /// <param name="lineNumber">Line number of log location</param>
        /// <exception cref="System.ArgumentNullException">correlationId</exception>
        public LogData(
            DateTime timeStamp, 
            Guid eventId, 
            uint sequenceNumber,
            Guid? correlationId, 
            Level level, 
            string message, 
            string username, 
            string machineName,
            int processId,
            string processName,
            Exception exception,
            string methodName,
            string filePath,
            int lineNumber,
            Dictionary<string, object> properties)
        {
            CorrelationId = correlationId;
            TimeStamp = timeStamp;
            EventId = eventId;
            SequenceNumber = sequenceNumber;
            Level = level;
            Message = message;
            Username = username;
            MachineName = machineName;
            ProcessId = processId;
            ProcessName = processName;
            Exception = exception;
            MethodName = methodName;
            FilePath = filePath;
            LineNumber = lineNumber;
            Properties = properties;
        }

        /// <summary>
        /// Gets the time stamp.
        /// </summary>
        public DateTime TimeStamp { get; }

        /// <summary>
        /// Gets unique the event identifier assigned to each event.
        /// </summary>
        public Guid EventId { get;  }

        public uint SequenceNumber { get; }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        public Guid? CorrelationId { get;  }

        /// <summary>
        /// Gets the log level.
        /// </summary>
        public Level Level { get;  }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get;  }

        /// <summary>
        /// Gets the Username.
        /// </summary>
        public string Username { get;  }

        /// <summary>
        /// Gets the MachineName.
        /// </summary>
        public string MachineName { get;  }

        /// <summary>
        /// Gets the ProcessId.
        /// </summary>
        public int ProcessId { get;  }

        /// <summary>
        /// Gets the ProcessName.
        /// </summary>
        public string ProcessName { get; }

        /// <summary>
        /// Gets the Exception, if any.
        /// </summary>
        public Exception Exception { get;  }

        /// <summary>
        /// Gets the method name if any
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// The path of the file with MethodName
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// The line number of the log location
        /// </summary>
        public int LineNumber { get; }

        /// <summary>
        /// Gets the extended properties.
        /// </summary>
        public Dictionary<string, object> Properties { get;  }
    }
}