using DLT.Plugin.Core.Extension;
using Tekla.Structures.Model;
using static Tekla.Structures.Catalogs.ProfileItem;

namespace DLT.Plugin.Core.ProfileReport
{
    public abstract class ProfileReportBase
    {
        public double Height { get; protected set; }
        public double Width { get; protected set; }
        public double Thickness { get; protected set; }
        public ProfileItemTypeEnum ProfileType { get; }
        public ProfileItemSubTypeEnum ProfileSubType { get; }

        public bool IsBeam => this.Part is Beam;
        public bool IsPolyBeam => this.Part is PolyBeam;
        public bool IsContourPlate => this.Part is ContourPlate;

        protected Part Part { get; }

        private const string PROPERTY_GROUP = "PROFILE.";
        public ProfileReportBase(Part part)
        {
            Part = part;
            this.ProfileType = part.GetProfileType();
            this.ProfileSubType = part.GetProfileSubType();
        }
        protected double GetDiameter()
        {
            return GetDoubleReport("DIAMETER");
        }

        protected double GetFlangeThickness()
        {
            return GetDoubleReport("FLANGE_THICKNESS");
        }

        protected double GetFlangeThickness1()
        {
            return GetDoubleReport("FLANGE_THICKNESS_1");
        }

        protected double GetFlangeThickness2()
        {
            return GetDoubleReport("FLANGE_THICKNESS_2");
        }

        protected double GetFlangeWidth()
        {
            return GetDoubleReport("FLANGE_WIDTH");
        }

        protected double GetFlangeWidth1()
        {
            return GetDoubleReport("FLANGE_WIDTH_1");
        }

        protected double GetFlangeWidth2()
        {
            return GetDoubleReport("FLANGE_WIDTH_2");
        }

        protected double GetHeight()
        {
            return GetDoubleReport("HEIGHT");
        }

        protected double GetPlateThickness()
        {
            return GetDoubleReport("PLATE_THICKNESS");
        }

        protected double GetWebThickness()
        {
            return GetDoubleReport("WEB_THICKNESS");
        }

        protected double GetWebThickness1()
        {
            return GetDoubleReport("WEB_THICKNESS_1");
        }

        protected double GetWebThickness2()
        {
            return GetDoubleReport("WEB_THICKNESS_2");
        }

        protected double GetWidth()
        {
            return GetDoubleReport("WIDTH");
        }

        protected double GetWidth1()
        {
            return GetDoubleReport("WIDTH_1");
        }

        protected double GetWidth2()
        {
            return GetDoubleReport("WIDTH_2");
        }

        protected double GetRoundingRadius()
        {
            return GetDoubleReport("ROUNDING_RADIUS");
        }

        protected string GetProfileSubType()
        {
            return GetStringReport("SUBTYPE");
        }

        protected double GetDoubleReport(string propertyName)
        {
            double value = 0;
            this.Part.GetReportProperty(PROPERTY_GROUP + propertyName, ref value);
            return value;
        }

        protected string GetStringReport(string propertyName)
        {
            string value = string.Empty;
            this.Part.GetReportProperty(PROPERTY_GROUP + propertyName, ref value);
            return value;
        }

        protected int GetIntReport(string propertyName)
        {
            int value = 0;
            this.Part.GetReportProperty(PROPERTY_GROUP + propertyName, ref value);
            return value;
        }
    }
}
