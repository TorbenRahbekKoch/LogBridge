using System;
using System.Diagnostics;

namespace SoftwarePassion.LogBridge
{
    public struct LogLocation
    {
        public Type LoggingClassType;
        public string MethodName;
        public string FileName;
        public string LineNumber;
        public StackFrame StackFrame;
    }
}