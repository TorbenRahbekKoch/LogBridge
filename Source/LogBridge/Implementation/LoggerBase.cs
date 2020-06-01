using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using SoftwarePassion.Common.TimeProviding;
using SoftwarePassion.LogBridge.Configuring;
using SoftwarePassion.LogBridge.Context;
using SoftwarePassion.LogBridge.Extension;
using SoftwarePassion.LogBridge.Implementation;

// ReSharper disable once CheckNamespace
namespace SoftwarePassion.LogBridge
{
    public partial class Logger
    {
        private readonly ILogProvider provider;
        private readonly Configuration configuration;
        private readonly ITime timeProvider;
        private readonly IUsernameProvider usernameProvider;

        public Logger(
            Configuration configuration,
            ITime timeProvider,
            IUsernameProvider usernameProvider,
            ILogProvider provider)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
            this.usernameProvider = usernameProvider ?? throw new ArgumentNullException(nameof(usernameProvider));
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
            this.Context =
                configuration.ExtendedProperties.Any()
                    ? new LogContext(configuration.ExtendedProperties)
                    : null;
        }

        public LogContext Context { get; }

        /// <summary>
        /// To dissallow construction without parameters
        /// </summary>
        private Logger() { }

        public Guid LogEntry(
            Guid? correlationId,
            Exception exception,
            Level level,
            object extendedProperties,
            string message,
            string methodName,
            string filePath,
            int lineNumber)
        {
            var isEnabled = 
                configuration.SkipLevelEnabledCheck || provider.IsLevelEnabled(level);
            if (!isEnabled)
                return Guid.Empty;

            var eventId = Guid.NewGuid();
            var extendedPropertyValues =
                CalculateExtendedProperties(extendedProperties, out var extendedCorrelationId);

            // Calculate extended properties
            // Extract metrics from message
            // Calculate correlation Id
            var actualCorrelationId = CalculateCorrelationId(
                correlationId,
                extendedCorrelationId);

            var sequenceNumber = 
                configuration.UseSequenceNumbers
                ? SequenceNumber.Next()
                : 0;

            var logData = new LogData(
                timeProvider.UtcNow,
                eventId,
                sequenceNumber,
                actualCorrelationId,
                level,
                message,
                usernameProvider.Username,
                configuration.MachineName,
                configuration.ProcessId,
                configuration.ProcessName,
                exception,
                methodName,
                filePath,
                lineNumber,
                extendedPropertyValues);

            try
            {
                provider.LogEntry(logData);
            }
            catch (Exception ex)
            {
                if (configuration.InternalDiagnosticsEnabled)
                    Trace.WriteLine($"Provider: {provider.GetType().FullName}.LogEntry(...) failed: {ex}");

                return Guid.Empty;
            }

            return eventId;
        }

        /// <summary>
        /// Returns whether any of the configured providers have the given Level
        /// enabled:
        /// </summary>
        /// <param name="level">The Level to query for</param>
        /// <returns>Returns true if the Level is enabled, false otherwise.</returns>
        public bool IsLevelEnabled(Level level)
        {
            return provider.IsLevelEnabled(level);
        }

        private Guid? CalculateCorrelationId(
            Guid? explicitCorrelationId,
            Guid? extendedCorrelationId)
        {
            if (explicitCorrelationId != null)
                return explicitCorrelationId;

            if (extendedCorrelationId != null)
                return extendedCorrelationId;

            return Context?.CorrelationId;
        }

        private Dictionary<string, object> CalculateExtendedProperties(object extendedProperties, out Guid? correlationId)
        {
            if (extendedProperties == null)
            {
                return CalculateExtendedPropertiesFromLogContext(out correlationId);
            }

            // TypeDescriptor.GetProperties(Type...) is cached (by TypeDescriptor itself)
            var properties = TypeDescriptor
                .GetProperties(extendedProperties.GetType())
                .OfType<PropertyDescriptor>()
                .ToList();
            var propertyValues = CalculateExtendedPropertiesFromLogContext(out correlationId);
            foreach (PropertyDescriptor property in properties)
            {
                var propertyValue = property.GetValue(extendedProperties);
                if (!IsSpecialPropertyValue(propertyValue, property.Name, ref correlationId))
                {
                    propertyValues[property.Name] = propertyValue;
                }
            }

            return propertyValues;
        }

        private Dictionary<string, object> CalculateExtendedPropertiesFromLogContext(out Guid? correlationId)
        {
            correlationId = null;

            var propertiesValue = Context?.ExtendedProperties;
            if (propertiesValue == null)
                return CreateDictionary();

            var properties = propertiesValue.ToList();
            var propertyValues = CreateDictionary();
            foreach (var property in properties)
            {
                if (!IsSpecialPropertyValue(property.Value, property.Name, ref correlationId))
                {
                    propertyValues[property.Name] = property.Value;
                }
            }

            return propertyValues;
        }

        private bool IsSpecialPropertyValue(object propertyValue, string propertyName, ref Guid? correlationId)
        {
            if (propertyValue is Guid guid && string.Compare(
                    propertyName,
                    LogConstants.CorrelationIdKey,
                    StringComparison.OrdinalIgnoreCase)
                == 0)
            {
                correlationId = guid;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Creates a dictionary which is case-insensitive by using
        /// StringComparer.OrdinalIgnoreCase
        /// </summary>
        /// <returns>Dictionary{System.StringSystem.Object}.</returns>
        private Dictionary<string, object> CreateDictionary()
        {
            return new Dictionary<string, object>(
                10,
                StringComparer.OrdinalIgnoreCase);
        }
    }
}