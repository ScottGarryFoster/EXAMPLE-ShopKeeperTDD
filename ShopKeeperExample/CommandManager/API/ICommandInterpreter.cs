using ItemManager;

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

        /// <summary>
        /// Attempts to interpret the given text and extract the item.
        /// </summary>
        /// <param name="input">Inputs text to interpret. </param>
        /// <param name="value">Value if any otherwise the default enum value. </param>
        /// <returns>True means could extract information. </returns>
        bool InterpretItem(string input, out Item value);
    }
}