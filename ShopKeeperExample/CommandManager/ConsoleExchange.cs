using System;

namespace CommandManager
{
    /// <summary>
    /// Takes input from the User or Manages such from the User using the console.
    /// </summary>
    public class ConsoleExchange : IUserInfoExchange
    {
        /// <summary>
        /// Writes text to the screen.
        /// </summary>
        /// <param name="text">Text to write. </param>
        public void WriteText(string text)
        {
            Console.WriteLine(text);
        }

        /// <summary>
        /// Reads text from the screen.
        /// </summary>
        /// <returns>Reads text and returns the result. </returns>
        public string ReadText()
        {
            return Console.ReadLine();
        }
    }
}