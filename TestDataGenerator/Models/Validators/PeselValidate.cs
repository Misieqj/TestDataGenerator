using TestDataGenerator.Models.Data;

namespace TestDataGenerator.Models.Validators
{
    public static class PeselValidate
    {
        public static bool IsCorrect(string pesel)
        {
            var peselChecksum = int.Parse(pesel.Substring(PeselChecksum.Index,1));
            var calculatedChecksum = PeselChecksum.Calculate(pesel.Substring(0,PeselChecksum.Index));

            return peselChecksum.Equals(calculatedChecksum);
        }
    }
}
