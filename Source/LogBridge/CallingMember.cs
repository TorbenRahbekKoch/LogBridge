﻿using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace SoftwarePassion.LogBridge
{
    internal static class CallingMember
    {
        public static StackFrame Find(int startingStackFrameOffset, int stackFrameOffsetCount)
        {
            Contract.Requires(startingStackFrameOffset >= 0);
            Contract.Requires(stackFrameOffsetCount >= 0);

            try
            {
                var thisAssembly = typeof (CallingMember).Assembly.FullName;
                int currentFrame = startingStackFrameOffset;
                var stackFrame = new StackFrame(currentFrame);
                
                MethodBase methodBase = stackFrame.GetMethod();
                if (methodBase != null)
                {
                    var declaringType = methodBase.DeclaringType;
                    while (declaringType != null  &&
                        declaringType.Assembly.FullName == thisAssembly) 
                    {
                        stackFrame = new StackFrame(++currentFrame);
                        methodBase = stackFrame.GetMethod();
                        declaringType = methodBase.DeclaringType;
                    }
                }

                if (stackFrameOffsetCount == 0)
                    return stackFrame;
                
                return new StackFrame(currentFrame + stackFrameOffsetCount);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}