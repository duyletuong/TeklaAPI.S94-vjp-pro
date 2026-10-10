using Tekla.Structures.Model;

namespace DLT.Plugin.Core.PartCreation
{
    public struct PartProperties
    {
        public NumberingSeries PartNumber { get; set; }
        public NumberingSeries AssemblyNumber { get; set; }
        public string Profile { get; set; }
        public string Material { get; set; }
        public string Class { get; set; }
        public string Name { get; set; }
        public string Finish { get; set; }
        public Position Position { get; set; }

        public PartProperties(NumberingSeries partNumber, NumberingSeries assemblyNumber, string profile, string material, string _class, string name, string finish, Position position)
        {
            PartNumber = partNumber;
            AssemblyNumber = assemblyNumber;
            Profile = profile;
            Material = material;
            Class = _class;
            Name = name;
            Finish = finish;
            Position = position;
        }
    }
}
