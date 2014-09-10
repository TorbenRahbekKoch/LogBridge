using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SoftwarePassion.LogBridge.Tests.Unit.DescribeTests
{
    public static class Methods
    {
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method1(byte value1, short value2, int value3, long value4, bool value5, bool value6)
        {
            return Describe.Parameters(value1, value2, value3, value4, value5, value6);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method2(string value)
        {
            return Describe.Parameters(value);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method3(double value)
        {
            return Describe.Parameters(value);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method4(int value1, string value2, double value3)
        {
            return Describe.Parameters(value1, value2, value3);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method5(ICollection<int> value)
        {
            return Describe.Parameters(value);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method6(IDictionary<string, int> value)
        {
            return Describe.Parameters(value);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method7(Enums value)
        {
            return Describe.Parameters(value);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method8(Guid value)
        {
            return Describe.Parameters(value);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method9(Class1 value)
        {
            return Describe.Parameters(value);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method10(Class2 value)
        {
            return Describe.Parameters(value);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method11(DateTime value)
        {
            return Describe.Parameters(value);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method12(object value)
        {
            return Describe.Parameters(value);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method15(DateTime? value)
        {
            return Describe.Parameters(value);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method16(object value)
        {
            return Describe.Parameters(value);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method17(IEnumerable<int> value)
        {
            return Describe.Parameters(value);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static string Method18(TimeSpan value)
        {
            return Describe.Parameters(value);
        }
    }
}