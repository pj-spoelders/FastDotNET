using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;

namespace Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summaryHS = BenchmarkRunner.Run<HighSpeedBenchmark>(new DebugInProcessConfig());
            //var summary = BenchmarkRunner.Run<MemoryBenchmarkerDemo>();

        }
    }
}
