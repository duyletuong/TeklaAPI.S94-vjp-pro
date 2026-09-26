using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Plugins;

namespace TeklaAPI.S94
{
    public class StructuresData
    {
        [StructuresField("H1")]
        public double H1;

        [StructuresField("H2")]
        public double H2;

        [StructuresField("webThick")]
        public double webThick;

        [StructuresField("outerFlangeThick")]
        public double outerFlangeThick;

        [StructuresField("innerFlangeThick")]
        public double innerFlangeThick;

        [StructuresField("outerFlangeWidth")]
        public double outerFlangeWidth;

        [StructuresField("innerFlangeWidth")]
        public double innerFlangeWidth;

        [StructuresField("webPrefix")]
        public string webPrefix;

        [StructuresField("webASPrefix")]
        public string webASPrefix;

        [StructuresField("outerFlangePrefix")]
        public string outerFlangePrefix;

        [StructuresField("innerFlangePrefix")]
        public string innerFlangePrefix;

        [StructuresField("webStartNum")]
        public int webStartNum;

        [StructuresField("webASStartNum")]
        public int webASStartNum;

        [StructuresField("innerFlangeStartNum")]
        public int innerFlangeStartNum;

        [StructuresField("outerFlangeStartNum")]
        public int outerFlangeStartNum;

        [StructuresField("webMaterial")]
        public string webMaterial;

        [StructuresField("outerFlangeMaterial")]
        public string outerFlangeMaterial;

        [StructuresField("innerFlangeMaterial")]
        public string innerFlangeMaterial;
    }
}
