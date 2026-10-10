using System.Collections.Generic;
using Tekla.Structures.Model;

namespace DLT.Plugin.Core.BoltCreation
{
    public struct BoltProperties
    {
        public double BoltSize { get; }
        public string BoltStandard { get; }
        public double Tolerance { get; }
        public List<double> BoltDistX { get; }
        public List<double> BoltDistY { get; }
        public Position Position { get; }
        public Offset StartOffset { get; }
        public Offset EndOffset { get; }
        public double CutLength { get; }
        public double SlottedHoleX { get; }
        public double SlottedHoleY { get; }
        public SlottedHole SlottedHole { get; }
        /// <summary>
        /// Indicates whether the instance is a bolt or just a hole. 
        /// </summary>
        public bool Bolt { get; }

        public BoltProperties(
            double boltSize,
            string boltStandard,
            double tolerance,
            List<double> boltDistX,
            List<double> boltDistY,
            Position position,
            Offset startOffset,
            Offset endOffset,
            double cutLength,
            double slottedHoleX,
            double slottedHoleY,
            SlottedHole slottedHole,
            bool bolt)
        {
            BoltSize = boltSize;
            BoltStandard = boltStandard;
            Tolerance = tolerance;
            BoltDistX = boltDistX;
            BoltDistY = boltDistY;
            Position = position;
            StartOffset = startOffset;
            EndOffset = endOffset;
            CutLength = cutLength;
            Bolt = bolt;
            SlottedHoleX = slottedHoleX;
            SlottedHoleY = slottedHoleY;
            SlottedHole = slottedHole;
        }
    }

    public struct SlottedHole
    {
        public bool Hole1 { get; }
        public bool Hole2 { get; }
        public bool Hole3 { get; }
        public bool Hole4 { get; }
        public bool Hole5 { get; }

        public SlottedHole(bool hole1, bool hole2)
        {
            Hole1 = hole1;
            Hole2 = hole2;
            Hole3 = false;
            Hole4 = false;
            Hole5 = false;
        }

        public SlottedHole(bool hole1, bool hole2, bool hole3, bool hole4, bool hole5)
        {
            Hole1 = hole1;
            Hole2 = hole2;
            Hole3 = hole3;
            Hole4 = hole4;
            Hole5 = hole5;
        }
    }
}
