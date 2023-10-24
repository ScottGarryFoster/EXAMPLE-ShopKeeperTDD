using System;
using CommandManager;
using NUnit.Framework;

namespace CommandManagerTests
{
    [TestFixture]
    public class ShopGameLoopTests
    {
        [Test]
        public void OnConstruction_ThrowsArgumentException_WhenExchangeIsNullTest()
        {
            IUserInfoExchange given = null;

            bool thrown = false;
            try
            {
                new ShopGameLoop(given);
            }
            catch (ArgumentNullException)
            {
                thrown = true;
            }
            
            Assert.IsTrue(thrown, "Exception not thrown.");
        }
    }
}