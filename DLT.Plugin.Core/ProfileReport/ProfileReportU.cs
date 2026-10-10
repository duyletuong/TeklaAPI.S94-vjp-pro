using Tekla.Structures.Model;

namespace DLT.Plugin.Core.ProfileReport
{
    public class ProfileReportU : ProfileReportBase
    {
        public double FlangeThickness { get; }
        public ProfileReportU(Part part) : base(part)
        {
            base.Height = base.GetHeight();
            base.Width = base.GetWidth();
            base.Thickness = base.GetWebThickness();

            this.FlangeThickness = base.GetFlangeThickness();
        }
    }
}
