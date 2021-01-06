using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    //https://gameprogrammingpatterns.com/data-locality.html
    //https://discourse.julialang.org/t/struct-of-arrays-soa-vs-array-of-structs-aos/30015
    //AOS vs SOA -> SIMD?
    //TODO
    public class PackedVsUnpackedHotVsCold
    {
        [Params(10000, 100000, 1000000)]
        public int nr_of_elements;
        public Vector3D[] position;
        public Vector3D[] velocity;

        public ProjectileStruct[] projectileArray;

        [GlobalSetup]
        public void GlobalSetup()
        {
            position = new Vector3D[nr_of_elements];
            velocity = new Vector3D[nr_of_elements];

             projectileArray = new ProjectileStruct[nr_of_elements];
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


            }


        }
        void UpdateProjectile(ref ProjectileStruct projectile)
        {
            projectile.Position = projectile.Position * projectile.Velocity;
        }
        void UpdateProjectile(ref Vector3D position, in Vector3D velocity)
        {
            position = position * velocity;
        }
        [Benchmark]
        public void BenchUnpacked()
        {
            for (int i = 0; i < nr_of_elements; i++)
            {
                UpdateProjectile(ref position[i], in velocity[i]);

            }
        }
        [Benchmark]
        public void BenchPacked()
        {
            for (int i = 0; i < nr_of_elements; i++)
            {
                UpdateProjectile(ref projectileArray[i]);

            }
        }
    }
}
