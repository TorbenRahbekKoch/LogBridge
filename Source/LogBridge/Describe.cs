﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using SoftwarePassion.Common.Core.Extensions;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// Class for helping to describe method parameters when logging within a method.
    /// </summary>
    /// <remarks>
    /// Use like this:
    /// <code>
    ///   var description = Describe.Parameters(param1, param2, param3, param4...)
    /// </code>
    /// where param1... are the parameters of the containing method in the exact same order.
    /// Not all types can be described. In that case it writes paramName: Type or ?? instead.
    /// Please note that due to optimizations (inlining) the method described can be one 
    /// higher in the call stack than you might expect. Not very likely to happen with complex
    /// methods, though.
    /// </remarks>
    public static class Describe
    {
        /// <summary>
        /// Creates a describer, which captures the Method calling, and therefore
        /// can be used to describe the calling method in any other context.
        /// </summary>
        /// <param name="parameterValues">The method parameters.</param>
        /// <returns>A Func, which describes the calling method.</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Func<DescribeDescriptor> CreateDescriber(params object[] parameterValues)
        {
            // Find calling method
            const int parentFrame = 1;
            var stackFrame = new StackFrame(parentFrame);
            MethodBase methodBase = stackFrame.GetMethod();
            if (methodBase == null)
            {
                return () => new DescribeDescriptor()
                {
                    MethodName = string.Empty,
                    FullClassName = string.Empty,
                    ParameterDescription = string.Empty
                };                
            }

            return () => Describe.MethodAndParameters(methodBase, parameterValues);
        }

        /// <summary>
        /// Creates a <see cref="DescribeDescriptor"/> from the surrounding
        /// method and the given parameterValues.
        /// </summary>
        /// <param name="parameterValues">The parameter values in correct order</param>
        /// <returns>A <see cref="DescribeDescriptor"/>.</returns>
        public static DescribeDescriptor MethodAndParameters(params object[] parameterValues)
        {
            // Find calling method
            const int parentFrame = 1;
            var stackFrame = new StackFrame(parentFrame);
            MethodBase methodBase = stackFrame.GetMethod();
            if (methodBase == null)
            {
                return new DescribeDescriptor()
                {
                    MethodName = string.Empty,
                    FullClassName = string.Empty,
                    ParameterDescription = string.Empty
                };
                
            }
            return MethodAndParameters(methodBase, parameterValues);
        }

        /// <summary>
        /// Creates a textual description of the parameters from the
        /// surrounding method and the given parameters.
        /// </summary>
        /// <param name="parameterValues">The parameter values in correct order.</param>
        /// <returns>A textual description of the parameters.</returns>
        public static string Parameters(params object[] parameterValues)
        {
            // Find calling method
            const int parentFrame = 1;
            var stackFrame = new StackFrame(parentFrame);
            MethodBase methodBase = stackFrame.GetMethod();
            if (methodBase == null)
                return string.Empty;

            return MethodAndParameters(methodBase, parameterValues).ParameterDescription;
        }

        /// <summary>
        /// Describes the calling method with the specified parameter values.
        /// </summary>
        /// <param name="methodBase"></param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <returns>A string containing the description.</returns>
        private static DescribeDescriptor MethodAndParameters(MethodBase methodBase, params object[] parameterValues)
        {
            try
            {
                var checkedParameterValues = parameterValues;
                var parameterValueLength = 0;
                if (parameterValues != null)
                    parameterValueLength = parameterValues.Length;
                else
                    checkedParameterValues = new object[] { };
                
                // Now describe the parameters
                var descriptorBuilder = new DescriptorBuilder();                
                var methodParameters = methodBase.GetParameters();
                for (int index = 0; index < methodParameters.Length; index++)
                {
                    if (index >= parameterValueLength)
                        continue;

                    if (index > 0)
                        descriptorBuilder.Append(", ");

                    var parameterValue = checkedParameterValues[index];
                    if (parameterValue == null)
                        DescribeNullParameter(descriptorBuilder, methodParameters[index]);
                    else
                    {
                        DescribeParameter(0, descriptorBuilder, methodParameters[index], parameterValue);
                    }
                }

                string parameterDescription = "{0}.{1}({2})".FormatInvariant(methodBase.DeclaringType.FullName, methodBase.Name, descriptorBuilder.ToString());
                return new DescribeDescriptor()
                {
                    MethodName = methodBase.Name,
                    FullClassName = methodBase.DeclaringType.FullName,
                    ParameterDescription = parameterDescription
                };
            }
            catch (Exception e)
            {
                return new DescribeDescriptor()
                {
                    MethodName = methodBase.Name,
                    FullClassName = methodBase.DeclaringType.FullName,
                    ParameterDescription = "Exception describing method: " + e.ToString()
                };
            }
        }

        private static void DescribeParameter(int recursionDepth, DescriptorBuilder descriptorBuilder, ParameterInfo parameterInfo, object parameterValue)
        {
            Contract.Requires(descriptorBuilder != null);
            Contract.Requires(parameterInfo != null);

            if (RecursionDepthExceeded(recursionDepth, descriptorBuilder))
                return;

            try
            {
                descriptorBuilder.Append(parameterInfo.Name + ": ");
                DescribeParameter(recursionDepth + 1, descriptorBuilder, parameterInfo.ParameterType, parameterValue);
            }
            catch (Exception ex)
            {
                descriptorBuilder.AppendLine("Exception describing parameter '" + parameterInfo.Name + "' : " + ex.ToString());
            }
        }

        private static void DescribeNullParameter(DescriptorBuilder descriptorBuilder, ParameterInfo methodParameter)
        {
            Contract.Requires(descriptorBuilder != null);
            Contract.Requires(methodParameter != null);

            try
            {
                var t = methodParameter.ParameterType;
                if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof (Nullable<>))
                {
                    descriptorBuilder.Append(methodParameter.Name + ": ");
                    var nullableType = t.GetGenericArguments()[0];
                    descriptorBuilder.Append(nullableType.FullName);
                    descriptorBuilder.Append(": null");
                }
                else
                {
                    descriptorBuilder.Append(methodParameter.Name + ": null");
                }
            }
            catch (Exception ex)
            {
                descriptorBuilder.AppendLine("Exception describing parameter '" + methodParameter.Name + "' : " + ex.ToString());
            }
        }

        private static void DescribeParameter(int recursionDepth, DescriptorBuilder descriptorBuilder, Type type, object parameterValue)
        {
            Contract.Requires(descriptorBuilder != null);
            Contract.Requires(type != null);

            if (RecursionDepthExceeded(recursionDepth, descriptorBuilder))
                return;

            if (parameterValue == null)
            {
                descriptorBuilder.Append("null");
                return;
            }

            var compareType = parameterValue.GetType();
            if (compareType.IsGenericType && compareType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                compareType = compareType.GetGenericArguments()[0];
            }

            Func<Type, bool> isType = t => t == compareType;
            if (isType(typeof(string)))
            {
                descriptorBuilder.Append(DescribeParameter(parameterValue as string));
                return;
            }

            if (isType(typeof(bool)))
            {
                descriptorBuilder.Append(DescribeParameter((bool)parameterValue));
                return;
            }

            if (isType(typeof(byte)))
            {
                descriptorBuilder.Append(DescribeParameter((byte)parameterValue));
                return;
            }

            if (isType(typeof(short)))
            {
                descriptorBuilder.Append(DescribeParameter((short)parameterValue));
                return;
            }

            if (isType(typeof(int)))
            {
                descriptorBuilder.Append(DescribeParameter((int)parameterValue));
                return;
            }

            if (isType(typeof(long)))
            {
                descriptorBuilder.Append(DescribeParameter((long)parameterValue));
                return;
            }

            if (isType(typeof(float)) || isType(typeof(double)))
            {
                descriptorBuilder.Append(DescribeParameter((double)parameterValue));
                return;
            }

            if (isType(typeof(decimal)))
            {
                descriptorBuilder.Append(DescribeParameter((decimal)parameterValue));
                return;
            }

            if (isType(typeof(TimeSpan)))
            {
                descriptorBuilder.Append(DescribeParameter((TimeSpan)parameterValue));
                return;
            }

            if (isType(typeof(DateTime)))
            {
                descriptorBuilder.Append(DescribeParameter((DateTime)parameterValue));
                return;
            }

            if (isType(typeof(Guid)))
            {
                descriptorBuilder.Append(DescribeParameter((Guid)parameterValue));
                return;
            }

            if (compareType.FullName.StartsWith("System.Collections.Generic.KeyValuePair", StringComparison.Ordinal))
            {
                DescribeKeyValuePair(recursionDepth + 1, descriptorBuilder, parameterValue);
                return;
            }

            var iCollectionValue = parameterValue as ICollection;
            if (iCollectionValue != null)
            {
                DescribeParameter(recursionDepth + 1, descriptorBuilder, iCollectionValue);
                return;
            }

            var iEnumerableValue = parameterValue as IEnumerable;
            if (iEnumerableValue != null)
            {
                DescribeParameter(recursionDepth + 1, descriptorBuilder, iEnumerableValue);
                return;
            }

            if (compareType.BaseType == typeof(System.Enum))
            {
                DescribeParameter(descriptorBuilder, (System.Enum)parameterValue);
                return;
            }

            if (compareType.IsClass)
            {
                DescribeClassParameter(recursionDepth + 1, descriptorBuilder, parameterValue);
                return;
            }

            descriptorBuilder.AppendLine("|Type: {0}|".FormatInvariant(type.FullName));
        }

        private static string DescribeParameter(TimeSpan value)
        {
            return value.ToString();
        }

        private static string DescribeParameter(DateTime value)
        {
            return "DateTimeKind.{0}:{1}".FormatInvariant(value.Kind, value.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
        }

        private static void DescribeClassParameter(int recursionDepth, DescriptorBuilder descriptorBuilder, object value)
        {
            Contract.Requires(descriptorBuilder != null);
            Contract.Requires(value != null);

            if (RecursionDepthExceeded(recursionDepth, descriptorBuilder))
                return;

            // Describing a class parameter requires describing each property, each of which can also
            // be class parameters. Therefore there is an element of recursion.

            var properties = value.GetType().GetProperties();

            descriptorBuilder.Append(value.GetType().FullName);
            descriptorBuilder.AppendLine("[");
            descriptorBuilder.Indent();
            bool isFirst = true;
            foreach (var propertyInfo in properties)
            {
                if (!isFirst)
                {
                    descriptorBuilder.AppendLine(",");
                }

                isFirst = false;

                descriptorBuilder.Append(propertyInfo.Name);
                descriptorBuilder.Append(": ");
                try
                {
                    var propertyValue = propertyInfo.GetValue(value, BindingFlags.GetProperty, null, null,
                                                              CultureInfo.InvariantCulture);
                    DescribeParameter(recursionDepth + 1, descriptorBuilder, propertyInfo.PropertyType, propertyValue);
                }
                catch (NullReferenceException)
                {
                    descriptorBuilder.Append("null");
                }
                catch (Exception exception)
                {
                    descriptorBuilder.Append(exception.Message);
                }
            }

            descriptorBuilder.Append("]");
            descriptorBuilder.Outdent();
        }

        private static string DescribeParameter(Guid value)
        {
            return value.ToString();
        }

        private static void DescribeParameter(DescriptorBuilder parameterDescription, Enum value)
        {
            Contract.Requires(parameterDescription != null);
            Contract.Requires(value != null);

            parameterDescription.Append("{0}.{1}".FormatInvariant(value.GetType().Name, value.ToString()));
        }

        private static string DescribeParameter(string value)
        {
            if (value == null)
                return "null";

            return "\"{0}\"".FormatInvariant(value);
        }

        private static string DescribeParameter(bool value)
        {
            return "{0}".FormatInvariant(value);
        }

        private static string DescribeParameter(long value)
        {
            return "{0}".FormatInvariant(value);
        }

        private static string DescribeParameter(double value)
        {
            return "{0}".FormatInvariant(value);
        }

        private static string DescribeParameter(decimal value)
        {
            return "{0}".FormatInvariant(value);
        }

        private static void DescribeParameter(int recursionDepth, DescriptorBuilder parameterDescription, ICollection value)
        {
            Contract.Requires(parameterDescription != null);

            if (RecursionDepthExceeded(recursionDepth, parameterDescription))
                return;

            if (value == null)
            {
                parameterDescription.AppendLine("null");
                return;
            }

            bool isFirst = true;
            parameterDescription.AppendLine("[");
            parameterDescription.Indent();
            foreach (object item in value)
            {
                if (!isFirst)
                    parameterDescription.AppendLine(",");
                isFirst = false;

                DescribeParameter(recursionDepth + 1, parameterDescription, item.GetType(), item);
            }

            parameterDescription.Append("]");
            parameterDescription.Outdent();
        }

        private static void DescribeParameter(int recursionDepth, DescriptorBuilder parameterDescription, IEnumerable value)
        {
            Contract.Requires(parameterDescription != null);

            if (RecursionDepthExceeded(recursionDepth, parameterDescription))
                return;

            if (value == null)
            {
                parameterDescription.AppendLine("null");
                return;
            }

            bool isFirst = true;
            parameterDescription.AppendLine("[");
            parameterDescription.Indent();
            foreach (object item in value)
            {
                if (!isFirst)
                    parameterDescription.AppendLine(",");
                isFirst = false;

                DescribeParameter(recursionDepth + 1, parameterDescription, item.GetType(), item);
            }

            parameterDescription.Append("]");
            parameterDescription.Outdent();
        }

        private static void DescribeKeyValuePair(int recursionDepth, DescriptorBuilder descriptorBuilder, object keyValuePair)
        {
            Contract.Requires(descriptorBuilder != null);
            Contract.Requires(keyValuePair != null);

            if (RecursionDepthExceeded(recursionDepth, descriptorBuilder))
                return;

            var properties = keyValuePair.GetType().GetProperties(); 

            var keyProperty = properties.Single(p => p.Name == "Key");
            var valueProperty = properties.Single(p => p.Name == "Value");

            var keyValue = keyProperty.GetValue(keyValuePair, BindingFlags.GetProperty, null, null, CultureInfo.InvariantCulture);
            var valueValue = valueProperty.GetValue(keyValuePair, BindingFlags.GetProperty, null, null, CultureInfo.InvariantCulture);

            descriptorBuilder.Append("[");
            DescribeParameter(recursionDepth + 1, descriptorBuilder, keyValue.GetType(), keyValue);
            descriptorBuilder.Append(":");
            DescribeParameter(recursionDepth + 1, descriptorBuilder, valueValue.GetType(), valueValue);
            descriptorBuilder.Append("]");
        }

        private static bool RecursionDepthExceeded(int recursionDepth, DescriptorBuilder descriptorBuilder)
        {
            bool recursionTooDeep = recursionDepth > 8;
            if (recursionTooDeep)
                descriptorBuilder.Append("Recursion to deep.");
            return recursionTooDeep;
        }
    }
}