using Tekla.Structures.Model;

namespace DLT.Plugin.Core.WeldCreator
{
    public class WeldCreator
    {
        private WeldProperties properties;

        public WeldCreator(WeldProperties properties)
        {
            this.properties = properties;
        }

        public Weld CreateWeld()
        {
            Weld weld = new Weld();
            weld.MainObject = this.properties.MainObject;
            weld.SecondaryObject = this.properties.SecondaryObject;
            weld.ShopWeld = true;

            if (weld.Insert())
            {
                return weld;
            }

            return null;
        }
    }
}
