using System;
using GameElements;
using NUnit.Framework;

namespace GameElementsTests
{
    [TestFixture]
    public class ShopKeeperTests
    {
        private IShopKeeper testClass;
        
        private const Item ValidItem = Item.Shield;
        private const int ValidPrice = 0;

        [SetUp]
        public void Setup()
        {
            this.testClass = new ShopKeeper();
        }
        
        [Test]
        public void Give_Returns0_When0QuantityGivenTest()
        {
            int givenQuantity = 0;
            
            int actual = testClass.Give(ValidItem, givenQuantity, ValidPrice);
            
            Assert.AreEqual(givenQuantity, actual);
        }
        
        [Test]
        public void Give_Returns1_When1QuantityGivenTest()
        {
            int givenQuantity = 1;
            
            int actual = testClass.Give(ValidItem, givenQuantity, ValidPrice);
            
            Assert.AreEqual(givenQuantity, actual);
        }
        
        [Test]
        public void Give_ReturnsSumOfTwoCalls_WhenGiveCalledTwiceWithPositiveValuesTest()
        {
            int givenQuantity = 1;
            int givenQuantitySecond = 3;
            int expected = 4;
            
            testClass.Give(ValidItem, givenQuantity, ValidPrice);
            int actual = testClass.Give(ValidItem, givenQuantitySecond, ValidPrice);
            
            Assert.AreEqual(expected, actual);
        }
    }
}