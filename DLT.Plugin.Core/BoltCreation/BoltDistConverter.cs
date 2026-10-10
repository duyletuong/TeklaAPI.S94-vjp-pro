using System;
using System.Collections.Generic;
using System.Linq;

namespace DLT.Plugin.Core.BoltCreation
{
    public class BoltDistConverter
    {
        private readonly int numOfBolt;
        private readonly string distanceList;

        public BoltDistConverter(int numOfBolt, string distList)
        {
            this.numOfBolt = numOfBolt;
            this.distanceList = distList;
        }

        public List<double> GetBoltDist()
        {
            if (numOfBolt == 0) return new List<double> { 0 };

            return ConvertToBoltDistList(this.distanceList);
        }

        private List<double> ConvertToBoltDistList(string input)
        {
            var doubleList = new List<double>();

            if (string.IsNullOrWhiteSpace(input))
                return doubleList;

            foreach (var part in input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (double.TryParse(part, out double value))
                {
                    doubleList.Add(value);
                }
            }

            return doubleList.Count > 0 ? ToBoltDist(this.numOfBolt, doubleList) : new List<double> { 0 };
        }

        private static List<double> ToBoltDist(int num, List<double> doubleList)
        {
            if (num <= 1)
                return new List<double> { 0 };

            if (doubleList == null || doubleList.Count == 0)
                return new List<double> { 0 };

            int needed = num - 1;
            var result = new List<double>();

            if (doubleList.Count >= needed)
            {
                result = doubleList.Take(needed).ToList();
            }
            else
            {
                result.AddRange(doubleList);
                while (result.Count < needed)
                {
                    result.Add(doubleList.Last());
                }
            }

            return result;
        }
    }
}
