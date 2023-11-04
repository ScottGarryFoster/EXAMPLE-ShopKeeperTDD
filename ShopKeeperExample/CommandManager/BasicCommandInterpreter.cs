using System;
using GameElements;

namespace CommandManager
{
    /// <summary>
    /// Interprets commands but does not attempt to do anything too fancy in terms of language extraction.
    /// </summary>
    public class BasicCommandInterpreter : ICommandInterpreter
    {
        /// <summary>
        /// Attempts to interpret the given text and extract whole numbers.
        /// </summary>
        /// <param name="input">Inputs text to interpret. </param>
        /// <param name="value">Value found if any. <c>-1 if none found.</c> </param>
        /// <returns>True means extraction successful. </returns>
        public bool InterpretWholeNumber(string input, out int value)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                value = -1;
                return false;
            }
            
            return int.TryParse(input.Trim(), out value);
        }

        /// <summary>
        /// Attempts to interpret the given text and extract the item.
        /// </summary>
        /// <param name="input">Inputs text to interpret. </param>
        /// <param name="value">Value if any otherwise the default enum value. </param>
        /// <returns>True means could extract information. </returns>
        public bool InterpretItem(string input, out Item value)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                value = 0;
                return false;
            }
            
            return Enum.TryParse(input.Trim(), ignoreCase: true, out value);
        }
    }
}