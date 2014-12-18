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

        public static bool InternalDiagnosticsEnabled
        {
            get
            {
                return diagnosticsEnabledValue;
            }
        }

        private static string ReadSetting(string key)
        {
            var value = ConfigurationManager.AppSettings[InternalDiagnosticsAppSettingsKey];
            if (value == null)
                return string.Empty;
            return value;
        }

        private static readonly bool diagnosticsEnabledValue = string.Compare("true", ReadSetting(InternalDiagnosticsAppSettingsKey), StringComparison.OrdinalIgnoreCase) == 0;
    }
}