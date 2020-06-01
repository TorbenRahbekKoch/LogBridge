using System.Collections.Generic;
using System.Globalization;
using BenchmarkDotNet.Attributes;


// |     Method |     Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
// |----------- |---------:|----------:|----------:|-------:|------:|------:|----------:|
// | Dictionary | 8.402 us | 0.0520 us | 0.0461 us | 5.0049 |     - |     - |  15.42 KB |
// |       List | 5.373 us | 0.0302 us | 0.0268 us | 3.1128 |     - |     - |   9.59 KB |
namespace Benchmarking
{
    [MemoryDiagnoser]
    public class DictionaryOrListofKeyValuePair
    {

        [Benchmark]
        public void Dictionary()
        {
            var dict = new Dictionary<string, object>();
            var value = 1;
            for (var count = 0; count < 100; count++)
            {
                var key = count.ToString(CultureInfo.InvariantCulture);
                dict.Add(key, value);
                value += count;
            }
        }

        [Benchmark]
        public void List()
        {
            var list = new List<KeyValuePair<string, object>>();
            var value = 1;
            for (var count = 0; count < 100; count++)
            {
                var key = count.ToString(CultureInfo.InvariantCulture);
                list.Add(new KeyValuePair<string, object>(key, value));
                value += count;
            }
        }
    }
}