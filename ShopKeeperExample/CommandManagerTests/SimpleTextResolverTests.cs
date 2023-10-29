using System;
using CommandManager;
using NUnit.Framework;

namespace CommandManagerTests
{
    public class SimpleTextResolverTests
    {
        [Test]
        public void OnConstruction_ArgumentNullIsThrown_IfNullIsGivenTest()
        {
            // Arrange
            string given = null;
            
            // Act
            bool didThrow = false;
            try
            {
                new SimpleTextResolver(given);
            }
            catch (ArgumentNullException)
            {
                didThrow = true;
            }

            // Assert
            Assert.IsTrue(didThrow);
        }
        
        [Test]
        public void OnConstruction_ArgumentNullIsThrown_IfWhiteSpaceIsGivenTest()
        {
            // Arrange
            string given = "  ";
            
            // Act
            bool didThrow = false;
            try
            {
                new SimpleTextResolver(given);
            }
            catch (ArgumentNullException)
            {
                didThrow = true;
            }

            // Assert
            Assert.IsTrue(didThrow);
        }
        
        [Test]
        public void IsValid_ReturnsTrue_WhenGivenValueAtConstructionIsTheSameOnValidTest()
        {
            // Arrange
            string given = "october";
            ISingleTextResolver testClass = new SimpleTextResolver(given);
            
            // Act
            bool actual = testClass.IsValid(given);

            // Assert
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void IsValid_ReturnsFalse_WhenGivenValueAtConstructionIsNotTheSameOnValidTest()
        {
            // Arrange
            string constructed = "october";
            string given = "november";
            Assert.AreNotEqual(constructed, given);
            
            ISingleTextResolver testClass = new SimpleTextResolver(constructed);
            
            // Act
            bool actual = testClass.IsValid(given);

            // Assert
            Assert.IsFalse(actual);
        }
        
        [Test]
        public void IsValid_ReturnsTrue_WhenConstructedAndValidValueAreOnlyNotEqualByCaseTest()
        {
            // Arrange
            string constructed = "oCtObEr";
            string given = "OcToBeR";
            Assert.AreNotEqual(constructed, given);
            Assert.AreEqual(constructed.ToLower(), given.ToLower());
            
            ISingleTextResolver testClass = new SimpleTextResolver(constructed);
            
            // Act
            bool actual = testClass.IsValid(given);

            // Assert
            Assert.IsTrue(actual);
        }
    }
}