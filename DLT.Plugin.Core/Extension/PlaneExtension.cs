using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace DLT.Plugin.Core.Extension
{
    public static class PlaneExtension
    {
        //public static Plane OffSetX(this Plane plane, double offsetValue) 
        //{
        //    plane.Origin.OffsetX(offsetValue);
        //    return plane;
        //}

        //public static Plane OffSetY(this Plane plane, double offsetValue)
        //{
        //    plane.Origin.OffsetY(offsetValue);
        //    return plane;
        //}

        //public static Plane OffSetZ(this Plane plane, double offsetValue)
        //{
        //    plane.Origin.OffsetZ(offsetValue);
        //    return plane;
        //}

        public static Plane RotateX(this Plane plane, double angleRad, double newLength = 400)
        {
            Matrix rotate = MatrixFactory.Rotate(angleRad, plane.AxisX);
            Point yRef = plane.Origin.OffsetAlongVector(plane.AxisY, newLength);

            return new Plane
            {
                Origin = plane.Origin,
                AxisX = plane.AxisX.GetNewLength(newLength),
                AxisY = new Vector(rotate.Transform(yRef) - plane.Origin)
            };
        }

        public static Plane RotateY(this Plane plane, double angleRad, double newLength = 400)
        {
            Matrix rotate = MatrixFactory.Rotate(angleRad, plane.AxisY);
            Point xRef = plane.Origin.OffsetAlongVector(plane.AxisX, newLength);

            return new Plane
            {
                Origin = plane.Origin,
                AxisX = new Vector(rotate.Transform(xRef) - plane.Origin),
                AxisY = plane.AxisY.GetNewLength(newLength)
            };
        }

        public static Plane RotateZ(this Plane plane, double angleRad, double newLength = 400)
        {
            Vector axisZ = plane.AxisX.Cross(plane.AxisY);
            Matrix rotate = MatrixFactory.Rotate(angleRad, axisZ);
            Point xRef = plane.Origin.OffsetAlongVector(plane.AxisX, newLength);
            Point yRef = plane.Origin.OffsetAlongVector(plane.AxisY, newLength);

            return new Plane
            {
                Origin = plane.Origin,
                AxisX = new Vector(rotate.Transform(xRef) - plane.Origin),
                AxisY = new Vector(rotate.Transform(yRef) - plane.Origin)
            };
        }

        public static GeometricPlane ToGeometricPlane(this Plane plane)
        {
            return new GeometricPlane
            {
                Origin = plane.Origin,
                Normal = plane.AxisX.Cross(plane.AxisY)
            };
        }
    }
}
