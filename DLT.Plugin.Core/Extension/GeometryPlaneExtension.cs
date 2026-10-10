using Tekla.Structures.Geometry3d;

namespace DLT.Plugin.Core.Extension
{
    public static class GeometryPlaneExtension
    {
        public static GeometricPlane OffsetAlongVector(this GeometricPlane plane, Vector direct, double distance)
        {
            Point newOrigin = plane.Origin.OffsetAlongVector(direct, distance);
            return new GeometricPlane
            {
                Normal = plane.Normal,
                Origin = newOrigin,
            };
        }
    }
}
