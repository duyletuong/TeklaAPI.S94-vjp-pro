using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace DLT.Plugin.Core.PartCreation
{
    public class BeamCreator
    {
        private readonly Point start;
        private readonly Point end;
        private PartProperties properties;

        public BeamCreator(Point start, Point end, PartProperties properties)
        {
            this.start = start;
            this.end = end;
            this.properties = properties;
        }

        public Beam CreateInstance()
        {
            Beam beam = new Beam();
            beam.StartPoint = start;
            beam.EndPoint = end;
            beam.Profile.ProfileString = properties.Profile;
            beam.Material.MaterialString = properties.Material;
            beam.Class = properties.Class;
            beam.PartNumber = properties.PartNumber;
            beam.AssemblyNumber = properties.AssemblyNumber;
            beam.Name = properties.Name;
            beam.Finish = properties.Finish;
            beam.Position = properties.Position;

            if (beam.Insert()) return beam;

            return null;
        }
    }
}
