using CommandManager;
using Moq;
using NUnit.Framework;

namespace CommandManagerTests
{
    public class CommandResolverTests
    {
        private ICommandResolver testClass;
        private ISingleTextResolver validResolver;
        
        [SetUp]
        public void Setup()
        {
            this.validResolver = new Mock<ISingleTextResolver>().Object;
            this.testClass = new CommandResolver();
        }
        
        
        [Test]
        public void GiveCommand_ReturnsTrue_WhenCommandHasAnEntryTest()
        {
            var given = new RunableCommand { Entry = this.validResolver };

            bool actual = this.testClass.GiveCommand(given);
            
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void GiveCommand_ReturnsFalse_WhenCommandIsBlankTest()
        {
            var given = new RunableCommand();

            bool actual = this.testClass.GiveCommand(given);
            
            Assert.IsFalse(actual);
        }
        
        [Test]
        public void GiveCommand_ReturnsTrue_WhenCommandIsValueStageIsTrueTest()
        {
            var given = new RunableCommand { IsValueStage = true };

            bool actual = this.testClass.GiveCommand(given);
            
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void GiveCommand_ReturnsFalse_WhenRecursiveCommandIsEmptyTest()
        {
            var empty = new RunableCommand();
            var given = new RunableCommand
            {
                Entry = this.validResolver,
                NextCommand = empty
            };

            bool actual = this.testClass.GiveCommand(given);
            
            Assert.IsFalse(actual);
        }
        
        [Test]
        public void GiveCommand_ReturnsTrue_WhenRecursiveCommandHasAnEntryTest()
        {
            var inner = new RunableCommand {Entry = this.validResolver};
            var given = new RunableCommand
            {
                Entry = this.validResolver,
                NextCommand = inner
            };

            bool actual = this.testClass.GiveCommand(given);
            
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void GiveCommand_ReturnsTrue_WhenRecursiveCommandIsAValueTest()
        {
            var inner = new RunableCommand {IsValueStage = true};
            var given = new RunableCommand
            {
                Entry = this.validResolver,
                NextCommand = inner
            };

            bool actual = this.testClass.GiveCommand(given);
            
            Assert.IsTrue(actual);
        }
        
        [Test, Timeout(1000)]
        public void GiveCommand_DoesNotCrash_WhenCommandInNextIsRecursiveTest()
        {
            var given = new RunableCommand
            {
                Entry = this.validResolver
            };
            given.NextCommand = given;

            bool actual = this.testClass.GiveCommand(given);
            
            Assert.IsFalse(actual);
        }
    }
}