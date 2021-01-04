using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Text;

namespace Benchmarks
{
    [MemoryDiagnoser, MaxIterationCount(20)]
    public class MemoryBenchmarkerDemo
    {
        int NumberOfItems = 100000;
        [Benchmark]
        public string ConcatStringsUsingStringBuilder()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < NumberOfItems; i++)
            {
                sb.Append("Hello World!" + i);
            }
            return sb.ToString();
        }
        [Benchmark]
        public string ConcatStringsUsingGenericList()
        {
            var list = new List<string>(NumberOfItems);
            for (int i = 0; i < NumberOfItems; i++)
            {
                list.Add("Hello World!" + i);
            }
            return list.ToString();
        }
        [Benchmark]
        public string ConcatUsingStringBuilderWithCalculatedCapacity()
        {
            int maxNrOfCharacters = ("Hello World!".Length + 1 + NumberOfItems.ToString().Length);
            var sb = new StringBuilder(NumberOfItems *maxNrOfCharacters);
            for (int i = 0; i < NumberOfItems; i++)
            {
                sb.Append("Hello World!" + i);
            }
            return sb.ToString();
        }
        [Benchmark]
        public string ConcatStringsUsingConcatStrApproach()
        {
            var s = "";
            for (int i = 0; i < 10000; i++)
            {
                s+=("Hello World!" + i);
            }
            return s;
        }
    }
}