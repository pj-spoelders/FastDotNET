using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    public static class BenchUtility
    {
        //https://stackoverflow.com/questions/273313/randomize-a-listt
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rnd = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static void Shuffle<T>(this T[] array)
        {
            Random rnd = new Random();

            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
        }
    }

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
    //https://www.jacksondunstan.com/articles/3860
    public class HighSpeedBenchmark
    {


        [Params(10000,100000, 1000000)]
        public int nr_of_elements;

        private List<AICar> AICarList;
        private List<AICarStruct> AICarStructList;
        private List<AICar> AICarShuffledList;
        private List<AICarStruct> AICarStructShuffledList;
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
            //without shuffling it's 2 times faster, but this is probably because of very simple memory allocation going on and small object size, so basically a happy convenience memory wise
            AICarStructList = new List<AICarStruct>(nr_of_elements);
            for (int i = 0; i < nr_of_elements; i++)
            {
                AICarStructList.Add(new AICarStruct { SomePos = rnd.Next() });
            }

            //shuffled, shallow copy! refences
            AICarShuffledList = new List<AICar>(AICarList);
            BenchUtility.Shuffle(AICarShuffledList);
            //shuffled, copy from value types
            AICarStructShuffledList = new List<AICarStruct>(AICarStructList);
            BenchUtility.Shuffle(AICarStructShuffledList);

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
        public void ShuffledListOfClasses()
        {

            int res = 0;
            for (int i = 0; i < nr_of_elements; i++)
            {
                res += AICarShuffledList[i].SomePos;
            }

        }
        [Benchmark]
        public void ShuffledListOfStructs()
        {
            int res = 0;
            for (int i = 0; i < nr_of_elements; i++)
            {
                res += AICarStructShuffledList[i].SomePos;
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

