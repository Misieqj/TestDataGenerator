using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDataGenerator.Models
{
    public class Pesel
    {
        private const int ChecksumIndex = 10;
        private readonly List<int> _digitMultiplier = new List<int>() {1, 3, 7, 9, 1, 3, 7, 9, 1, 3, 0};
        private string _pesel;
        private readonly Random _random;

        public Pesel()
        {
            _random = new Random();
        }
        
        public string Generate(bool? isMan = null)
        {
            _pesel = string.Empty;
            
            Generate_1Date();
            Generate_2Supplement();
            Generate_3Sex(isMan);
            Generate_4Checksum();
            
            return _pesel;
        }

        public string GenerateFromDate(DateTime birthdate, bool? isMan = null)
        {
            _pesel = string.Empty;
            
            Generate_1DateValue(birthdate);
            Generate_2Supplement();
            Generate_3Sex(isMan);
            Generate_4Checksum();
            
            return _pesel;
        }

        public string GenerateFromAge(int age, bool? isMan = null)
        {
            _pesel = string.Empty;
            
            Generate_1Date(age, age);
            Generate_2Supplement();
            Generate_3Sex(isMan);
            Generate_4Checksum();
            
            return _pesel;
        }

        public string GenerateFromAgeRange(int minAge, int maxAge, bool? isMan = null)
        {
            _pesel = string.Empty;
            
            Generate_1Date(minAge, maxAge);
            Generate_2Supplement();
            Generate_3Sex(isMan);
            Generate_4Checksum();
            
            return _pesel;
        }

        public bool Validate(string pesel)
        {
            var peselChecksum = int.Parse(pesel.Substring(ChecksumIndex,1));
            var calculatedChecksum = Checksum(pesel.Substring(0,ChecksumIndex));

            return peselChecksum.Equals(calculatedChecksum);
        }

        private void Generate_1Date(int minYear = 0, int maxYear = 77)
        {
            var randomYear = _random.Next(minYear, maxYear);
            var randomMonth = _random.Next(13);
            var randomDay = _random.Next(32);

            _pesel = DateTime.Today
                .AddYears(-randomYear)
                .AddMonths(-randomMonth)
                .AddDays(-randomDay)
                .ToString("yyMMdd");
        }

        private void Generate_1DateValue(DateTime birthdate)
        {
            var month = birthdate.Month;
            if (birthdate.Year >= 2000)
                month += 20;

            _pesel = birthdate.ToString("yy")
                     +month.ToString("D2")
                     +birthdate.ToString("dd");
        }

        private void Generate_2Supplement()
        {
            _pesel += _random.Next(1000).ToString("D3");
        }

        private void Generate_3Sex(bool? isMan)
        {
            switch (isMan)
            {
                case true:
                    _pesel += _random.Next(10).ToString("D1");
                    break;
                case false:
                    _pesel += (_random.Next(5)*2+1).ToString("D1");
                    break;
                default:
                    _pesel += (_random.Next(5)*2).ToString("D1");
                    break;
            }
        }

        private void Generate_4Checksum()
        {
            var checksum = Checksum(_pesel);

            _pesel += checksum.ToString();
        }

        private int Checksum(string pesel)
        {
            var intList = pesel.Select(x => int.Parse(x.ToString())).ToList();
            var checksum = intList.Select((t, i) => t * _digitMultiplier[i]).Sum();

            checksum %= 10;
            if (checksum > 0)
                checksum = 10 - checksum;

            return checksum;
        }
    }
}
