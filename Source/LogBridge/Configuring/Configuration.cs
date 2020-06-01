using System;
using System.Collections.Generic;
using SoftwarePassion.LogBridge.Extension;

namespace SoftwarePassion.LogBridge.Configuring
{
    /// <summary>
    /// Configuration for LogBridge.
    /// </summary>
    public class Configuration
    {
        public Configuration(
            LogBridgeApplicationSettings settings)
        {
            this.InternalDiagnosticsEnabled = settings.InternalDiagnosticsEnabled;
            this.ExtractMetricsFromMessage = settings.ExtractMetricsFromMessage;
            this.MachineName = settings.MachineName;
            this.ProcessName = settings.ProcessName;
            this.ProcessId = settings.ProcessId;
            this.ExtendedProperties = settings.ExtendedProperties ?? new List<ExtendedProperty>();
            this.UseSequenceNumbers = settings.UseSequenceNumbers;
        }

        /// <summary>
        /// Whether to skip checks for whether debug levels are enabled. Since a level check
        /// may come with a slight performance hit, this allows for the check to be disabled.
        /// </summary>
        public bool SkipLevelEnabledCheck { get; }

        /// <summary>
        /// Whether automatic sequence numbering should be used. Comes with a slight
        /// performance hit, but helps order log messages close in time.
        /// </summary>
        public bool UseSequenceNumbers { get; }

        /// <summary>
        /// Whether to extract metrics to properties from the message, e.g.
        /// logger.Debug("{failedCount} items failed", failedCount); will result
        /// in an added "failedCount" extended property.
        /// </summary>
        public bool ExtractMetricsFromMessage { get; }

        /// <summary>
        /// The name (real or pseudo) of the machine on which the process runs
        /// </summary>
        public string MachineName { get; }

        /// <summary>
        /// The name (real or pseudo) of the process
        /// </summary>
        public string ProcessName { get; }

        /// <summary>
        /// The id (real or pseudo) of the process
        /// </summary>
        public int ProcessId { get; }

        /// <summary>
        /// Gets the extended properties defined in the settings, if
        /// any. Otherwise an empty list is returned.
        /// </summary>
        /// <value>The extended properties.</value>
        public IEnumerable<ExtendedProperty> ExtendedProperties { get; }

        /// <summary>
        /// Gets a value indicating whether internal diagnostics is enabled. When internal
        /// diagnostics is enabled errors in the framework is reported via trace.
        /// </summary>
        public bool InternalDiagnosticsEnabled { get; }
    }
}