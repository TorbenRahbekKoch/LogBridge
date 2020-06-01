using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Benchmarking
{
    // |  Method |     Mean |     Error |    StdDev |   Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
    // |-------- |---------:|----------:|----------:|---------:|-------:|------:|------:|----------:|
    // | Default | 748.6 ns | 27.078 ns | 78.557 ns | 714.6 ns | 0.3757 |     - |     - |   1.16 KB |
    // |   Small | 593.9 ns |  2.393 ns |  2.122 ns | 593.6 ns | 0.3328 |     - |     - |   1.02 KB |
    // |     Big | 603.9 ns |  3.811 ns |  3.565 ns | 604.2 ns | 0.6275 |     - |     - |   1.93 KB |

    [MemoryDiagnoser]
    public class DictionarySizeCreate
    {
        [Benchmark]
        public void Default()
        {
            var dictionary = new Dictionary<string, object>(0);
            dictionary.Add("42", 3);
            dictionary.Add("43", 3);
            dictionary.Add("44", 3);
            dictionary.Add("45", 3);
            dictionary.Add("46", 3);
            dictionary.Add("47", 3);
            dictionary.Add("48", 3);
            dictionary.Add("49", 3);
        }

        [Benchmark]
        public void Small()
        {
            var dictionary = new Dictionary<string, object>(5);
            dictionary.Add("42", 3);
            dictionary.Add("43", 3);
            dictionary.Add("44", 3);
            dictionary.Add("45", 3);
            dictionary.Add("46", 3);
            dictionary.Add("47", 3);
            dictionary.Add("48", 3);
            dictionary.Add("49", 3);
        }

        [Benchmark]
        public void Big()
        {
            var dictionary = new Dictionary<string, object>(50);
            dictionary.Add("42", 3);
            dictionary.Add("43", 3);
            dictionary.Add("44", 3);
            dictionary.Add("45", 3);
            dictionary.Add("46", 3);
            dictionary.Add("47", 3);
            dictionary.Add("48", 3);
            dictionary.Add("49", 3);

        }
    }
}