using System;
using TestDataGenerator.Models.Data;
using TestDataGenerator.Models.Data.Enums;

namespace TestDataGenerator.Models.Generators
{
    public class Pesel
    {
        private readonly Random _random;
        private readonly Gender _gender;
        private string _pesel;

        public Pesel(Gender gender = Gender.None)
        {
            _random = new Random();
            _gender = gender;
            Generate_1_DateFromRange();
        }

        public Pesel SetAge(int age)
        {
            Generate_1_DateFromRange(age, age);
            
            return this;
        }

        public Pesel SetAgeRange(int age1, int age2)
        {
            Generate_1_DateFromRange(age1, age2);
            
            return this;
        }

        public Pesel SetAgeFromBday(DateTime birthdate)
        {
            Generate_1_DateFromBday(birthdate);
            
            return this;
        }
        
        
        public string Generate()
        {
            Generate_2_Random();
            Generate_3_Gender();
            Generate_4_Checksum();
            
            return _pesel;
        }

        private void Generate_1_DateFromRange(int minYear = 0, int maxYear = 77)
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

        private void Generate_1_DateFromBday(DateTime birthdate)
        {
            var month = birthdate.Month;
            if (birthdate.Year >= 2000)
                month += 20;

            _pesel = birthdate.ToString("yy")
                     +month.ToString("D2")
                     +birthdate.ToString("dd");
        }

        private void Generate_2_Random()
            => _pesel += _random.Next(1000).ToString("D3");

        private void Generate_3_Gender()
        {
            switch (_gender)
            {
                case Gender.Male:
                    _pesel += (_random.Next(5)*2+1).ToString("D1");
                    break;
                case Gender.Female:
                    _pesel += (_random.Next(5)*2).ToString("D1");
                    break;
                default:
                    _pesel += _random.Next(10).ToString("D1");
                    break;
            }
        }

        private void Generate_4_Checksum()
            => _pesel += PeselChecksum.Calculate(_pesel).ToString();
    }
}
