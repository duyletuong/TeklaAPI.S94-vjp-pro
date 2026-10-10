using Tekla.Structures.Model;

namespace DLT.Plugin.Core.ProfileReport
{
    internal class ProfileReportP : ProfileReportBase
    {
        public ProfileReportP(Part part) : base(part)
        {
            base.Height = base.GetHeight();
            base.Width = base.GetWidth();
            base.Thickness = base.GetPlateThickness();
        }
    }
}
