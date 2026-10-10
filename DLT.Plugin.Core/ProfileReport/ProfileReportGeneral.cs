using Tekla.Structures.Model;
using static Tekla.Structures.Catalogs.ProfileItem;

namespace DLT.Plugin.Core.ProfileReport
{
    public class ProfileReportGeneral : ProfileReportBase
    {
        public ProfileReportGeneral(Part part) : base(part)
        {
            base.Thickness = this.GetThickness();
            base.Width = base.ProfileType == ProfileItemTypeEnum.PROFILE_PL ? base.GetHeight() : base.GetWidth();
            base.Height = base.GetHeight();
        }

        private double GetThickness()
        {
            switch (base.ProfileType)
            {
                case ProfileItemTypeEnum.PROFILE_I:
                case ProfileItemTypeEnum.PROFILE_U:
                    return base.GetWebThickness();

                case ProfileItemTypeEnum.PROFILE_C:
                case ProfileItemTypeEnum.PROFILE_CC:
                case ProfileItemTypeEnum.PROFILE_D:
                case ProfileItemTypeEnum.PROFILE_P:
                case ProfileItemTypeEnum.PROFILE_Z:
                case ProfileItemTypeEnum.PROFILE_ZZ:
                    return base.GetPlateThickness();

                case ProfileItemTypeEnum.PROFILE_PL:
                    return base.GetWidth();
            }

            return 0;
        }
    }
}
