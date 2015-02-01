using System;
using System.Collections.Generic;
using System.Configuration;
using SoftwarePassion.Common.Core;
using SoftwarePassion.Common.Core.Extensions;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// Gobal configuration for LogBridge.
    /// </summary>
    public static class Configuration
    {
        private const string LogWrapperTypeAppSettingsKeyName = "SoftwarePassion.LogBridge.LogWrapperType";
        private const string LogWrapperAssemblyAppSettingsKeyName = "SoftwarePassion.LogBridge.LogWrapperAssembly";
        private const string ThrowOnResolverFailAppSettingsKeyName = "SoftwarePassion.LogBridge.ThrowOnResolverFail";
        private const string InternalDiagnosticsAppSettingsKey = "SoftwarePassion.LogBridge.InternalDiagnosticsEnabled";
        private const string LogBridgeConfigurationSectionName = "logBridge";

        /// <summary>
        /// Gets the type of the log wrapper, if set.
        /// </summary>
        /// <value>The type of the log wrapper.</value>
        public static Option<string> LogWrapperType
        {
            get
            {
                string explicitWrapperType = null;
                if (configurationSection.IsSome)
                {
                    explicitWrapperType = configurationSection.Value.LogWrapperType;
                }

                if (explicitWrapperType.IsNullOrEmpty())
                    explicitWrapperType = ConfigurationManager.AppSettings[LogWrapperTypeAppSettingsKeyName];
                    
                var explicitWrapperTypeOption = explicitWrapperType == null
                    ? Option.None<string>()
                    : Option.Some(explicitWrapperType);
                return explicitWrapperTypeOption;
            }
        }

        /// <summary>
        /// Gets the log wrapper assembly, if set.
        /// </summary>
        /// <value>The log wrapper assembly.</value>
        public static Option<string> LogWrapperAssembly
        {
            get
            {
                string explicitWrapperAssembly = null;
                if (configurationSection.IsSome)
                {
                    explicitWrapperAssembly = configurationSection.Value.LogWrapperAssembly;
                }

                if (explicitWrapperAssembly.IsNullOrEmpty())
                    explicitWrapperAssembly = ConfigurationManager.AppSettings[LogWrapperAssemblyAppSettingsKeyName];

                var explicitWrapperAssemblyOption = explicitWrapperAssembly == null
                    ? Option.None<string>()
                    : Option.Some(explicitWrapperAssembly);
                return explicitWrapperAssemblyOption;
            }
        }

        /// <summary>
        /// Gets the stack frame offset count from the configuration, if indicated.
        ///  Otherwise 0 (zero) is returned.
        /// </summary>
        /// <value>The stack frame offset count. The default value is zero.</value>
        public static int StackFrameOffsetCount
        {
            get
            {
                if (configurationSection.IsSome)
                {
                    return configurationSection.Value.StackFrameOffsetCount;
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets the extended properties defined in the configuration file, if
        /// any. Otherwise an empty list is returned.
        /// </summary>
        /// <value>The extended properties.</value>
        public static IEnumerable<ExtendedProperty> ExtendedProperties
        {
            get
            {
                return extendedProperties;
            }
        }

        /// <summary>
        /// Gets a value indicating whether to throw and exception when the resolver fails.
        /// </summary>
        public static bool ThrowOnResolverFail
        {
            get
            {
                if (configurationSection.IsSome)
                {
                    return configurationSection.Value.ThrowOnResolverFail;
                }

                var throwOnResolverFailValue = ConfigurationManager.AppSettings[ThrowOnResolverFailAppSettingsKeyName];
                if (throwOnResolverFailValue == null)
                    return false;
                return string.Compare("true", throwOnResolverFailValue, StringComparison.OrdinalIgnoreCase) == 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether internal diagnostics is enabled.
        /// </summary>
        public static bool InternalDiagnosticsEnabled
        {
            get
            {
                return diagnosticsEnabledValue;
            }
        }

        private static bool GetInternalDiagnosticsEnabled()
        {
            if (configurationSection.IsSome)
            {
                return configurationSection.Value.InternalDiagnosticsEnabled;
            }

            return string.Compare(
                "true", 
                ReadSetting(InternalDiagnosticsAppSettingsKey), 
                StringComparison.OrdinalIgnoreCase) == 0;
        }

        private static string ReadSetting(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (value == null)
                return string.Empty;
            return value;
        }

        private static IList<ExtendedProperty> GetExtendedProperties()
        {
            var result = new List<ExtendedProperty>();
            if (configurationSection.IsSome)
            {
                foreach (ConfigurationExtendedPropertyItem extendedProperty in configurationSection.Value.ExtendedProperties)
                {
                    result.Add(new ExtendedProperty(extendedProperty.Name, extendedProperty.Value));
                }

                return result;
            }

            return result;
        }

        private static Option<LogBridgeConfigurationSection> GetConfigurationSection()
        {
            var section = ConfigurationManager.GetSection(LogBridgeConfigurationSectionName) as LogBridgeConfigurationSection;
            if (section == null)
                return Option.None<LogBridgeConfigurationSection>();

            return Option.Some(section);
        }

        private static readonly Option<LogBridgeConfigurationSection> configurationSection = GetConfigurationSection();
        private static readonly IList<ExtendedProperty> extendedProperties = GetExtendedProperties();
        private static readonly bool diagnosticsEnabledValue = GetInternalDiagnosticsEnabled();
    }
}