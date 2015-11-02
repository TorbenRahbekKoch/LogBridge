namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// Encapsulates an extended property whether assigned programmatically
    /// or in the configuration file.
    /// </summary>
    public class ExtendedProperty
    {
        /// <summary>
        /// Creates an ExtendedProperty with the given name and value.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        public ExtendedProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// The Name of the property.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The Value of the property.
        /// </summary>
        public string Value { get; private set; }
    }
}