using Tekla.Structures.Model;

namespace DLT.Plugin.Core.ProfileReport
{
    public class ProfileReportPD : ProfileReportBase
    {
        public double Diameter { get; }
        public ProfileReportPD(Part part) : base(part)
        {
            this.Diameter = base.GetDiameter();
            base.Thickness = base.GetPlateThickness();
        }
    }
}
