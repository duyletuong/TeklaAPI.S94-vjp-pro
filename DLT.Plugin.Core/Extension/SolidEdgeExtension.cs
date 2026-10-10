using Tekla.Structures.Geometry3d;
using Tekla.Structures.Solid;

namespace DLT.Plugin.Core.Extension
{
    public static class SolidEdgeExtension
    {
        public static Vector GetDirection(this Edge edge)
        {
            return new Vector(edge.EndPoint - edge.StartPoint);
        }
    }
}
