using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;

namespace Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            //demonstrates struct vs class, array vs list
            //var summaryStructVsClass = BenchmarkRunner.Run<StructVsClassBenchmark>();
            
            //demonstrates modify in place vs write result somewhere else and then replace the original (uses 2x as much memory)
            var summaryModifyInPaceVsSeparateStoreAndReplace = BenchmarkRunner.Run<ModifyInPlaceVsSeparateStore>();
            
            //demonstrates packed vs unpacked / array of structs vs struct of arrays (WIP)
            //var summaryPackedVsUnpacked = BenchmarkRunner.Run<PackedVsUnpackedHotVsCold>();

            //DEBUGGING
            //var summaryModifyInPaceVsSeparateStoreAndReplace = BenchmarkRunner.Run<ModifyInPlaceVsSeparateStore>(new DebugInProcessConfig());
            //var summaryPackedVsUnpacked = BenchmarkRunner.Run<PackedVsUnpackedHotVsCold>(new DebugInProcessConfig());

            //demonstrates string concatenation techniques
            //var summary = BenchmarkRunner.Run<MemoryBenchmarkerDemo>();

        }
    }
}
