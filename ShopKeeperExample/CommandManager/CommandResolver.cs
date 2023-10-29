using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandManager
{
    /// <summary>
    /// Resolves commands.
    /// </summary>
    public class CommandResolver : ICommandResolver
    {
        /// <summary>
        /// Commands to use when resolving strings.
        /// </summary>
        private List<IRunableCommand> commands;

        public CommandResolver()
        {
            this.commands = new List<IRunableCommand>();
        }
        
        /// <summary>
        /// Resolves the given command and runs the output if any.
        /// </summary>
        /// <param name="command">Command to resolve. </param>
        /// <returns>True means command found. </returns>
        public bool ResolveCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                return false;
            }

            string[] commandPieces = command.Split(' ');
            foreach(IRunableCommand runable in commands)
            {
                if (ResolveEachCommandPiece(
                        current: 0, 
                        commandPieces, 
                        runable, 
                        values: new List<object>()))
                {
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// Resolves a single command by resolving a single part of the command structure.
        /// </summary>
        /// <param name="current">Current segment of the command. </param>
        /// <param name="commandPieces">All the segments of commands. </param>
        /// <param name="runable">The event to run when complete. </param>
        /// <param name="values">Current values collected. </param>
        /// <returns>True means could find a command to resolve. </returns>
        private bool ResolveEachCommandPiece(
            int current, 
            string[] commandPieces,
            IRunableCommand runable,
            List<object> values)
        {
            if (current == commandPieces.Length)
            {
                return false;
            }
            
            bool didResolve = false;
            string currentPiece = commandPieces[current++];
            if (CurrentSegmentOfCommandIsValid(current, currentPiece, runable))
            {
                if (runable.IsValueStage)
                {
                    values.Add(currentPiece);
                }
                
                if (runable.NextCommand != null)
                {
                    didResolve = ResolveEachCommandPiece(current, commandPieces, runable.NextCommand, values);
                }
                else
                {
                    didResolve = true;
                    
                    var args = new CommandArgs()
                    {
                        CommandStrings = commandPieces,
                        CommandValues = values.ToArray(),
                    };
                    runable.Runable?.Invoke(this, args);
                }
            }

            return didResolve;
        }
        
        /// <summary>
        /// Gives the resolver a new command.
        /// </summary>
        /// <param name="command">Command to give. </param>
        /// <returns>True means the command was valid. </returns>
        public bool GiveCommand(IRunableCommand command)
        {
            bool isValid = IsValidCommand(command, new HashSet<IRunableCommand>());
            if (isValid)
            {
                this.commands.Add(command);
            }

            return isValid;
        }

        /// <summary>
        /// Determines if the given command is valid to add to the list.
        /// <c>Runs Recursively.</c>
        /// </summary>
        /// <param name="command">Command to test. </param>
        /// <param name="previousCommands">
        /// A list of previous commands. Prevents the stacktrace going in circles.
        /// </param>
        /// <returns>True means valid. </returns>
        private bool IsValidCommand(IRunableCommand command, HashSet<IRunableCommand> previousCommands)
        {
            if (!previousCommands.Add(command))
            {
                // Prevents a recursive loop
                return false;
            }
            
            if (!command.IsValueStage && command.Entry == null)
            {
                return false;
            }

            if (command.NextCommand != null)
            {
                return IsValidCommand(command.NextCommand, previousCommands);
            }

            return true;
        }
        
        /// <summary>
        /// Determines if the current piece is valid within a larger command.
        /// </summary>
        /// <param name="current">Current stage in the command. </param>
        /// <param name="currentPiece">Current piece to evaluate. </param>
        /// <param name="runable">Command to run. </param>
        /// <returns>True means valid. </returns>
        private bool CurrentSegmentOfCommandIsValid(int current, string currentPiece, IRunableCommand runable)
        {
            if (string.IsNullOrWhiteSpace(currentPiece))
            {
                return false;
            }

            bool isValid = false;
            if (current > 0)
            {
                if (runable.IsValueStage)
                {
                    isValid = true;
                }
            }

            if (!isValid)
            {
                if (runable?.Entry?.IsValid(currentPiece) == true)
                {
                    isValid = true;
                }
            }

            return isValid;
        }
    }
}