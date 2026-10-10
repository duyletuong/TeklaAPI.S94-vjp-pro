namespace DLT.Plugin.Core.Extension
{
    public static class DoubleExtension
    {
        public static double Ro(this double value, int digit = 3)
        {
            return System.Math.Round(value, digit);
        }
    }
}
