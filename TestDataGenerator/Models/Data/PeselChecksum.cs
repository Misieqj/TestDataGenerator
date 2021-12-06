using System.Collections.Generic;
using System.Linq;

namespace TestDataGenerator.Models.Data
{
    public static class PeselChecksum
    {
        public const int Index = 10;
        private static readonly List<int> DigitMultiplier = new List<int>() {1, 3, 7, 9, 1, 3, 7, 9, 1, 3, 0};

        public static int Calculate(string pesel)
        {
            var intList = pesel.Select(x => int.Parse(x.ToString())).ToList();
            var checksum = intList.Select((t, i) => t * DigitMultiplier[i]).Sum();

            checksum %= 10;
            if (checksum > 0)
                checksum = 10 - checksum;

            return checksum;
        }
    }
}
