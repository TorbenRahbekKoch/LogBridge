using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
        public static DescribeDescriptor MethodAndParameters(params object[] parameterValues)
        {
            // Find calling method
            const int parentFrame = 1;
            var stackFrame = new StackFrame(parentFrame);
            MethodBase methodBase = stackFrame.GetMethod();
            if (methodBase == null)
                return new DescribeDescriptor()
                {
                    MethodName = string.Empty,
                    FullClassName = string.Empty,
                    ParameterDescription = string.Empty
                };

            return MethodAndParameters(methodBase, parameterValues);
        }

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
                        DescribeParameter(descriptorBuilder, methodParameters[index], parameterValue);
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

        private static void DescribeParameter(DescriptorBuilder descriptorBuilder, ParameterInfo methodParameter, object parameterValue)
        {
            Contract.Requires(descriptorBuilder != null);
            Contract.Requires(methodParameter != null);

            try
            {
                DescribeParameter(descriptorBuilder, methodParameter.ParameterType, parameterValue);
            }
            catch (Exception ex)
            {
                descriptorBuilder.AppendLine("Exception describing parameter '" + methodParameter.Name + "' : " + ex.ToString());
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
                    var nullableType = t.GetGenericArguments()[0];
                    descriptorBuilder.Append(nullableType.FullName);
                    descriptorBuilder.Append(": null");
                }
                else
                {
                    descriptorBuilder.Append("null");
                }
            }
            catch (Exception ex)
            {
                descriptorBuilder.AppendLine("Exception describing parameter '" + methodParameter.Name + "' : " + ex.ToString());
            }
        }

        private static void DescribeParameter(DescriptorBuilder descriptorBuilder, Type type, object parameterValue)
        {
            Contract.Requires(descriptorBuilder != null);
            Contract.Requires(type != null);

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
                DescribeKeyValuePair(descriptorBuilder, parameterValue);
                return;
            }

            var iCollectionValue = parameterValue as ICollection;
            if (iCollectionValue != null)
            {
                DescribeParameter(descriptorBuilder, iCollectionValue);
                return;
            }

            var iEnumerableValue = parameterValue as IEnumerable;
            if (iEnumerableValue != null)
            {
                DescribeParameter(descriptorBuilder, iEnumerableValue);
                return;
            }

            if (compareType.BaseType == typeof(System.Enum))
            {
                DescribeParameter(descriptorBuilder, (System.Enum)parameterValue);
                return;
            }

            if (compareType.IsClass)
            {
                DescribeClassParameter(descriptorBuilder, parameterValue);
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

        private static void DescribeClassParameter(DescriptorBuilder descriptorBuilder, object value)
        {
            Contract.Requires(descriptorBuilder != null);
            Contract.Requires(value != null);

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
                    DescribeParameter(descriptorBuilder, propertyInfo.PropertyType, propertyValue);
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

        private static void DescribeParameter(DescriptorBuilder parameterDescription, ICollection value)
        {
            Contract.Requires(parameterDescription != null);

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

                DescribeParameter(parameterDescription, item.GetType(), item);
            }

            parameterDescription.Append("]");
            parameterDescription.Outdent();
        }

        private static void DescribeParameter(DescriptorBuilder parameterDescription, IEnumerable value)
        {
            Contract.Requires(parameterDescription != null);

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

                DescribeParameter(parameterDescription, item.GetType(), item);
            }

            parameterDescription.Append("]");
            parameterDescription.Outdent();
        }

        private static void DescribeKeyValuePair(DescriptorBuilder descriptorBuilder, object keyValuePair)
        {
            Contract.Requires(descriptorBuilder != null);
            Contract.Requires(keyValuePair != null);

            var properties = keyValuePair.GetType().GetProperties();
            var keyProperty = properties.Single(p => p.Name == "Key");
            var valueProperty = properties.Single(p => p.Name == "Value");

            var keyValue = keyProperty.GetValue(keyValuePair, BindingFlags.GetProperty, null, null, CultureInfo.InvariantCulture);
            var valueValue = valueProperty.GetValue(keyValuePair, BindingFlags.GetProperty, null, null, CultureInfo.InvariantCulture);

            descriptorBuilder.Append("[");
            DescribeParameter(descriptorBuilder, keyValue.GetType(), keyValue);
            descriptorBuilder.Append(":");
            DescribeParameter(descriptorBuilder, valueValue.GetType(), valueValue);
            descriptorBuilder.Append("]");
        }
    }
}