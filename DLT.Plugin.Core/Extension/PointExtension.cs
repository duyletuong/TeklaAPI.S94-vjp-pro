using System;
using Tekla.Structures.Geometry3d;

namespace DLT.Plugin.Core.Extension
{
    public static class PointExtension
    {
        public static Point TransformTo(this Point point, CoordinateSystem coordinateSystem)
        {
            return MatrixFactory.ToCoordinateSystem(coordinateSystem).Transform(point);
        }

        /// <summary>
        /// Transform point from coordinate system to another coordinate system
        /// </summary>
        public static Point TransformBy(this Point point, CoordinateSystem fromCoords, CoordinateSystem toCoords)
        {
            // dont use stupid method in below comment
            //MatrixFactory.ByCoordinateSystems();

            Matrix toGlobal = MatrixFactory.FromCoordinateSystem(fromCoords);
            Matrix toLocal = MatrixFactory.ToCoordinateSystem(toCoords);

            Point global = toGlobal.Transform(point);

            return toLocal.Transform(global);
        }

        /// <summary>
        /// Transform point from coordinate system to global
        /// </summary>
        public static Point TransformToGlobal(this Point point, CoordinateSystem fromCoords)
        {
            Matrix toGlobal = MatrixFactory.FromCoordinateSystem(fromCoords);
            Matrix toLocal = MatrixFactory.ToCoordinateSystem(new CoordinateSystem());

            Point global = toGlobal.Transform(point);

            return toLocal.Transform(global);
        }

        public static Point ProjectTo(this Point point, GeometricPlane plane)
        {
            return Projection.PointToPlane(point, plane);
        }

        public static Point OffsetX(this Point point, double offsetValue)
        {
            return new Point(point.X + offsetValue, point.Y, point.Z);
        }

        public static Point OffsetY(this Point point, double offsetValue)
        {
            return new Point(point.X, point.Y + offsetValue, point.Z);
        }

        public static Point OffsetZ(this Point point, double offsetValue)
        {
            return new Point(point.X, point.Y, point.Z + offsetValue);
        }

        public static Point ToXYPlane(this Point point)
        {
            return new Point(point.X, point.Y);
        }

        public static Point ToXZPlane(this Point point)
        {
            return new Point(point.X, 0, point.Z);
        }

        public static Point ToYZPlane(this Point point)
        {
            return new Point(0, point.Y, point.Z);
        }

        public static Point OffsetAlongVector(this Point target, Vector direction, double distance)
        {
            if (Math.Abs(distance) < GeometryConstants.DISTANCE_EPSILON * 100)
            {
                return target;
            }

            double magnitude = direction.GetLength();
            if (magnitude < GeometryConstants.DISTANCE_EPSILON)
            {
                return target;
            }

            double scale = distance / magnitude;

            return new Point(
                target.X + direction.X * scale,
                target.Y + direction.Y * scale,
                target.Z + direction.Z * scale
            );
        }
    }
}
