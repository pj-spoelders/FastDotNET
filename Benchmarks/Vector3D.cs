namespace Benchmarks
{
    public struct Vector3D
    {
        public float X;
        public float Y;
        public float Z;

        public static Vector3D operator*(Vector3D a,Vector3D b)
        {
            return new Vector3D
            {
                X = a.X * b.X,
                Y = a.Y * b.Y,
                Z = a.Z * b.Z
            };
        }
        public static Vector3D operator+( Vector3D a, Vector3D b)
        {
            return new Vector3D
            {
                X = a.X + b.X,
                Y = a.Y + b.Y,
                Z = a.Z + b.Z
            };
        }
    }
}