using Tekla.Structures.Model;

namespace DLT.Plugin.Core.ProfileReport
{
    public class ProfileReportRHS : ProfileReportBase
    {
        public double RoundingRadius { get; }

        public ProfileReportRHS(Part part) : base(part)
        {
            base.Height = base.GetHeight();
            base.Width = base.GetWidth();
            base.Thickness = base.GetPlateThickness();
            this.RoundingRadius = base.GetRoundingRadius();
        }
    }
}
