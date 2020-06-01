using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Benchmarking
{
    [MemoryDiagnoser]
    public class ToArrayOrToList
    {
        /// <summary>
        /// Fastest and uses less memory
        /// </summary>
        [Benchmark]
        public void ToArray()
        {
            string[] parameters = new[] {"p1", "p2", null, "p3", "p4"};
            var nullFormattedParameters = parameters.Select(p => p == null ? "[null]" : p.ToString())
                .ToArray();

        }

        [Benchmark]
        public void ToList()
        {
            string[] parameters = new[] {"p1", "p2", null, "p3", "p4"};
            var nullFormattedParameters = parameters.Select(p => p == null ? "[null]" : p.ToString())
                .ToList();
        }
    }
}