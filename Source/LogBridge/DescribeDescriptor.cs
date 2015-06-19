using SoftwarePassion.Common.Core.Extensions;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// Describes the result from Describe.MethodAndParameters().
    /// </summary>
    public struct DescribeDescriptor
    {
        public string MethodName;
        public string FullClassName;
        public string ParameterDescription;

        /// <summary>
        /// Returns the fully qualified method name with parameters.
        /// </summary>
        public override string ToString()
        {
            return "{0}.{1}({2})".FormatInvariant(FullClassName, MethodName, ParameterDescription);
        }
    }
}