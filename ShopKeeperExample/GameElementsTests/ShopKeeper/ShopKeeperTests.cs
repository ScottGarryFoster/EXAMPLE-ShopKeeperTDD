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
            
            int actual = this.testClass.Give(ValidItem, givenQuantity);
            
            Assert.AreEqual(givenQuantity, actual);
        }
        
        [Test]
        public void Give_Returns1_When1QuantityGivenTest()
        {
            int givenQuantity = 1;
            
            int actual = this.testClass.Give(ValidItem, givenQuantity);
            
            Assert.AreEqual(givenQuantity, actual);
        }
        
        [Test]
        public void Give_ReturnsSumOfTwoCalls_WhenGiveCalledTwiceWithPositiveValuesTest()
        {
            int givenQuantity = 1;
            int givenQuantitySecond = 3;
            int expected = 4;
            
            this.testClass.Give(ValidItem, givenQuantity);
            int actual = this.testClass.Give(ValidItem, givenQuantitySecond);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Give_Returns1_WhenDifferentItemsGivenWithSingleQuantityTest()
        {
            int givenQuantity = 1;
            int expected = 1;
            
            this.testClass.Give(ValidItem, givenQuantity);
            int actual = this.testClass.Give(ValidOtherItem, givenQuantity);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Give_Returns3_WhenDifferentItemsGivenWithSingleQuantityAndLastItemAddsUpTo3StockTest()
        {
            int givenQuantityFirstItem = 2;
            int givenQuantityOtherItem = 1;
            int givenQuantityFirstItemSecondTime = 1;
            int expected = 3;
            
            this.testClass.Give(ValidItem, givenQuantityFirstItem);
            this.testClass.Give(ValidOtherItem, givenQuantityOtherItem);
            int actual = this.testClass.Give(ValidItem, givenQuantityFirstItemSecondTime);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Give_DecreasesStock_WhenGivenNegativeQuantityTest()
        {
            int givenQuantityFirstItem = 2;
            int givenSecondQuantity = -1;
            int expected = 1;
            
            this.testClass.Give(ValidItem, givenQuantityFirstItem);
            int actual = this.testClass.Give(ValidItem, givenSecondQuantity);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Give_DoesNotDecreaseStockBelowZero_WhenGivenNegativeQuantityBelowZeroTest()
        {
            int givenQuantityFirstItem = 2;
            int givenSecondQuantity = -3;
            int expected = 0;
            
            this.testClass.Give(ValidItem, givenQuantityFirstItem);
            int actual = this.testClass.Give(ValidItem, givenSecondQuantity);
            
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region GetPrice_SetPrice

        [Test]
        public void GetPrice_ReturnsZero_WhenNothingHasBeenSetTest()
        {
            int expected = 0;

            int actual = this.testClass.GetPrice(ValidItem);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void GetPrice_ReturnsSetPrice_WhenPriceIsSetTest()
        {
            int expected = 5;
            this.testClass.SetPrice(ValidItem, expected);

            int actual = this.testClass.GetPrice(ValidItem);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void GetPrice_ReturnsSetPriceForAllItems_WhenPriceIsSetForMultipleItemsTest()
        {
            int expected = 5;
            int expectedSecond = 8;
            this.testClass.SetPrice(ValidItem, expected);
            this.testClass.SetPrice(ValidOtherItem, expectedSecond);

            int actual = this.testClass.GetPrice(ValidItem);
            int actual2 = this.testClass.GetPrice(ValidOtherItem);
            
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedSecond, actual2);
        }
        
        [Test]
        public void GetPrice_DoesNotSetPriceBelowFree_WhenPriceIsSetNegativeTest()
        {
            int expected = 0;
            int given = -1;
            this.testClass.SetPrice(ValidItem, given);

            int actual = this.testClass.GetPrice(ValidItem);
            
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Buy
        
        #endregion
    }
}