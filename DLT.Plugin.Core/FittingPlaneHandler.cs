using DLT.Plugin.Core.Extension;
using DLT.Plugin.Core.ProfileReport;
using System;
using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Solid;

namespace DLT.Plugin.Core
{
    public class FittingPlaneHandler
    {
        private readonly PluginInputInfo primary;
        private readonly PluginInputInfo secon;
        private double gapLength;
        private double? partCutOffSet;
        private FittingSide fittingSide;
        private FittingPosition fittingPosition;
        private FittingType vFitting;
        private FittingType hFitting;
        private const double DEFAULT_VECTOR_LENGTH = 400;
        private bool isCutting;

        public FittingPlaneHandler(PluginInputInfo primary, PluginInputInfo secon, FittingPosition fittingPosition,
            double gapLength, FittingType vFitting = FittingType.Othor, FittingType hFitting = FittingType.Othor, double? partCutOffset = null, bool isCutting = false)
        {
            this.primary = primary;
            this.secon = secon;
            this.vFitting = vFitting;
            this.hFitting = hFitting;
            this.fittingPosition = fittingPosition;
            this.gapLength = gapLength;
            this.partCutOffSet = partCutOffset;
            this.fittingSide = GetFittingSide();
            this.isCutting = isCutting;
        }

        /// <summary>
        /// Creates a fitting with its plane origin located at the center of the secondary part.
        /// </summary>
        public Fitting CreateFitting()
        {
            if (this.primary.Part is Beam && this.secon.Part is Beam)
            {
                Plane fittingPlane = GetFittingPlane(out Plane partCut);

                Plane rotatedPlane = new Plane
                {
                    Origin = fittingPlane.Origin,
                    AxisX = fittingPlane.AxisY,
                    AxisY = fittingPlane.AxisX
                };

                if (this.CreateFlangeCut())
                {
                    this.CreateCutPlaneInternal(this.secon.Part as Beam, partCut);
                }

                return CreateFittingInternal(this.secon.Part as Beam, rotatedPlane);
            }

            return null;
        }

        private Plane GetFittingPlane(out Plane partCutPlane)
        {
            partCutPlane = null;
            Plane gapPlane = GetGapPlane();

            Point fittingOrigin = GetFittingOrigin(gapPlane);

            if (fittingOrigin == null) return null;

            Plane othorPlane = OthorFittingPlane(fittingOrigin);

            Plane fittingPlane = new Plane
            {
                Origin = fittingOrigin,
                AxisX = hFitting == FittingType.Parallel ? GetParallelSeconX(gapPlane, fittingOrigin) : othorPlane.AxisX,
                AxisY = vFitting == FittingType.Parallel ? GetParallelSeconY(gapPlane, fittingOrigin) : othorPlane.AxisY
            };

            if (this.CreateFlangeCut())
            {
                Vector centerSeconToFittingOrigin = new Vector(fittingOrigin - this.secon.GetCenterPoint());
                Vector normalCutPlane = partCutPlane.AxisX.Cross(partCutPlane.AxisY);

                partCutPlane = new Plane
                {
                    Origin = fittingOrigin,
                    AxisX = centerSeconToFittingOrigin.Dot(normalCutPlane) >= 0 ? fittingPlane.AxisY : fittingPlane.AxisY * -1,
                    AxisY = this.primary.CoordinateSystem.AxisX
                };
            }

            return ReCenterPlane(fittingPlane);
        }

        private Plane GetGapPlane()
        {
            double offsetX = this.fittingPosition == FittingPosition.ByComponent
                                                  ? this.gapLength
                                                  : this.primary.GetSolid().MaximumPoint.X + this.gapLength;

            Vector axisY = this.fittingSide == FittingSide.FrontSidePrimaryPart
                                            ? this.primary.CoordinateSystem.AxisY
                                            : this.primary.CoordinateSystem.GetZAxis();

            return new Plane
            {
                Origin = new Point().OffsetX(offsetX),
                AxisX = this.primary.CoordinateSystem.AxisX,
                AxisY = axisY
            };
        }

        private Point GetFittingOrigin(Plane gapPlane)
        {
            GeometricPlane onGapPlane = new GeometricPlane
            {
                Origin = gapPlane.Origin,
                Normal = VectorExtension.AxisXDirect
            };

            Point origin = this.GetAllSeconSolidLineParallelAxisX()
                               .Select(l => Intersection.LineToPlane(l, onGapPlane))
                               .OrderByDescending(p => Distance.PointToPoint(new Point(), ProjectOnSecon(p)))
                               .FirstOrDefault();

            if (this.CreateFlangeCut())
            {
                ProfileReportHI profileReportSecon = new ProfileReportHI(this.secon.Part);

                Plane partCut = new Plane
                {
                    Origin = this.secon.CoordinateSystem.Origin.OffsetAlongVector(OffsetDirect(), profileReportSecon.Thickness / 2 + Math.Abs(this.partCutOffSet.Value)),
                    AxisX = this.secon.CoordinateSystem.AxisX,
                    AxisY = this.secon.CoordinateSystem.AxisY
                };

                Line gapPlaneXpartCutPlane = Intersection.PlaneToPlane(onGapPlane, new GeometricPlane(partCut.Origin, partCut.AxisX, partCut.AxisY));

                Plane parallelSeconOnOrigin = new Plane
                {
                    Origin = origin,
                    AxisX = this.secon.CoordinateSystem.AxisX,
                    AxisY = this.secon.CoordinateSystem.AxisY
                };

                return Intersection.LineToPlane(gapPlaneXpartCutPlane,
                    new GeometricPlane(origin, this.secon.CoordinateSystem.AxisX, this.secon.CoordinateSystem.GetZAxis()));
            }

            return origin;

            Point ProjectOnSecon(Point p) => Projection.PointToPlane(p, new GeometricPlane(this.secon.CoordinateSystem));

            Vector OffsetDirect()
            {
                Point projectOriginOnSeconCoords = Projection.PointToPlane(origin, new GeometricPlane(this.secon.CoordinateSystem));

                return new Vector(origin - projectOriginOnSeconCoords);
            }
        }

        private Fitting CreateFittingInternal(Beam father, Plane fittingPlane)
        {
            if (fittingPlane == null || father == null) return null;

            Fitting fitting = new Fitting();
            fitting.Father = father;
            fitting.Plane = fittingPlane;

            if (fitting.Insert()) return fitting;

            return null;
        }

        private CutPlane CreateCutPlaneInternal(Beam father, Plane plane)
        {
            CutPlane cutPlane = new CutPlane();
            cutPlane.Plane = plane;
            cutPlane.Father = father;

            if (cutPlane.Insert())
            {
                return cutPlane;
            }

            return null;
        }

        private Plane OthorFittingPlane(Point origin)
        {
            return new Plane
            {
                Origin = origin,
                AxisX = this.secon.CoordinateSystem.GetZAxis().GetNewLength(DEFAULT_VECTOR_LENGTH),
                AxisY = this.secon.CoordinateSystem.AxisY.GetNewLength(DEFAULT_VECTOR_LENGTH)
            };
        }

        private Vector GetParallelSeconX(Plane gapPlane, Point fittingOrigin)
        {
            Plane parallelSeconOnOrigin = new Plane
            {
                Origin = fittingOrigin,
                AxisX = this.secon.CoordinateSystem.AxisX,
                AxisY = this.secon.CoordinateSystem.GetZAxis()
            };

            Vector xParallel = Intersection.PlaneToPlane(new GeometricPlane(fittingOrigin, gapPlane.AxisX, gapPlane.AxisY),
                                                         new GeometricPlane(fittingOrigin, parallelSeconOnOrigin.AxisX, parallelSeconOnOrigin.AxisY))
                                           .Direction;
            return xParallel.GetNewLength(DEFAULT_VECTOR_LENGTH);
        }

        private Vector GetParallelSeconY(Plane gapPlane, Point fittingOrigin)
        {
            Plane yParallelSeconAtNearest = new Plane
            {
                Origin = fittingOrigin,
                AxisX = this.secon.CoordinateSystem.AxisX,
                AxisY = this.secon.CoordinateSystem.AxisY
            };

            Vector yParallel = Intersection.PlaneToPlane(new GeometricPlane(fittingOrigin, gapPlane.AxisX, gapPlane.AxisY),
                                                         new GeometricPlane(fittingOrigin, yParallelSeconAtNearest.AxisX, yParallelSeconAtNearest.AxisY))
                                           .Direction;

            return yParallel.GetNewLength(DEFAULT_VECTOR_LENGTH);
        }

        private FittingSide GetFittingSide()
        {
            Vector normalSecon = this.secon.CoordinateSystem.AxisX.Cross(this.primary.CoordinateSystem.AxisX);
            Vector normalPrimary = this.primary.CoordinateSystem.GetZAxis();

            return IsAngleLessOrEqual45(normalPrimary, normalSecon) ? FittingSide.TopSidePrimaryPart
                                                                    : FittingSide.FrontSidePrimaryPart;
        }

        private bool IsAngleLessOrEqual45(Vector a, Vector b)
        {
            if (a == null || b == null)
                return false;

            double dot = Math.Abs(a.Dot(b));
            double lenA = a.GetLength();
            double lenB = b.GetLength();

            if (lenA == 0 || lenB == 0)
                return false;

            double cos45 = Math.Cos(Math.PI / 4);
            return dot * dot >= (cos45 * cos45) * lenA * lenA * lenB * lenB;
        }

        private Plane ReCenterPlane(Plane fittingPlane)
        {
            Plane centerSecon = MovePlaneToCenterSecon();
            if (!this.secon.ProfileNeedRecenter()) return centerSecon;

            Point middleSolidInterSect = FindMiddleSolidIntersect();
            if (middleSolidInterSect == null) return centerSecon;

            return new Plane
            {
                Origin = middleSolidInterSect,
                AxisX = centerSecon.AxisX,
                AxisY = centerSecon.AxisY
            };

            Point FindMiddleSolidIntersect()
            {
                Solid seconSolid = this.secon.GetSolid();
                LineSegment lineSegment = new LineSegment(seconSolid.GetCenter().OffsetAlongVector(this.secon.CoordinateSystem.GetZAxis(), int.MaxValue),
                                                          seconSolid.GetCenter().OffsetAlongVector(this.secon.CoordinateSystem.GetZAxis(), int.MinValue));
                var intersects = seconSolid.Intersect(lineSegment.Point1, lineSegment.Point2);

                if (intersects.Count != 2) return null;

                Point centerIntersect = new LineSegment((Point)intersects[0], (Point)intersects[1]).GetCenTer();

                return Intersection.LineToPlane(new Line(centerIntersect, this.secon.CoordinateSystem.AxisX),
                                                new GeometricPlane(fittingPlane.Origin, fittingPlane.AxisX, fittingPlane.AxisY));
            }

            Plane MovePlaneToCenterSecon()
            {
                Point centerFather = Intersection.LineToPlane(new Line(this.secon.CoordinateSystem.Origin, this.secon.CoordinateSystem.AxisX),
                                                          new GeometricPlane(fittingPlane.Origin, fittingPlane.AxisX, fittingPlane.AxisY));

                return new Plane
                {
                    Origin = new Point(centerFather),
                    AxisX = new Vector(fittingPlane.AxisX),
                    AxisY = new Vector(fittingPlane.AxisY)
                };
            }
        }

        private IEnumerable<Line> GetAllSeconSolidLineParallelAxisX()
        {
            Solid seconSolid = this.secon.GetSolid();

            return seconSolid.GetEdgeEnumerator().ToIEnumerable<Edge>()
                                                 .Where(e => Parallel.VectorToVector(e.GetDirection(), secon.CoordinateSystem.AxisX))
                                                 .Select(e => new Line(e.StartPoint, e.EndPoint));
        }

        private bool CreateFlangeCut()
        {
            return this.hFitting == FittingType.PartCut && this.partCutOffSet != null && this.secon.Part is Beam;
        }

        private enum FittingSide
        {
            FrontSidePrimaryPart,
            TopSidePrimaryPart
        }
    }

    public enum FittingPosition
    {
        ByComponent,
        BySolid
    }

    public enum FittingType
    {
        Othor,
        Parallel,
        PartCut
    }
}
