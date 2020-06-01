using System;
using System.Diagnostics;
using System.Reflection;

namespace SoftwarePassion.LogBridge.Implementation
{
    internal static class CallingMember
    {
        public static StackFrame Find(int startingStackFrameOffset, string currentAssemblyName)
        {
            try
            {
                int currentFrame = startingStackFrameOffset;
                var stackFrame = new StackFrame(currentFrame);
                
                MethodBase methodBase = stackFrame.GetMethod();
                if (methodBase != null)
                {
                    // Work up the stack until we leave the current assembly
                    // which is the assembly of LogBridge
                    var declaringType = methodBase.DeclaringType;
                    var declaringAssemblyName = declaringType?.Assembly.FullName;
                    while (declaringType != null  &&
                        declaringAssemblyName == currentAssemblyName) 
                    {
                        stackFrame = new StackFrame(++currentFrame);
                        methodBase = stackFrame.GetMethod();
                        declaringType = methodBase.DeclaringType;
                    }
                }

                return stackFrame;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}