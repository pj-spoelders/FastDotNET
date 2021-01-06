using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    //https://www.jacksondunstan.com/articles/3860
    public class ModifyInPlaceVsSeparateStore
    {
        [Params(10000, 100000)]
        public int nr_of_elements;
        public Vector3D[] position;
        public Vector3D[] velocity;
        public Vector3D[] newPosition;


        //void UpdateProjectile(ref ProjectileStruct projectile, float time)
        void UpdateProjectile(ref Vector3D position,in Vector3D velocity)
        {
            position = position * velocity;
        }
        void UpdateProjectileInStore(in Vector3D position, in Vector3D velocity, ref Vector3D newPosition)
        {
            newPosition =  position * velocity;
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            position = new Vector3D[nr_of_elements];
            velocity = new Vector3D[nr_of_elements];
            newPosition = new Vector3D[nr_of_elements];

            //randomize
            Random rnd = new Random();

            for (int i = 0; i < nr_of_elements; i++)
            {
                position[i].X = BenchUtility.NextFloat(rnd);
                position[i].Y = BenchUtility.NextFloat(rnd);
                position[i].Z = BenchUtility.NextFloat(rnd);

                velocity[i].X = BenchUtility.NextFloat(rnd);
                velocity[i].Y = BenchUtility.NextFloat(rnd);
                velocity[i].Z = BenchUtility.NextFloat(rnd);

                newPosition[i].X = BenchUtility.NextFloat(rnd);
                newPosition[i].Y = BenchUtility.NextFloat(rnd);
                newPosition[i].Z = BenchUtility.NextFloat(rnd);
            }


        }


        [Benchmark]
        public void InPlace()
        {
            for (int i = 0; i < nr_of_elements; i++)
            {
                position[i] = position[i] * velocity[i];
                
            }
        }

        [Benchmark]
        public void InStoreAndSwap()
        {
            for (int i = 0; i < nr_of_elements; i++)
            {
                newPosition[i] = position[i] * velocity[i];

            }
            //SWAP
            var holdReferenceTemp = position;
            position = newPosition;
            newPosition = holdReferenceTemp;
        }
        [Benchmark]
        public void InStoreAndReplace()
        {
            for (int i = 0; i < nr_of_elements; i++)
            {
                newPosition[i] = position[i] * velocity[i];

            }
            //SWITCH AND REPLACE
            position = newPosition;
            newPosition = new Vector3D[nr_of_elements];
        }
        [Benchmark]
        public void InStoreNoSOR()
        {
            for (int i = 0; i < nr_of_elements; i++)
            {
                newPosition[i] = position[i] * velocity[i];

            }

        }
        [Benchmark]
        public void InPlaceMethod()
        {
            for (int i = 0; i < nr_of_elements; i++)
            {
               UpdateProjectile(ref position[i], in velocity[i]);

            }
        }
        [Benchmark]
        public void InStoreMethodNoSOR()
        {
            for (int i = 0; i < nr_of_elements; i++)
            {
                UpdateProjectileInStore(in position[i] ,in velocity[i],ref newPosition[i]);

            }
        }
        [Benchmark]
        public void InStorMethodAndSwap()
        {
            for (int i = 0; i < nr_of_elements; i++)
            {
                UpdateProjectileInStore(in position[i], in velocity[i], ref newPosition[i]);

            }
            //SWAP
            var holdReferenceTemp = position;
            position = newPosition;
            newPosition = holdReferenceTemp;

        }

    }
}
