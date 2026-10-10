using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace DLT.Plugin.Core.PartCreation
{
    public class ContourPlateCreator
    {
        private Contour contour;
        private PartProperties properties;
        public ContourPlateCreator(Contour contour, PartProperties properties)
        {
            this.contour = contour;
            this.properties = properties;
        }

        public ContourPlate CreateInstance()
        {
            ContourPlate contourPlate = new ContourPlate();
            foreach (Point point in contour.ContourPoints)
            {
                contourPlate.AddContourPoint(new ContourPoint(point, null));
            }
            contourPlate.Profile.ProfileString = properties.Profile;
            contourPlate.Material.MaterialString = properties.Material;
            contourPlate.Class = properties.Class.ToString();
            contourPlate.PartNumber = properties.PartNumber;
            contourPlate.AssemblyNumber = properties.AssemblyNumber;
            contourPlate.Name = properties.Name;
            contourPlate.Finish = properties.Finish;
            contourPlate.Position = properties.Position;

            if (contourPlate.Insert()) return contourPlate;

            return null;
        }
    }
}
