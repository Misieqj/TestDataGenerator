using System;
using NUnit.Framework;
using TestDataGenerator.Models;

namespace TestDataGenerator.Tests
{
    [TestFixture]
    public class PeselTests
    {
        private Pesel _pesel;
        private string _result;
        private bool _isCorrect;
        
        [SetUp]
        public void Initialize()
        {
            _pesel = new Pesel();
        }
        
        
        [Test]
        public void PeselGenerationTest()
        {
            Console.WriteLine("Test generowania numeru pesel");

            _result = _pesel.Generate();
            PrintResults("Generowanie bez parametrów", _result);

            _result = _pesel.GenerateFromDate(new DateTime(1984, 03, 05));
            PrintResults("Generowanie dla konkretnej daty", _result);
            
            _result = _pesel.GenerateFromAge(20);
            PrintResults("Generowanie dla konkretnego wieku", _result);

            _result = _pesel.GenerateFromAgeRange(20, 25);
            PrintResults("Generowanie dla zakresu lat", _result);
        }
    
        [Test]
        public void PeselValidationTests() {
            Console.WriteLine("Test walidacji numeru pesel");

            _isCorrect = _pesel.Validate("18222872772");
            PrintResults("Weryfikacja poprawnego numeru", _isCorrect.ToString());

            _isCorrect = _pesel.Validate("18222872777");
            PrintResults("Weryfikacja błędnego numeru", _isCorrect.ToString());
        }

        private void PrintResults(string title, string result)
        {
            Console.WriteLine($"===> {title}");
            Console.WriteLine($"  -> {result}\r\n");
        }
    }
}
