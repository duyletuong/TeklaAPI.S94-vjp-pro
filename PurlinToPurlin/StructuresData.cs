using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLT.Plugin.Core.Dialog;
using Tekla.Structures.Plugins;

namespace PurlinToPurlin
{
    public class StructuresData : ConnectionDataBase
    {
        [StructuresField("gap"), Default(10.0)]
        public double gap;

        [StructuresField("fitPos"), Default(0)]
        public int fittingPosition;

        [StructuresField("cuttingType"), Default(0)]
        public int fittingType;
    }
}
