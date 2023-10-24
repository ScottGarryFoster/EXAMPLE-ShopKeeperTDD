using System;

namespace CommandManager
{
    /// <summary>
    /// Handles a command firing.
    /// </summary>
    public delegate EventHandler RunableCommandCallEvent(Object sender, CommandArgs args);
        
    /// <summary>
    /// A command which is run-able.
    /// </summary>
    public interface IRunableCommand
    {
        /// <summary>
        /// If <see cref="IsValueStage"/> is false then this is the resolver for this entry.
        /// </summary>
        ISingleTextResolver Entry { get; }
        
        /// <summary>
        /// True means this is at the Value stage and the current entry needs to be resolved as a value.
        /// </summary>
        bool IsValueStage { get; }
        
        /// <summary>
        /// The next command to run to make this valid.
        /// </summary>
        IRunableCommand NextCommand { get; }

        /// <summary>
        /// Method to run when this is successful.
        /// </summary>
        RunableCommandCallEvent Runable { get; set; }
    }
}