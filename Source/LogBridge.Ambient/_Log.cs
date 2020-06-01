using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SoftwarePassion.LogBridge.Configuring;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// Convenience ambient class for Logging.
    /// </summary>
    /// <remarks>
    /// About correlationId: CorrelationId can be used to correlate log events
    /// across e.g. several function calls. The LogWrapper used has to store the 
    /// correlationId in an accessible and searchable manner for this to be 
    /// really useful. CorrelationId is stored in the LogWrappers Properties.
    /// About extendedProperties: Each property of a given extendedProperties
    /// object is stored in the LogWrappers Properties. 
    /// </remarks>
    public static class Log
    {

        /// <summary>
        /// The null exception message - used when an overload with an exception 
        /// parameter is called, but said parameter is null.
        /// </summary>
        public const string NullExceptionMessage = "[null exception]";

		