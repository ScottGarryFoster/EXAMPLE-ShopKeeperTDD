using System;

namespace CommandManager
{
    /// <summary>
    /// Logic for the Shop Keeping game
    /// </summary>
    public class ShopGameLoop : IGameLoop
    {
        /// <summary>
        /// Takes input from the User or Manages such from the User.
        /// </summary>
        private readonly IUserInfoExchange userExchange;
        
        public ShopGameLoop(IUserInfoExchange exchange)
        {
            this.userExchange = exchange ??
                                throw new ArgumentNullException(
                                    $"{typeof(ShopGameLoop)}: " +
                                    $"{nameof(exchange)} must not be null.");
        }
        
        /// <summary>
        /// Runs the game.
        /// </summary>
        public void RunGame()
        {
            this.userExchange.WriteText("Loading");
            IRunableCommand testCommandValue = new RunableCommand()
            {
                IsValueStage = true,
                Runable = TestMethod,
            };
            
            IRunableCommand testCommand = new RunableCommand()
            {
                Entry = new SimpleTextResolver("testcommand"),
                NextCommand = testCommandValue
            };

            ICommandResolver resolver = new CommandResolver();
            resolver.GiveCommand(testCommand);
            
            while (true)
            {
                this.userExchange.WriteText("What is your command");
                string command = this.userExchange.ReadText();
                if (!resolver.ResolveCommand(command))
                {
                    this.userExchange.WriteText("Some help message");
                }
            }

        }

        private EventHandler TestMethod(object sender, CommandArgs args)
        {
            foreach (string command in args.CommandStrings)
            {
                this.userExchange.WriteText($"CMD: {command}");
            }
            
            foreach (string command in args.CommandValues)
            {
                this.userExchange.WriteText($"VALUES: {command}");
            }

            return null;
        }
    }
}