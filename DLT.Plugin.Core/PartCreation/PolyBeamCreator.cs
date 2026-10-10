using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace DLT.Plugin.Core.PartCreation
{
    public class PolyBeamCreator
    {
        private Contour contour;
        private PartProperties properties;
        public PolyBeamCreator(Contour contour, PartProperties properties)
        {
            this.contour = contour;
            this.properties = properties;
        }

        public PolyBeam CreateInstance()
        {
            PolyBeam polyBeam = new PolyBeam();
            foreach (Point point in contour.ContourPoints)
            {
                polyBeam.AddContourPoint(new ContourPoint(point, null));
            }
            polyBeam.Profile.ProfileString = properties.Profile;
            polyBeam.Material.MaterialString = properties.Material;
            polyBeam.Class = properties.Class.ToString();
            polyBeam.PartNumber = properties.PartNumber;
            polyBeam.AssemblyNumber = properties.AssemblyNumber;
            polyBeam.Name = properties.Name;
            polyBeam.Finish = properties.Finish;
            polyBeam.Position = properties.Position;

            if (polyBeam.Insert()) return polyBeam;

            return null;
        }
    }
}
