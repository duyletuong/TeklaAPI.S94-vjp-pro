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
        public override bool Run()
        {
            try
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

                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message + Environment.NewLine + e.StackTrace);
                return false;
            }
        }

        private PartProperties GetMainBeamProperty()
        {
            return new PartProperties
            {
                Profile = "PL10*200",
                Material = "SS400",
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
                Material = "SS400",
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
                Material = "SS400",
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
