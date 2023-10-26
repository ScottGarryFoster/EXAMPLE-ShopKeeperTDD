﻿using System;
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
        
        #region GiveCommand
        
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
        
        #endregion

        #region ResolveCommand

        [Test]
        public void ResolveCommand_ReturnFalse_WhenGivenEmptyStringTest()
        {
            string given = string.Empty;

            bool actual = this.testClass.ResolveCommand(given);
            
            Assert.IsFalse(actual);
        }

        [Test]
        public void ResolveCommand_ReturnFalse_WhenGivenStringIsNullTest()
        {
            string given = null;

            bool actual = this.testClass.ResolveCommand(given);
            
            Assert.IsFalse(actual);
        }

        [Test]
        public void ResolveCommand_ReturnFalse_WhenCommandGivenButNothingSetupTest()
        {
            string given = "testcommand";

            bool actual = this.testClass.ResolveCommand(given);
            
            Assert.IsFalse(actual);
        }

        [Test]
        public void ResolveCommand_ReturnTrue_WhenCommandGivenAndSetupTest()
        {
            string given = "testcommand";
            
            // Arrange
            var resolver = new Mock<ISingleTextResolver>();
            resolver.Setup(x => x.IsValid(given)).Returns(true);
            IRunableCommand newCommand = new RunableCommand()
            {
                Entry = resolver.Object
            };
            this.testClass.GiveCommand(newCommand);

            // Act
            bool actual = this.testClass.ResolveCommand(given);
            
            // Assert
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void ResolveCommand_ReturnFalse_WhenCommandGivenAndButNotResolvedTest()
        {
            string given = "testcommand";
            
            // Arrange
            var resolver = new Mock<ISingleTextResolver>();
            resolver.Setup(x => x.IsValid(given)).Returns(false);
            IRunableCommand newCommand = new RunableCommand()
            {
                Entry = resolver.Object
            };
            this.testClass.GiveCommand(newCommand);

            // Act
            bool actual = this.testClass.ResolveCommand(given);
            
            // Assert
            Assert.IsFalse(actual);
        }
        
        [Test]
        public void ResolveCommand_ReturnTrue_WhenGivenMultipleCommandAndAskedToResolveOneGivenTest()
        {
            string before = "previouscommand";
            string given = "testcommand";
            string after = "nextcommand";
            
            // Arrange
            this.testClass.GiveCommand(CreateCommand(before));
            this.testClass.GiveCommand(CreateCommand(given));
            this.testClass.GiveCommand(CreateCommand(after));

            // Act
            bool actual = this.testClass.ResolveCommand(given);
            
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ResolveCommand_HandlesCommandsWithoutResolvers_WhenGivenAValidCommandInputTest()
        {
            string given = "testcommand";
            
            // Arrange
            this.testClass.GiveCommand(new RunableCommand());
            this.testClass.GiveCommand(CreateCommand(given));
            this.testClass.GiveCommand(new RunableCommand());

            // Act
            bool actual = this.testClass.ResolveCommand(given);
            
            // Assert
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void ResolveCommand_RunsCommand_WhenItResolvesAndIsTheLastInChainTest()
        {
            string given = "testcommand";
            
            // Arrange
            IRunableCommand newCommand = CreateCommand(given);
            this.testClass.GiveCommand(newCommand);
            
            bool didRun = false;
            newCommand.Runable += (sender, args) =>
            {
                didRun = true;
                return null;
            };

            // Act
            this.testClass.ResolveCommand(given);
            
            // Assert
            Assert.IsTrue(didRun);
        }
        
        [Test]
        public void ResolveCommand_ReturnsTrue_WhenTwoCommandsAreChainedTest()
        {
            string firstGiven = "testcommand";
            string subGiven = "subcommand";
            string given = $"{firstGiven} {subGiven}";
            
            // Arrange
            IRunableCommand subCommand = CreateCommand(subGiven);
            IRunableCommand newCommand = CreateCommandWithSubCommand(firstGiven, subCommand);
            this.testClass.GiveCommand(newCommand);

            // Act
            bool actual = this.testClass.ResolveCommand(given);
            
            // Assert
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void ResolveCommand_RunCommandInSecondDivision_WhenTwoCommandsAreChainedTest()
        {
            string firstGiven = "testcommand";
            string subGiven = "subcommand";
            string given = $"{firstGiven} {subGiven}";
            
            // Arrange
            IRunableCommand subCommand = CreateCommand(subGiven);
            IRunableCommand newCommand = CreateCommandWithSubCommand(firstGiven, subCommand);
            this.testClass.GiveCommand(newCommand);
            
            bool didRun = false;
            subCommand.Runable += (sender, args) =>
            {
                didRun = true;
                return null;
            };

            // Act
            this.testClass.ResolveCommand(given);
            
            // Assert
            Assert.IsTrue(didRun);
        }
        
        [Test]
        public void ResolveCommand_DoesNotRunRunableInFirstCommand_WhenTwoCommandsAreChainedTest()
        {
            string firstGiven = "testcommand";
            string subGiven = "subcommand";
            string given = $"{firstGiven} {subGiven}";
            
            // Arrange
            IRunableCommand subCommand = CreateCommand(subGiven);
            IRunableCommand newCommand = CreateCommandWithSubCommand(firstGiven, subCommand);
            this.testClass.GiveCommand(newCommand);
            
            bool didRun = false;
            newCommand.Runable += (sender, args) =>
            {
                didRun = true;
                return null;
            };

            // Act
            this.testClass.ResolveCommand(given);
            
            // Assert
            Assert.IsFalse(didRun);
        }
        
        [Test]
        public void ResolveCommand_ReturnsFalse_WhenFirstCommandResolvesButSecondDoesNotTest()
        {
            string firstGiven = "testcommand";
            string subGiven = "subcommand";
            string given = $"{firstGiven} {subGiven}";
            
            // Arrange
            var resolver = new Mock<ISingleTextResolver>();
            resolver.Setup(x => x.IsValid(subGiven)).Returns(false);
            IRunableCommand subCommand = new RunableCommand()
            {
                Entry = resolver.Object
            };
            
            IRunableCommand newCommand = CreateCommandWithSubCommand(firstGiven, subCommand);
            this.testClass.GiveCommand(newCommand);
            
            bool didRun = false;
            newCommand.Runable += (sender, args) =>
            {
                didRun = true;
                return null;
            };

            // Act
            bool actual = this.testClass.ResolveCommand(given);
            
            // Assert
            Assert.IsFalse(actual);
        }

        private IRunableCommand CreateCommand(string given)
        {
            var resolver = new Mock<ISingleTextResolver>();
            resolver.Setup(x => x.IsValid(given)).Returns(true);
            IRunableCommand newCommand = new RunableCommand()
            {
                Entry = resolver.Object
            };
            return newCommand;
        }
        
        private IRunableCommand CreateCommandWithSubCommand(string given, IRunableCommand subCommand)
        {
            var resolver = new Mock<ISingleTextResolver>();
            resolver.Setup(x => x.IsValid(given)).Returns(true);
            IRunableCommand newCommand = new RunableCommand()
            {
                Entry = resolver.Object,
                NextCommand = subCommand
            };
            return newCommand;
        }

        #endregion
    }
}