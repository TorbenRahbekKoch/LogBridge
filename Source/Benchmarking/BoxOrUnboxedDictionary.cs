using System.Collections.Generic;
using BenchmarkDotNet.Attributes;


// |            Method |     Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
// |------------------ |---------:|----------:|----------:|-------:|------:|------:|----------:|
// |   BoxedDictionary | 32.58 us | 0.6413 us | 1.1565 us | 7.5989 |     - |     - |   24000 B |
// | UnboxedDictionary | 22.99 us | 0.1556 us | 0.1299 us |      - |     - |     - |         - |

namespace Benchmarking
{
    [MemoryDiagnoser]
    public class BoxOrUnboxedDictionary
    {
        private readonly Dictionary<int, object> boxedData = new Dictionary<int, object>();
        private readonly Dictionary<int, int> unboxedData = new Dictionary<int, int>();

        [Benchmark]
        public void BoxedDictionary()
        {
            var value = 1;
            for (var count = 0; count < 1000; count++)
            {
                boxedData[count] = value;
                value += (int)boxedData[count];
            }
        }

        [Benchmark]
        public void UnboxedDictionary()
        {
            var value = 1;
            for (var count = 0; count < 1000; count++)
            {
                unboxedData[count] = value;
                value += unboxedData[count];
            }
        }
    }
}