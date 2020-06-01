using System.Collections.Generic;

namespace SoftwarePassion.LogBridge.Configuring
{
    /// <summary>
    /// The configuration section for LogBridge.
    /// </summary>
    public class LogBridgeApplicationSettings 
    {
        public LogBridgeApplicationSettings() { }

        public LogBridgeApplicationSettings(
            bool internalDiagnosticsEnabled, 
            IEnumerable<ExtendedProperty> extendedProperties)
        {
            InternalDiagnosticsEnabled = internalDiagnosticsEnabled;
            ExtendedProperties = extendedProperties;
        }

        /// <summary>
        /// Whether to enable internal diagnostics, causing LogBridge to log
        /// internal issues using Trace.
        /// </summary>
        public bool InternalDiagnosticsEnabled { get; set; }

        /// <summary>
        /// By making the MachineName configurable instead of relying on Environment.MachineName
        /// we can have telling machine names even for etc. docker scenarios.
        /// </summary>
        public string MachineName { get; set; }

        public string ProcessName { get; set; }

        public int ProcessId { get; set; }

        public bool ExtractMetricsFromMessage { get; set; }

        public bool UseSequenceNumbers { get; set; }

        /// <summary>
        /// The list of default extended properties.
        /// </summary>
        public IEnumerable<ExtendedProperty> ExtendedProperties { get; set; }
    }


    // Create a LogBridge Json Configuration Loader with recommended JSON structure..???
}