using Tekla.Structures.Model;

namespace DLT.Plugin.Core.ProfileReport
{
    public class ProfileReportHI : ProfileReportBase
    {
        public double FlangeThickness { get; }
        public double FlangeThickness1 { get; }
        public double FlangeThickness2 { get; }
        public ProfileReportHI(Part part) : base(part)
        {
            base.Height = base.GetHeight();
            base.Width = base.GetWidth();
            base.Thickness = GetWebThickness();

            this.FlangeThickness = base.GetFlangeThickness();
            this.FlangeThickness1 = base.GetFlangeThickness1();
            this.FlangeThickness2 = base.GetFlangeThickness2();
        }
    }
}
