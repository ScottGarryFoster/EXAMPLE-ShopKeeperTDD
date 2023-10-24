namespace CommandManager
{
    /// <summary>
    /// Resolves commands.
    /// </summary>
    public interface ICommandResolver
    {
        /// <summary>
        /// Resolves the given command and runs the output if any.
        /// </summary>
        /// <param name="command">Command to resolve. </param>
        /// <returns>True means command found. </returns>
        bool ResolveCommand(string command);

        /// <summary>
        /// Gives the resolver a new command.
        /// </summary>
        /// <param name="command">Command to give. </param>
        /// <returns>True means the command was valid. </returns>
        bool GiveCommand(IRunableCommand command);
    }
}