﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using SoftwarePassion.Common.Core.PluginManagement;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// Resolves the LogWrapper implementation to use.
    /// </summary>
    public static class LogWrapperResolver
    {
        /// <summary>
        /// Resolves the LogWrapper to use.
        /// </summary>
        /// <param name="diagnosticsEnabled">If set to <c>true</c> diagnostics is enabled.</param>
        /// <returns>LogWrapper.</returns>
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
                    excludeSystemAssemblies:    ExcludeSystemAssemblies.Yes,
                    typesToExclude:             new List<Type>() {typeof (NullLogWrapper)},
                    assembliesToExclude:        new List<AssemblyName>(),
                    assembliesToLoadExplicitly: explicitWrapperAssemblies,
                    typeToExplicitlyLookFor:    explicitWrapperType);

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