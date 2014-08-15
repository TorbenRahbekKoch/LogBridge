using System;
using System.Collections.Generic;

namespace SoftwarePassion.LogBridge.Tests.Shared
{
    public class ExtendedProperties
    {
        public int IntValue{get { return 42; }} 
        public string StringValue {get { return "87"; }}
        public Guid GuidValue { get { return new Guid("{C49134A0-F0A0-4A4D-BF9A-47E376F4FBF3}"); }}

        public Dictionary<string, object> AsProperties
        {
            get
            {
                var properties = new Dictionary<string, object>()
                {
                    {"IntValue", IntValue},
                    {"StringValue", StringValue},
                    {"GuidValue", GuidValue},
                };
                return properties;
            }
        }
    }
}