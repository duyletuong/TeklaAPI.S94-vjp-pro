using Tekla.Structures.Geometry3d;

namespace DLT.Plugin.Core.Extension
{
    public static class LineSegmentExtension
    {
        public static Point GetCenTer(this LineSegment segment)
        {
            double centerX = (segment.Point1.X + segment.Point2.X) / 2.0;
            double centerY = (segment.Point1.Y + segment.Point2.Y) / 2.0;
            double centerZ = (segment.Point1.Z + segment.Point2.Z) / 2.0;

            return new Point(centerX, centerY, centerZ);
        }
    }
}
