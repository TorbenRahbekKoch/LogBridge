using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using SoftwarePassion.Common.Core.PluginManagement;

namespace SoftwarePassion.LogBridge
{
    public static class LogWrapperResolver
    {
        public static LogWrapper Resolve(bool diagnosticsEnabled)
        {
            try
            {
                var explicitWrapperType = Configuration.LogWrapperType;

                var explicitWrapperAssembly = Configuration.LogWrapperAssembly;
                var explicitWrapperAssemblies = explicitWrapperAssembly.IsSome
                    ? new List<AssemblyName>() { new AssemblyName(explicitWrapperAssembly.Value) }
                    : new List<AssemblyName>();

                var configuration = new PluginFinderConfiguration(
                    ExcludeSystemAssemblies.Yes,
                    new List<Type>() {typeof (NullLogWrapper)},
                    new List<AssemblyName>(),
                    explicitWrapperAssemblies,
                    explicitWrapperType);

                return PluginFinder.FindAndActivate<LogWrapper>(
                    configuration,
                    diagnosticsEnabled);
            }
            catch (InvalidOperationException exception)
            {
                if (diagnosticsEnabled)
                    Trace.WriteLine("Failed to resolve a LogWrapper due to an exception: " + exception.ToString());

                if (Configuration.ThrowOnResolverFail)
                    throw;
                return new NullLogWrapper(diagnosticsEnabled);
            } 
        }
    }
}