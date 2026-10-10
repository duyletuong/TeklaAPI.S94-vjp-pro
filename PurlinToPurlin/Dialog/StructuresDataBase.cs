using Tekla.Structures.Plugins;

namespace DLT.Plugin.Core.Dialog
{
    /// <summary>
    /// Base for all StructuresData classes. Enables ApplyDefaults().
    /// </summary>
    public abstract class StructuresDataBase { }

    /// <summary>
    /// Base for Connection StructuresData — includes Tekla standard connection fields.
    /// </summary>
    public abstract class ConnectionDataBase : StructuresDataBase
    {
        [StructuresField("zsuunta"), Default(7)]    public int UpDirection;
        [StructuresField("zang1"), Default(0.0)]    public double RotationAngleY;
        [StructuresField("zang2"), Default(0.0)]    public double RotationAngleX;
        [StructuresField("OBJECT_LOCKED"), Default(0)] public int Locked;
        [StructuresField("group_no"), Default(-1)]  public int Class;
        [StructuresField("joint_code")]             public string ConnectionCode;
        [StructuresField("ad_root")]                public string AutoDefaults;
        [StructuresField("ac_root")]                public string AutoConnection;
    }
}
