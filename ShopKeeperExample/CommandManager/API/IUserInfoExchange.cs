namespace CommandManager
{
    /// <summary>
    /// Takes input from the User or Manages such from the User.
    /// </summary>
    public interface IUserInfoExchange
    {
        /// <summary>
        /// Writes text to the screen.
        /// </summary>
        /// <param name="text">Text to write. </param>
        void WriteText(string text);

        /// <summary>
        /// Reads text from the screen.
        /// </summary>
        /// <returns>Reads text and returns the result. </returns>
        string ReadText();
    }
}