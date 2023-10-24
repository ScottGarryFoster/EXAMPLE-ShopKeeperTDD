using System;
using CommandManager;
using ItemManager;
using NUnit.Framework;

namespace CommandManagerTests
{
    public class BasicCommandInterpreterTests
    {
        private ICommandInterpreter testClass;
        
        private const string ValidNumberAsString = "421";
        private const int ValidNumberAsNumber = 421;

        private const string ValidItemAsString = "Armour";
        private const string ValidItemAsStringInAllCaps = "ARMOUR";
        private const Item ValidItemAsEnum = Item.Armour;
        
        [SetUp]
        public void Setup()
        {
            Assert.AreNotEqual(ValidNumberAsNumber, -1, $"{nameof(ValidNumberAsNumber)} is -1 which is also the" +
                                                        " value for 'not valid' meaning tests will become " +
                                                        "inconclusive with this value. " +
                                                        "Please change it to something else.");
            
            Assert.AreNotEqual(ValidItemAsEnum, (Item)0, $"{nameof(ValidItemAsEnum)} is equal to the 0th Enum value." +
                                                   "This will mean tests will be inconclusive. " +
                                                   "Select a different enum value as the test.");
            
            this.testClass = new BasicCommandInterpreter();
        }
        
        #region InterpreteWholeNumber
        
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
        
        #endregion
        
        #region InterpretItem
        
        [Test]
        public void InterpretItem_ReturnsFalse_WhenGivenIsNullTest()
        {
            string given = null;

            bool actual = this.testClass.InterpretItem(given, out _);
            
            Assert.IsFalse(actual);
        }
        
        [Test]
        public void InterpretItem_ReturnsFalse_WhenGivenIsEmptyTest()
        {
            string given = String.Empty;

            bool actual = this.testClass.InterpretItem(given, out _);
            
            Assert.IsFalse(actual);
        }
        
        [Test]
        public void InterpretItem_ReturnsTrue_WhenGivenIsAValidItemTest()
        {
            string given = ValidItemAsString;

            bool actual = this.testClass.InterpretItem(given, out _);
            
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void InterpretItem_ReturnsTrue_WhenGivenIsAValidItemAndThereIsWhitespaceTest()
        {
            string given = $"   {ValidItemAsString}  ";

            bool actual = this.testClass.InterpretItem(given, out _);
            
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void InterpretItem_ReturnsCorrectItem_WhenGivenIsAValidItemTest()
        {
            this.testClass.InterpretItem(ValidItemAsString, out Item actual);
            
            Assert.AreEqual(ValidItemAsEnum, actual);
        }
        
        [Test]
        public void InterpretItem_ReturnsCorrectItem_WhenGivenIsAValidItemAndThereIsWhitespaceTest()
        {
            string given = $"   {ValidItemAsString}  ";

            this.testClass.InterpretItem(given, out Item actual);
            
            Assert.AreEqual(ValidItemAsEnum, actual);
        }
        
        [Test]
        public void InterpretItem_ReturnsCorrectItem_WhenGivenIsInAllCapsAndIsAnItemTest()
        {
            this.testClass.InterpretItem(ValidItemAsStringInAllCaps, out Item actual);
            
            Assert.AreEqual(ValidItemAsEnum, actual);
        }
        
        #endregion
    }
}