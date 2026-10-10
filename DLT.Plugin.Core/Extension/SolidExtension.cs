using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace DLT.Plugin.Core.Extension
{
    public static class SolidExtension
    {
        public static Point GetCenter(this Solid solid)
        {
            return new Point
            {
                X = (solid.MaximumPoint.X + solid.MinimumPoint.X) / 2,
                Y = (solid.MaximumPoint.Y + solid.MinimumPoint.Y) / 2,
                Z = (solid.MaximumPoint.Z + solid.MinimumPoint.Z) / 2
            };
        }
    }
}
