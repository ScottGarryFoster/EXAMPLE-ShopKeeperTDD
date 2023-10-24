using System;
using CommandManager;
using NUnit.Framework;

namespace CommandManagerTests
{
    public class BasicCommandInterpreterTests
    {
        private ICommandInterpreter testClass;
        private const string ValidNumberAsString = "421";
        private const int ValidNumberAsNumber = 421;
        
        [SetUp]
        public void Setup()
        {
            this.testClass = new BasicCommandInterpreter();
        }
        
        [Test]
        public void InterpretWholeNumber_ReturnsFalse_WhenGivenIsNullTest()
        {
            string given = null;

            bool actual = this.testClass.InterpretWholeNumber(given, out _);
            
            Assert.IsFalse(actual);
        }
        
        [Test]
        public void InterpretWholeNumber_ReturnsFalse_WhenGivenIsEmptyTest()
        {
            string given = String.Empty;

            bool actual = this.testClass.InterpretWholeNumber(given, out _);
            
            Assert.IsFalse(actual);
        }
        
        [Test]
        public void InterpretWholeNumber_ReturnsTrue_WhenGivenIsAValidNumberTest()
        {
            string given = ValidNumberAsString;

            bool actual = this.testClass.InterpretWholeNumber(given, out _);
            
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void InterpretWholeNumber_ReturnsTrue_WhenGivenIsAValidNumberAndThereIsWhitespaceTest()
        {
            string given = $"   {ValidNumberAsString}  ";

            bool actual = this.testClass.InterpretWholeNumber(given, out _);
            
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void InterpretWholeNumber_ReturnsCorrectNumber_WhenGivenIsAValidNumberTest()
        {
            this.testClass.InterpretWholeNumber(ValidNumberAsString, out int actual);
            
            Assert.AreEqual(ValidNumberAsNumber, actual);
        }
        
        [Test]
        public void InterpretWholeNumber_ReturnsCorrectNumber_WhenGivenIsAValidNumberAndThereIsWhitespaceTest()
        {
            string given = $"   {ValidNumberAsString}  ";

            this.testClass.InterpretWholeNumber(given, out int actual);
            
            Assert.AreEqual(ValidNumberAsNumber, actual);
        }
    }
}