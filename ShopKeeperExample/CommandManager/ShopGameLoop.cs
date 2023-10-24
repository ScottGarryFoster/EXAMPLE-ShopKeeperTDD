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
            this.userExchange.WriteText("Test");
            this.userExchange.ReadText();
        }
    }
}