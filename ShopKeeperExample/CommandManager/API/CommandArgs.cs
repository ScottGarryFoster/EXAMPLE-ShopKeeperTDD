using System;

namespace CommandManager
{
    /// <summary>
    /// Event arguments for commands in particular.
    /// </summary>
    public class CommandArgs : EventArgs
    {
        /// <summary>
        /// All the command strings including the values.
        /// </summary>
        public string[] CommandStrings;
        
        /// <summary>
        /// Values from the command.
        /// </summary>
        public object[] CommandValues;

        public CommandArgs()
        {
            CommandStrings = Array.Empty<string>();
            CommandValues = Array.Empty<object>();
        }
    }
}