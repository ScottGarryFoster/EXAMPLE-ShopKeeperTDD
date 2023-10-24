namespace CommandManager
{
    /// <summary>
    /// A command which is run-able.
    /// </summary>
    public class RunableCommand : IRunableCommand
    {
        /// <summary>
        /// If <see cref="IsValueStage"/> is false then this is the resolver for this entry.
        /// </summary>
        public ISingleTextResolver Entry { get; set; }
        
        /// <summary>
        /// True means this is at the Value stage and the current entry needs to be resolved as a value.
        /// </summary>
        public bool IsValueStage { get; set; }
        
        /// <summary>
        /// The next command to run to make this valid.
        /// </summary>
        public IRunableCommand NextCommand { get; set; }

        /// <summary>
        /// Method to run when this is successful.
        /// </summary>
        public RunableCommandCallEvent Runable { get; set; }
    }
}