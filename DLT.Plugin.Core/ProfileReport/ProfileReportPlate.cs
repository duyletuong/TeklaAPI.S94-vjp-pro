using Tekla.Structures.Model;

namespace DLT.Plugin.Core.ProfileReport
{
    public class ProfileReportPlate : ProfileReportBase
    {
        public ProfileReportPlate(Part part) : base(part)
        {
            base.Height = base.GetHeight();
            base.Thickness = base.GetWidth();
        }
    }
}
