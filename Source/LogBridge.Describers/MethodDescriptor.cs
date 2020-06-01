namespace SoftwarePassion.LogBridge.Describers
{
    /// <summary>
    /// Describes the result from Describe.MethodAndParameters().
    /// </summary>
    public struct MethodDescriptor
    {
        /// <summary>
        /// The name of the method.
        /// </summary>
        public string MethodName { get; internal set; }

        /// <summary>
        /// The full class name containing the method.
        /// </summary>
        public string FullClassName { get; internal set; }

        /// <summary>
        /// A textual description of the parameters to the method.
        /// </summary>
        public string ParameterDescription { get; internal set; }

        /// <summary>
        /// Returns the fully qualified method name with parameters.
        /// </summary>
        public override string ToString()
        {
            return $"{FullClassName}.{MethodName}({ParameterDescription})";
        }
    }
}