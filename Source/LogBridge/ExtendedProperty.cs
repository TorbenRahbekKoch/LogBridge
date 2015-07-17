namespace SoftwarePassion.LogBridge
{
    public class ExtendedProperty
    {
        public ExtendedProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }
    }
}