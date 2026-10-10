using Tekla.Structures.Model;

namespace DLT.Plugin.Core.ProfileReport
{
    public class ProfileReportZ : ProfileReportBase
    {
        public double FlangeWidth { get; }
        public double FlangeWidth1 { get; }
        public double FlangeWidth2 { get; }
        public ProfileReportZ(Part part) : base(part)
        {
            base.Height = base.GetHeight();
            base.Thickness = base.GetPlateThickness();

            this.FlangeWidth = base.GetFlangeWidth();
            this.FlangeWidth1 = base.GetFlangeWidth1();
            this.FlangeWidth2 = base.GetFlangeWidth2();
        }
    }
}
