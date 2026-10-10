using Tekla.Structures.Model;

namespace DLT.Plugin.Core.ProfileReport
{
    public class ProfileReportC : ProfileReportBase
    {
        public ProfileReportC(Part part) : base(part)
        {
            base.Height = base.GetHeight();
            base.Width = base.GetWidth();
            base.Thickness = base.GetPlateThickness();
        }
    }
}
