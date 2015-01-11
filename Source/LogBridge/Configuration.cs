using System;
using System.Configuration;
using SoftwarePassion.Common.Core;

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

        /// <summary>
        /// Gets the type of the log wrapper, if set.
        /// </summary>
        /// <value>The type of the log wrapper.</value>
        public static Option<string> LogWrapperType
        {
            get
            {
                var explicitWrapperType = ConfigurationManager.AppSettings[LogWrapperTypeAppSettingsKeyName];
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
                var explicitWrapperAssembly = ConfigurationManager.AppSettings[LogWrapperAssemblyAppSettingsKeyName];
                var explicitWrapperAssemblyOption = explicitWrapperAssembly == null
                    ? Option.None<string>()
                    : Option.Some(explicitWrapperAssembly);
                return explicitWrapperAssemblyOption;
            }
        }

        /// <summary>
        /// Gets a value indicating whether to throw and exception when the resolver fails.
        /// </summary>
        public static bool ThrowOnResolverFail
        {
            get
            {
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

        private static string ReadSetting(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (value == null)
                return string.Empty;
            return value;
        }

        private static readonly bool diagnosticsEnabledValue = string.Compare("true", ReadSetting(InternalDiagnosticsAppSettingsKey), StringComparison.OrdinalIgnoreCase) == 0;
    }
}