using System;

namespace CommandManager
{
    /// <summary>
    /// A simple single text truth resolver.
    /// </summary>
    public class SimpleTextResolver : ISingleTextResolver
    {
        /// <summary>
        /// The single resolver for this text resolver.
        /// </summary>
        private readonly string singleTruth;
        
        public SimpleTextResolver(string single)
        {
            singleTruth = !string.IsNullOrWhiteSpace(single)
                ? single
                : throw new ArgumentNullException($"{typeof(SimpleTextResolver)}.{nameof(SimpleTextResolver)}:" +
                                              $"{nameof(single)} must not be null or whitespace.");
        }
        
        /// <summary>
        /// Resolves the entry as valid.
        /// </summary>
        /// <param name="entry">Entry to resolve. </param>
        /// <returns>True means valid. </returns>
        public bool IsValid(string entry)
        {
            return string.Equals(entry, singleTruth, StringComparison.OrdinalIgnoreCase);
        }
    }
}