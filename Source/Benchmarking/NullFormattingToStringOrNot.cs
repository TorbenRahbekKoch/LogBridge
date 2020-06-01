using System.Globalization;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace Benchmarking
{
    [MemoryDiagnoser]
    public class NullFormattingToStringOrNot
    {
        // |          Method |     Mean |     Error |    StdDev |   Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        // |---------------- |---------:|----------:|----------:|---------:|-------:|------:|------:|----------:|
        // |     UseToString | 1.154 us | 0.0232 us | 0.0551 us | 1.135 us | 0.1202 |     - |     - |     384 B |
        // | DontUseToString | 1.057 us | 0.0209 us | 0.0196 us | 1.056 us | 0.0973 |     - |     - |     312 B |
        [Benchmark]
        public string UseToString()
        {
            object[] parameters = new object[] { 32, "p2", null, 42.87 };
            var nullFormattedParameters = parameters
                .Select(p => p == null ? "[null]" : p.ToString())
                .ToArray();
            var message = "{0} of these: {1} or {2} gives {3}";
            var formattedMessage = string.Format(
                CultureInfo.InvariantCulture,
                message,
                nullFormattedParameters
            );
            return formattedMessage;
        }


        /// <summary>
        /// Fastest and uses less memory
        /// </summary>
        /// <returns></returns>
        [Benchmark]
        public string DontUseToString()
        {
            object[] parameters = new object[] { 32, "p2", null, 42.87 };
            var nullFormattedParameters = parameters
                .Select(p => p ?? "[null]")
                .ToArray();
            var message = "{0} of these: {1} or {2} gives {3}";
            var formattedMessage = string.Format(
                CultureInfo.InvariantCulture,
                message,
                nullFormattedParameters
            );
            return formattedMessage;
        }
    }
}