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
    }
}