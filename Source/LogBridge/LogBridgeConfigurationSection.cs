using System;
using System.Configuration;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// The configuration section for LogBridge.
    /// </summary>
    public class LogBridgeConfigurationSection : ConfigurationSection
    {
        private const string LogWrapperTypeKey = "logWrapperType";
        private const string LogWrapperAssemblyKey = "logWrapperAssembly";
        private const string ThrowOnResolverFailKey = "throwOnResolverFail";
        private const string InternalDiagnosticsEnabledKey = "internalDiagnosticsEnabled";
        private const string ExtendedPropertiesKey = "extendedProperties";

        /// <summary>
        /// Whether LogBridge should throw an exception if it fails to resolve
        /// a implementation.
        /// </summary>
        [ConfigurationProperty(ThrowOnResolverFailKey, DefaultValue = false, IsRequired = false)]
        public bool ThrowOnResolverFail
        {
            get { return (bool) this[ThrowOnResolverFailKey]; }
            set { this[ThrowOnResolverFailKey] = value; }
        }

        /// <summary>
        /// Whether to enable internal diagnostics, causing LogBridge to log
        /// internal issues using Trace.
        /// </summary>
        [ConfigurationProperty(InternalDiagnosticsEnabledKey, DefaultValue = false, IsRequired = false)]
        public bool InternalDiagnosticsEnabled
        {
            get { return (bool)this[InternalDiagnosticsEnabledKey]; }
            set { this[InternalDiagnosticsEnabledKey] = value; }
        }

        /// <summary>
        /// The full name of the type of the LogWrapper.
        /// </summary>
        [ConfigurationProperty(LogWrapperTypeKey, DefaultValue = "", IsRequired = false)]
        public string LogWrapperType
        {
            get { return (string) this[LogWrapperTypeKey]; }
            set { this[LogWrapperTypeKey] = value; }
        }

        /// <summary>
        /// The fully qualified name of the assembly in which to find the 
        /// implementation of the LogWrapper.
        /// </summary>
        [ConfigurationProperty(LogWrapperAssemblyKey, DefaultValue = "", IsRequired = false)]
        public string LogWrapperAssembly
        {
            get { return (string) this[LogWrapperAssemblyKey]; }
            set { this[LogWrapperAssemblyKey] = value; }
        }

        /// <summary>
        /// The list of extended properties.
        /// </summary>
        [ConfigurationProperty(ExtendedPropertiesKey)]
        public ConfigurationExtendedPropertiesCollection ExtendedProperties
        {
            get
            {
                return (ConfigurationExtendedPropertiesCollection) this[ExtendedPropertiesKey];
            }
        }
    }

    /// <summary>
    /// Encapsulates an extended property in the configuration file.
    /// </summary>
    public class ConfigurationExtendedPropertyItem : ConfigurationElement
    {
        private const string NameKey = "name";
        private const string ValueKey = "value";

        /// <summary>
        /// The name of the property.
        /// </summary>
        [ConfigurationProperty(NameKey, IsRequired = true)]
        public string Name
        {
            get { return (string) base[NameKey]; }
        }

        /// <summary>
        /// The value of the property.
        /// </summary>
        [ConfigurationProperty(ValueKey, IsRequired = true)]
        public string Value
        {
            get { return (string) base[ValueKey]; }
        }
    }

    /// <summary>
    /// Encapsulates the list of extended properties in the configuration file.
    /// </summary>
    [ConfigurationCollection(typeof(Configuration), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
    public class ConfigurationExtendedPropertiesCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Creates a new <see cref="ConfigurationExtendedPropertyItem"/>.
        /// </summary>
        /// <returns>The created item.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigurationExtendedPropertyItem();
        }

        /// <summary>
        /// Returns the name (which is the key) of an item.
        /// </summary>
        /// <param name="element">The element from which to extract the key.</param>
        /// <returns>The name property of the element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ConfigurationExtendedPropertyItem).Name;
        }

        /// <summary>
        /// Returns the <see cref="ConfigurationExtendedPropertyItem"/> for the
        /// given index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The item for the index.</returns>
        public ConfigurationExtendedPropertyItem this[int index]
        {
            get { return (ConfigurationExtendedPropertyItem) base.BaseGet(index); }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                base.BaseAdd(index, value);
            }
        }
        
        /// <summary>
        /// Returns the <see cref="ConfigurationExtendedPropertyItem"/> for the
        /// given name.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <returns>The item for the name.</returns>
        public new ConfigurationExtendedPropertyItem this[string name]
        {
            get { return (ConfigurationExtendedPropertyItem)base.BaseGet(name); }
        }
    }
}