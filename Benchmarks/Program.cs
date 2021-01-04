using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;

namespace Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summaryHS = BenchmarkRunner.Run<HighSpeedBenchmark>();
            //var summary = BenchmarkRunner.Run<MemoryBenchmarkerDemo>();

        }
    }
}
