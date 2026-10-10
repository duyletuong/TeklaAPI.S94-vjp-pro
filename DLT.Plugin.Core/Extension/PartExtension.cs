using Tekla.Structures.Catalogs;
using Tekla.Structures.Model;
using static Tekla.Structures.Catalogs.ProfileItem;

namespace DLT.Plugin.Core.Extension
{
    public static class PartExtension
    {
        public static ProfileItemTypeEnum GetProfileType(this Part part)
        {
            LibraryProfileItem libraryProfile = new LibraryProfileItem();
            libraryProfile.Select(part.Profile.ProfileString);
            if (libraryProfile.ProfileItemType == ProfileItemTypeEnum.PROFILE_UNKNOWN)
            {
                ParametricProfileItem parametricProfile = new ParametricProfileItem();
                parametricProfile.Select(part.Profile.ProfileString);
                return parametricProfile.ProfileItemType;
            }
            else
            {
                return libraryProfile.ProfileItemType;
            }
        }

        public static ProfileItemSubTypeEnum GetProfileSubType(this Part part)
        {
            LibraryProfileItem libraryProfile = new LibraryProfileItem();
            libraryProfile.Select(part.Profile.ProfileString);
            if (libraryProfile.ProfileItemSubType == ProfileItemSubTypeEnum.PROFILE_UNKNOWN_SUBTYPE)
            {
                ParametricProfileItem parametricProfile = new ParametricProfileItem();
                parametricProfile.Select(part.Profile.ProfileString);
                return parametricProfile.ProfileItemSubType;
            }
            else
            {
                return libraryProfile.ProfileItemSubType;
            }
        }
    }
}
