using DLT.Plugin.Core.PartCreation;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace DLT.Plugin.Core.BoltCreation
{
    public class BoltCreator
    {
        private Part bolted;
        private Part boltTo;
        private readonly Point start;
        private readonly Point end;
        private BoltProperties properties;

        public BoltCreator(Part bolted, Part boltTo, Point start, Point end, BoltProperties properties)
        {
            this.bolted = bolted;
            this.boltTo = boltTo;
            this.start = start;
            this.end = end;
            this.properties = properties;
        }

        public BoltCreator(Part bolted, Point start, Point end, BoltProperties properties)
        {
            this.bolted = bolted;
            this.start = start;
            this.end = end;
            this.properties = properties;
        }

        public BoltArray CreateInstance()
        {
            BoltArray bolt = new BoltArray();

            bolt.PartToBeBolted = this.bolted;
            if (this.boltTo != null) bolt.PartToBoltTo = this.boltTo;
            bolt.FirstPosition = this.start;
            bolt.SecondPosition = this.end;
            bolt.Bolt = properties.Bolt;

            bolt.BoltSize = this.properties.BoltSize;
            bolt.Tolerance = this.properties.Tolerance;
            bolt.BoltStandard = this.properties.BoltStandard;
            bolt.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_SITE;
            bolt.CutLength = properties.CutLength;
            bolt.ExtraLength = 0;
            bolt.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_YES;
            bolt.Position = properties.Position;

            bolt.Hole1 = this.properties.SlottedHole.Hole1;
            bolt.Hole2 = this.properties.SlottedHole.Hole2;
            bolt.Hole3 = this.properties.SlottedHole.Hole3;
            bolt.Hole4 = this.properties.SlottedHole.Hole4;
            bolt.Hole5 = this.properties.SlottedHole.Hole5;
            bolt.SlottedHoleX = this.properties.SlottedHoleX;
            bolt.SlottedHoleY = this.properties.SlottedHoleY;

            bolt.StartPointOffset = properties.StartOffset;
            bolt.EndPointOffset = properties.EndOffset;

            this.properties.BoltDistX.ForEach(d => bolt.AddBoltDistX(d));
            this.properties.BoltDistY.ForEach(d => bolt.AddBoltDistY(d));

            if (bolt.Insert())
            {
                return bolt;
            }

            return null;
        }
    }
}
