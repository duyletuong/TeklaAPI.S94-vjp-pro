using Tekla.Structures.Model;

namespace DLT.Plugin.Core.WeldCreator
{
    public struct WeldProperties
    {
        public Part MainObject { get; }
        public Part SecondaryObject { get; }

        public WeldProperties(Part mainObject, Part secondaryObject)
        {
            MainObject = mainObject;
            SecondaryObject = secondaryObject;
        }
    }
}
