using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tekla.Structures.Plugins;
using System.Threading.Tasks;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using System.Collections;
using DLT.Plugin.Core.Extension;

namespace TeklaAPI.S94
{
    //Khai bao ten plugin (trong cua so component tekla)
    [Plugin("My Super S94")]
    //Khai bao giao dien (form) cho plugin
    [PluginUserInterface("TeklaAPI.S94.MainForm")]

    public class S94 : PluginBase
    {
        //Khai bao instance cua class StructuresData
        private StructuresData Data;

        //Khai bao contructor cho class plugin
        public S94(StructuresData data)
        {
            Data = data;
        }

        public double H1;
        public double H2;
        public double webThick;
        public double outerFlangeThick;
        public double innerFlangeThick;
        public double outerFlangeWidth;
        public double innerFlangeWidth;
        public string webPrefix;
        public string webASPrefix;
        public string outerFlangePrefix;
        public string innerFlangePrefix;
        public int webStartNum;
        public int webASStartNum;
        public int innerFlangeStartNum;
        public int outerFlangeStartNum;
        public string webMaterial;
        public string outerFlangeMaterial;
        public string innerFlangeMaterial;

        public override List<InputDefinition> DefineInput()
        {
            List<InputDefinition> inputDefinitions = new List<InputDefinition>();

            Picker picker = new Picker();
            ArrayList points = picker.PickPoints(Picker.PickPointEnum.PICK_TWO_POINTS);

            inputDefinitions.Add(new InputDefinition(points[0] as Point));
            inputDefinitions.Add(new InputDefinition(points[1] as Point));

            return inputDefinitions;
        }

        public override bool Run(List<InputDefinition> Input)
        {
            GetValuesFromDialog();
            Point p1 = Input[0].GetInput() as Point;
            Point p2 = Input[1].GetInput() as Point;

            Beam topFlange = CreateTopFlange(p1, p2);
            Beam web = CreateWeb(p1, p2);
            Beam bottomFlange = CreateBottomFlange(p1, p2);

            WeldToAssembly(web, topFlange, bottomFlange);
            CreateCutting(web, bottomFlange, p1);

            return true;
        }

        private Beam CreateTopFlange(Point input1, Point input2)
        {
            Beam topFlange = new Beam();
            topFlange.StartPoint = input1;
            topFlange.EndPoint = input2;

            topFlange.Profile.ProfileString = "PL" + outerFlangeThick + "*" + outerFlangeWidth; //PL10*200
            topFlange.PartNumber.Prefix = outerFlangePrefix;
            topFlange.PartNumber.StartNumber = outerFlangeStartNum;
            topFlange.AssemblyNumber.Prefix = "A";
            topFlange.AssemblyNumber.StartNumber = webASStartNum;
            topFlange.Material.MaterialString = outerFlangeMaterial;

            topFlange.Position.Rotation = Position.RotationEnum.FRONT;
            topFlange.Position.Depth = Position.DepthEnum.BEHIND;
            topFlange.Position.Plane = Position.PlaneEnum.MIDDLE;

            topFlange.Insert();

            return topFlange;
        }

        private Beam CreateWeb(Point input1, Point input2)
        {
            Beam web = new Beam();
            web.StartPoint = input1.OffsetZ(- outerFlangeThick);
            web.EndPoint = input2.OffsetZ(- outerFlangeThick);

            double webHeight = 0;

            if (H1 > H2)
            {
                webHeight = H1;
            }
            else 
            {
                webHeight = H2;
            }

            web.Profile.ProfileString = "PL" + webThick + "*" + (webHeight - outerFlangeThick - innerFlangeThick); 
            web.PartNumber.Prefix = webPrefix;
            web.PartNumber.StartNumber = webStartNum;
            web.AssemblyNumber.Prefix = webASPrefix;
            web.AssemblyNumber.StartNumber = webASStartNum;
            web.Material.MaterialString = webMaterial;

            web.Position.Rotation = Position.RotationEnum.TOP;
            web.Position.Depth = Position.DepthEnum.BEHIND;
            web.Position.Plane = Position.PlaneEnum.MIDDLE;

            web.Insert();

            return web;
        }

        private Beam CreateBottomFlange(Point input1, Point input2)
        {
            Beam bottomFlange = new Beam();
            bottomFlange.StartPoint = input1.OffsetZ(- H1 + innerFlangeThick);
            bottomFlange.EndPoint = input2.OffsetZ(- H2 + innerFlangeThick);
           
            bottomFlange.Profile.ProfileString = "PL" + innerFlangeThick + "*" + innerFlangeWidth;
            bottomFlange.PartNumber.Prefix = innerFlangePrefix;
            bottomFlange.PartNumber.StartNumber = innerFlangeStartNum;
            bottomFlange.AssemblyNumber.Prefix = "A";
            bottomFlange.AssemblyNumber.StartNumber = innerFlangeStartNum;
            bottomFlange.Material.MaterialString = innerFlangeMaterial;

            bottomFlange.Position.Rotation = Position.RotationEnum.FRONT;
            bottomFlange.Position.Depth = Position.DepthEnum.BEHIND;
            bottomFlange.Position.Plane = Position.PlaneEnum.MIDDLE;

            bottomFlange.Insert();

            return bottomFlange;
        }

        private void WeldToAssembly(Beam web, Beam topFlange, Beam bottomFlange)
        {
            Weld webToTopFlange = new Weld();
            webToTopFlange.MainObject = web;
            webToTopFlange.SecondaryObject = topFlange;
            webToTopFlange.ShopWeld = true; //han tai nha may
            webToTopFlange.Insert();

            Weld webToBotFlange = new Weld();
            webToBotFlange.MainObject = web;
            webToBotFlange.SecondaryObject = bottomFlange;
            webToBotFlange.ShopWeld = true;
            webToBotFlange.Insert();
        }

        private void CreateCutting(Beam web, Beam bottomFlange, Point p1)
        {
            CutPlane cuttingPlane = new CutPlane();

            Plane plane = new Plane();
            plane.Origin = p1.OffsetZ( - (H1 - innerFlangeThick));
            plane.AxisX = bottomFlange.GetCoordinateSystem().AxisX;
            plane.AxisY = bottomFlange.GetCoordinateSystem().AxisY * -1;

            cuttingPlane.Father = web;
            cuttingPlane.Plane = plane;
            cuttingPlane.Insert();
        }

        private void GetValuesFromDialog()
        {
            H1 = Data.H1;
            if (IsDefaultValue(H1))
            {
                H1 = 400;
            }

            H2 = Data.H2;
            if (IsDefaultValue(H2))
            {
                H2 = 400; // TODO: chỉnh giá trị mặc định thực tế
            }

            webThick = Data.webThick;
            if (IsDefaultValue(webThick))
            {
                webThick = 8; // TODO
            }

            outerFlangeThick = Data.outerFlangeThick;
            if (IsDefaultValue(outerFlangeThick))
            {
                outerFlangeThick = 12; // TODO
            }

            innerFlangeThick = Data.innerFlangeThick;
            if (IsDefaultValue(innerFlangeThick))
            {
                innerFlangeThick = 12; // TODO
            }

            outerFlangeWidth = Data.outerFlangeWidth;
            if (IsDefaultValue(outerFlangeWidth))
            {
                outerFlangeWidth = 184; // TODO
            }

            innerFlangeWidth = Data.innerFlangeWidth;
            if (IsDefaultValue(innerFlangeWidth))
            {
                innerFlangeWidth = 184; // TODO
            }

            webPrefix = Data.webPrefix;
            if (IsDefaultValue(webPrefix))
            {
                webPrefix = "W"; // TODO
            }

            webASPrefix = Data.webASPrefix;
            if (IsDefaultValue(webASPrefix))
            {
                webASPrefix = "BEAM"; // TODO
            }

            outerFlangePrefix = Data.outerFlangePrefix;
            if (IsDefaultValue(outerFlangePrefix))
            {
                outerFlangePrefix = "F"; // TODO
            }

            innerFlangePrefix = Data.innerFlangePrefix;
            if (IsDefaultValue(innerFlangePrefix))
            {
                innerFlangePrefix = "F"; // TODO
            }

            webStartNum = Data.webStartNum;
            if (IsDefaultValue(webStartNum))
            {
                webStartNum = 1;
            }

            webASStartNum = Data.webASStartNum;
            if (IsDefaultValue(webASStartNum))
            {
                webASStartNum = 1;
            }

            innerFlangeStartNum = Data.innerFlangeStartNum;
            if (IsDefaultValue(innerFlangeStartNum))
            {
                innerFlangeStartNum = 1;
            }

            outerFlangeStartNum = Data.outerFlangeStartNum;
            if (IsDefaultValue(outerFlangeStartNum))
            {
                outerFlangeStartNum = 1;
            }

            webMaterial = Data.webMaterial;
            if (IsDefaultValue(webMaterial))
            {
                webMaterial = "S235JR"; // TODO
            }

            outerFlangeMaterial = Data.outerFlangeMaterial;
            if (IsDefaultValue(outerFlangeMaterial))
            {
                outerFlangeMaterial = "S235JR"; // TODO
            }

            innerFlangeMaterial = Data.innerFlangeMaterial;
            if (IsDefaultValue(innerFlangeMaterial))
            {
                innerFlangeMaterial = "S235JR"; // TODO
            }
        }
    }
}
