namespace CommandManager
{
    public interface ISingleTextResolver
    {
        /// <summary>
        /// Resolves the entry as valid.
        /// </summary>
        /// <param name="entry">Entry to resolve. </param>
        /// <returns>True means valid. </returns>
        bool IsValid(string entry);
    }
}