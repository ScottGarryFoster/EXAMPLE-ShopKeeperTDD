namespace CommandManager
{
    /// <summary>
    /// Interprets commands into computer read information.
    /// </summary>
    public interface ICommandInterpreter
    {
        /// <summary>
        /// Attempts to interpret the given text and extract whole numbers.
        /// </summary>
        /// <param name="input">Inputs text to interpret. </param>
        /// <param name="value">Value found if any. <c>-1 if none found.</c> </param>
        /// <returns>True means extraction successful. </returns>
        bool InterpretWholeNumber(string input, out int value);
    }
}