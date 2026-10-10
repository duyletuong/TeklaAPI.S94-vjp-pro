using Tekla.Structures.Geometry3d;

namespace DLT.Plugin.Core.Extension
{
    public static class CoordinateSystemExtension
    {
        public static Vector GetZAxis(this CoordinateSystem coordinateSystem)
        {
            return coordinateSystem.AxisX.Cross(coordinateSystem.AxisY);
        }
    }
}
