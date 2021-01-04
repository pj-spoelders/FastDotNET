using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    class Position
    {
        public int X;
        public int Y;
        public int Z;
    }
    class Direction
    {
        public int X;
        public int Y;
        public int Z;
    }
    class Speed
    {
        public double X;
        public double Y;
        public double Z;
    }
    class AICar
    {
        public int SomePos;
        public int SomeDir;
        public int Whatever;
    }
    public struct AICarStruct
    {
        public int SomePos;
        public int SomeDir;
        public int Whatever;
    }
    public class HighSpeedBenchmark
    {
        [Params(100000,1000000,10000000)]
        public int nr_of_elements;

        private List<AICar> AICarList;
        private List<AICarStruct> AICarStructList;
        private AICarStruct[] AICarStructArray;

        [GlobalSetup]
        public void GlobalSetup()
        {
            Random rnd = new Random();

            AICarList = new List<AICar>(nr_of_elements);
            for (int i = 0; i < nr_of_elements; i++)
            {
                AICarList.Add(new AICar { SomePos = rnd.Next() });
            }

            AICarStructList = new List<AICarStruct>(nr_of_elements);
            for (int i = 0; i < nr_of_elements; i++)
            {
                AICarStructList.Add(new AICarStruct { SomePos = rnd.Next() });
            }

            AICarStructArray = new AICarStruct[nr_of_elements];
            for (int i = 0; i < nr_of_elements; i++)
            {
                AICarStructArray[i].SomePos = rnd.Next();
            }

        }

        [Benchmark]
        public void ListOfClasses()
        {

            int res = 0;
            for (int i = 0; i < nr_of_elements; i++)
            {
                res += AICarList[i].SomePos;
            }

        }
        [Benchmark]
        public void ListOfStructs()
        {
            int res = 0;
            for (int i = 0; i < nr_of_elements; i++)
            {
                res += AICarStructList[i].SomePos;
            }

        }
        [Benchmark]
        public void ArrayOfStructs()
        {
            int res = 0;
            for (int i = 0; i < nr_of_elements; i++)
            {
                res += AICarStructArray[i].SomePos;
            }

        }
        [GlobalCleanup]
        public void GlobalCleanup()
        {
            // Disposing logic

        }
    }
}

