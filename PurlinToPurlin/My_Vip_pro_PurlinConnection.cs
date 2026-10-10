using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Plugins;
using Tekla.Structures.Dialog;
using DLT.Plugin.Core;
using DLT.Plugin.Core.PartCreation;
using Tekla.Structures.Model;
using DLT.Plugin.Core.Dialog;

namespace PurlinToPurlin
{
    //Khai bao ten Connection
    [Plugin("My_Vip_pro_PurlinConnection")]
    //Khai bao giao dien Connection
    [PluginUserInterface("My_Vip_pro_PurlinConnection")]
    //Khai bao kieu secondary
    [SecondaryType(SecondaryType.SECONDARYTYPE_ONE)]
    //Khai bao kieu Auto direction - thuong dung BASIC
    [AutoDirectionType(Tekla.Structures.AutoDirectionTypeEnum.AUTODIR_BASIC)]
    //Khai bao cach xac dinh mat phang coordinate system cua connection
    [PositionType(Tekla.Structures.PositionTypeEnum.COLLISION_PLANE)]

    public class My_Vip_pro_PurlinConnection : ConnectionBase
    {
        private readonly StructuresData data;
        private Model Model = new Model();
        PluginInputInfo primaryInput;
        PluginInputInfo secondaryInput;

        public My_Vip_pro_PurlinConnection(StructuresData structuresData)
        {
            this.data = structuresData.ApplyDefaults();
        }

        public override bool Run()
        {
            try
            {
                //lay main part nguoi dung pick
                Beam mainPart = Model.SelectModelObject(base.Primary) as Beam;
                Beam secondaryPart = Model.SelectModelObject(base.Secondaries[0]) as Beam;

                this.primaryInput = new PluginInputInfo(mainPart);
                this.secondaryInput = new PluginInputInfo(secondaryPart);

                Fitting fitting = CreateFitting();



                CreateDummyParts();
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message + Environment.NewLine + e.StackTrace);
                return false;
            }
        }

        private void CreateDummyParts()
        {
            new BeamCreator(new Tekla.Structures.Geometry3d.Point(),
                            new Tekla.Structures.Geometry3d.Point(1000, 0, 0),
                            GetMainBeamProperty())
                .CreateInstance();

            new BeamCreator(new Tekla.Structures.Geometry3d.Point(),
                            new Tekla.Structures.Geometry3d.Point(0, 1000, 0),
                            GetSeconBeamProperty())
                .CreateInstance();

            new BeamCreator(new Tekla.Structures.Geometry3d.Point(),
                            new Tekla.Structures.Geometry3d.Point(0, 0, 1000),
                            GetThirdBeamProperty())
                .CreateInstance();
        }

        private Fitting CreateFitting()
        {
            FittingPlaneHandler fittingPlaneHandler = new FittingPlaneHandler(
                primaryInput,
                secondaryInput,
                GetFittingPosFromDialog(),
                data.gap,
                GetFittingTypeFromDialog(),
                GetFittingTypeFromDialog());

            return fittingPlaneHandler.CreateFitting();

            FittingPosition GetFittingPosFromDialog()
            {
                System.Windows.Forms.MessageBox.Show(data.fittingPosition.ToString());
                if (data.fittingPosition == 0)
                {
                    return FittingPosition.ByBoxPlane;
                }

                return FittingPosition.ByCollisionPlane;
            }

            FittingType GetFittingTypeFromDialog()
            {
                if (data.fittingType == 0)
                {
                    return FittingType.Perpendicular;
                }

                return FittingType.Parallel;
            }
        }

        private PartProperties GetMainBeamProperty()
        {
            return new PartProperties
            {
                Profile = "PL10*200",
                Material = "S235JR",
                PartNumber = new NumberingSeries { Prefix = "p", StartNumber = 1},
                AssemblyNumber = new NumberingSeries("A", 1),
                Position = new Tekla.Structures.Model.Position(),
                Class = "5",
                Name = "ABC",
                Finish = ""
            };
        }

        private PartProperties GetSeconBeamProperty()
        {
            return new PartProperties
            {
                Profile = "PL10*200",
                Material = "S235JR",
                PartNumber = new NumberingSeries { Prefix = "p", StartNumber = 1 },
                AssemblyNumber = new NumberingSeries("A", 1),
                Position = new Tekla.Structures.Model.Position(),
                Class = "10",
                Name = "ABC",
                Finish = ""
            };
        }

        private PartProperties GetThirdBeamProperty()
        {
            return new PartProperties
            {
                Profile = "PL10*200",
                Material = "S235JR",
                PartNumber = new NumberingSeries { Prefix = "p", StartNumber = 1 },
                AssemblyNumber = new NumberingSeries("A", 1),
                Position = new Tekla.Structures.Model.Position(),
                Class = "7",
                Name = "ABC",
                Finish = ""
            };
        }
    }
}
