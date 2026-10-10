using DLT.Plugin.Core.Extension;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using static Tekla.Structures.Catalogs.ProfileItem;

namespace DLT.Plugin.Core
{
    public class PluginInputInfo
    {
        public Part Part { get; } = null;
        public CoordinateSystem CoordinateSystem { get; }
        public ProfileItemTypeEnum ProfileType { get; }
        public ProfileItemSubTypeEnum ProfileSubType { get; }

        private Solid solid = null;
        private Point center = null;
        private double height = 0;
        private double width = 0;
        private double thickness = 0;

        public PluginInputInfo(Part inputPart)
        {
            if (inputPart == null) return;
            //if (false == License.LicenseController.ValidateLicense())
            //    return;

            this.Part = inputPart;
            this.CoordinateSystem = inputPart.GetCoordinateSystem();
            this.ProfileType = inputPart.GetProfileType();
            this.ProfileSubType = inputPart.GetProfileSubType();
        }

        public bool IsValidInput()
        {
            return this.Part != null;
        }

        public Point GetCenterPoint()
        {
            if (this.center != null) return this.center;

            if (this.Part is Beam beam)
            {
                Point _center = new Point
                {
                    X = 0.5 * (beam.StartPoint.X + beam.EndPoint.X),
                    Y = 0.5 * (beam.StartPoint.Y + beam.EndPoint.Y),
                    Z = 0.5 * (beam.StartPoint.Z + beam.EndPoint.Z)
                };

                center = _center;
                return _center;
            }

            return this.center;
        }

        public bool ProfileNeedRecenter()
        {
            return this.ProfileType == ProfileItemTypeEnum.PROFILE_U
                || this.ProfileType == ProfileItemTypeEnum.PROFILE_CC
                || this.ProfileType == ProfileItemTypeEnum.PROFILE_C
                || this.ProfileType == ProfileItemTypeEnum.PROFILE_L;
        }

        public double GetHeight()
        {
            if (this.height != 0) return this.height;

            this.Part.GetReportProperty("HEIGHT", ref this.height);

            return this.height;
        }

        public double GetWidth()
        {
            if (this.width != 0) return this.width;

            this.Part.GetReportProperty("WIDTH", ref this.width);

            return this.width;
        }

        public double GetThickness()
        {
            if (this.thickness != 0) return this.thickness;

            this.Part.GetReportProperty("THICKNESS", ref this.thickness);

            return this.thickness;
        }

        public Solid GetSolid()
        {
            if (this.solid != null) return this.solid;

            this.solid = this.Part.GetSolid();

            return this.solid;
        }
    }
}
