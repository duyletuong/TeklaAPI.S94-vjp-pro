using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;

namespace DLT.Plugin.Core.Extension // Extension method
{
    public static class PointExtension
    {
        public static Point TransformTo(this Point point, CoordinateSystem coordinateSystem)
        {
            Matrix toCoor = MatrixFactory.ToCoordinateSystem(coordinateSystem);
            return toCoor.Transform(point);
        }

        public static Point OffsetZ(this Point point, double value)
        {
            return new Point(point.X, point.Y, point.Z + value);
        }
    }
}
