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
        private const Item ValidOtherItem = Item.Armour;
        private const int ValidPrice = 0;
        private const int ValidQuantity = 0;

        [SetUp]
        public void Setup()
        {
            this.testClass = new ShopKeeper();
            
            Assert.AreNotEqual(ValidItem, ValidOtherItem);
        }

        #region Give_Quantity

        [Test]
        public void Give_Returns0_When0QuantityGivenTest()
        {
            int givenQuantity = 0;
            
            int actual = testClass.Give(ValidItem, givenQuantity);
            
            Assert.AreEqual(givenQuantity, actual);
        }
        
        [Test]
        public void Give_Returns1_When1QuantityGivenTest()
        {
            int givenQuantity = 1;
            
            int actual = testClass.Give(ValidItem, givenQuantity);
            
            Assert.AreEqual(givenQuantity, actual);
        }
        
        [Test]
        public void Give_ReturnsSumOfTwoCalls_WhenGiveCalledTwiceWithPositiveValuesTest()
        {
            int givenQuantity = 1;
            int givenQuantitySecond = 3;
            int expected = 4;
            
            testClass.Give(ValidItem, givenQuantity);
            int actual = testClass.Give(ValidItem, givenQuantitySecond);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Give_Returns1_WhenDifferentItemsGivenWithSingleQuantityTest()
        {
            int givenQuantity = 1;
            int expected = 1;
            
            testClass.Give(ValidItem, givenQuantity);
            int actual = testClass.Give(ValidOtherItem, givenQuantity);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Give_Returns3_WhenDifferentItemsGivenWithSingleQuantityAndLastItemAddsUpTo3StockTest()
        {
            int givenQuantityFirstItem = 2;
            int givenQuantityOtherItem = 1;
            int givenQuantityFirstItemSecondTime = 1;
            int expected = 3;
            
            testClass.Give(ValidItem, givenQuantityFirstItem);
            testClass.Give(ValidOtherItem, givenQuantityOtherItem);
            int actual = testClass.Give(ValidItem, givenQuantityFirstItemSecondTime);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Give_DecreasesStock_WhenGivenNegativeQuantityTest()
        {
            int givenQuantityFirstItem = 2;
            int givenSecondQuantity = -1;
            int expected = 1;
            
            testClass.Give(ValidItem, givenQuantityFirstItem);
            int actual = testClass.Give(ValidItem, givenSecondQuantity);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Give_DoesNotDecreaseStockBelowZero_WhenGivenNegativeQuantityBelowZeroTest()
        {
            int givenQuantityFirstItem = 2;
            int givenSecondQuantity = -3;
            int expected = 0;
            
            testClass.Give(ValidItem, givenQuantityFirstItem);
            int actual = testClass.Give(ValidItem, givenSecondQuantity);
            
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}