using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using SoftwarePassion.Common.Core;
using SoftwarePassion.Common.Core.PluginManagement;

namespace SoftwarePassion.LogBridge
{
    public static class LogWrapperResolver
    {
        public const string LogWrapperTypeAppSettingsKeyName = "SoftwarePassion.LogBridge.LogWrapper";
        public const string LogWrapperAssemblyAppSettingsKeyName = "SoftwarePassion.LogBridge.LogWrapperAssembly";

        public static LogWrapper Resolve(bool diagnosticsEnabled)
        {
            try
            {
                var explicitWrapperType = ConfigurationManager.AppSettings[LogWrapperTypeAppSettingsKeyName];
                var explicitWrapperTypeOption = explicitWrapperType == null
                    ? Option.None<string>()
                    : Option.Some(explicitWrapperType);

                var explicitWrapperAssembly = ConfigurationManager.AppSettings[LogWrapperAssemblyAppSettingsKeyName];
                var explicitWrapperAssemblies = explicitWrapperAssembly == null
                    ? new List<AssemblyName>()
                    : new List<AssemblyName>() {new AssemblyName(explicitWrapperAssembly)};

                var configuration = new PluginFinderConfiguration(
                    ExcludeSystemAssemblies.Yes,
                    new List<Type>() {typeof (NullLogWrapper)},
                    new List<AssemblyName>(),
                    explicitWrapperAssemblies,
                    explicitWrapperTypeOption);

                return PluginFinder.FindAndActivate<LogWrapper>(
                    configuration,
                    diagnosticsEnabled);
            }
            catch (InvalidOperationException exception)
            {
                if (diagnosticsEnabled)
                    Trace.WriteLine("Failed to resolve a LogWrapper due to an exception: " + exception.ToString());
                return new NullLogWrapper(diagnosticsEnabled);
            } 
        }
    }
}