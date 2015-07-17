using System;
using System.Configuration;

namespace SoftwarePassion.LogBridge
{
    public class LogBridgeConfigurationSection : ConfigurationSection
    {
        private const string LogWrapperTypeKey = "logWrapperType";
        private const string LogWrapperAssemblyKey = "logWrapperAssembly";
        private const string ThrowOnResolverFailKey = "throwOnResolverFail";
        private const string InternalDiagnosticsEnabledKey = "internalDiagnosticsEnabled";
        private const string ExtendedPropertiesKey = "extendedProperties";

        [ConfigurationProperty(ThrowOnResolverFailKey, DefaultValue = false, IsRequired = false)]
        public bool ThrowOnResolverFail
        {
            get { return (bool) this[ThrowOnResolverFailKey]; }
            set { this[ThrowOnResolverFailKey] = value; }
        }

        [ConfigurationProperty(InternalDiagnosticsEnabledKey, DefaultValue = false, IsRequired = false)]
        public bool InternalDiagnosticsEnabled
        {
            get { return (bool)this[InternalDiagnosticsEnabledKey]; }
            set { this[InternalDiagnosticsEnabledKey] = value; }
        }

        [ConfigurationProperty(LogWrapperTypeKey, DefaultValue = "", IsRequired = false)]
        public string LogWrapperType
        {
            get { return (string) this[LogWrapperTypeKey]; }
            set { this[LogWrapperTypeKey] = value; }
        }

        [ConfigurationProperty(LogWrapperAssemblyKey, DefaultValue = "", IsRequired = false)]
        public string LogWrapperAssembly
        {
            get { return (string) this[LogWrapperAssemblyKey]; }
            set { this[LogWrapperAssemblyKey] = value; }
        }

        [ConfigurationProperty(ExtendedPropertiesKey)]
        public ConfigurationExtendedPropertiesCollection ExtendedProperties
        {
            get
            {
                return (ConfigurationExtendedPropertiesCollection) this[ExtendedPropertiesKey];
            }
        }
    }

    public class ConfigurationExtendedPropertyItem : ConfigurationElement
    {
        private const string NameKey = "name";
        private const string ValueKey = "value";

        [ConfigurationProperty(NameKey, IsRequired = true)]
        public string Name
        {
            get { return (string) base[NameKey]; }
        }

        [ConfigurationProperty(ValueKey, IsRequired = true)]
        public string Value
        {
            get { return (string) base[ValueKey]; }
        }
    }

    [ConfigurationCollection(typeof(Configuration), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
    public class ConfigurationExtendedPropertiesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigurationExtendedPropertyItem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ConfigurationExtendedPropertyItem).Name;
        }

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

        public ConfigurationExtendedPropertyItem this[string name]
        {
            get { return (ConfigurationExtendedPropertyItem)base.BaseGet(name); }
        }
    }
}