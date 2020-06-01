using System.Globalization;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace Benchmarking
{
    [MemoryDiagnoser]
    public class Formatting
    {
        // |             Method |     Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        // |------------------- |---------:|----------:|----------:|-------:|------:|------:|----------:|
        // | ExplicitFormatting | 1.186 us | 0.0228 us | 0.0262 us | 0.1392 |     - |     - |     440 B |
        // | ImplicitFormatting | 1.176 us | 0.0234 us | 0.0571 us | 0.1202 |     - |     - |     384 B |

        [Benchmark]
        public string ExplicitFormatting()
        {
            object[] parameters = new object[] { 32, "p2", null, 42.87 };
            var nullFormattedParameters = parameters.Select(p => p == null ? "[null]" : p.ToString())
                .ToArray();
            var message = "{0} of these: {1} or {2} gives {3}";
            var formattedMessage = string.Format(
                CultureInfo.InvariantCulture,
                message,
                nullFormattedParameters[0],
                nullFormattedParameters[1],
                nullFormattedParameters[2],
                nullFormattedParameters[3]
            );
            return formattedMessage;
        }

        /// <summary>
        /// Fastest (just) and uses less memory
        /// </summary>
        /// <returns></returns>
        [Benchmark]
        public string ImplicitFormatting()
        {
            object[] parameters = new object[] { 32, "p2", null, 42.87 };
            var nullFormattedParameters = parameters.Select(p => p == null ? "[null]" : p.ToString())
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