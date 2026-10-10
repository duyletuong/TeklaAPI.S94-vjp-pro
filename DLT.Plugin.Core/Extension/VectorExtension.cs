using Tekla.Structures.Geometry3d;

namespace DLT.Plugin.Core.Extension
{
    public static class VectorExtension
    {
        public static readonly Vector AxisXDirect = new Vector(1, 0, 0);
        public static readonly Vector AxisYDirect = new Vector(0, 1, 0);
        public static readonly Vector AxisZDirect = new Vector(0, 0, 1);

        public static Vector GetNewLength(this Vector v, double newLength)
        {
            double currentLength = v.GetLength();
            if (currentLength == 0) return new Vector(0, 0, 0);

            double scale = newLength / currentLength;
            return new Vector(v.X * scale, v.Y * scale, v.Z * scale);
        }

        public static Vector Reverse(this Vector v)
        {
            return new Vector
            {
                X = -v.X,
                Y = -v.Y,
                Z = -v.Z
            };
        }
    }
}
