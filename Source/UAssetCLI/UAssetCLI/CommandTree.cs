using System;
using System.Text;
using System.Collections.Generic;

namespace UAssetCLI
{
    interface ICommandTreeElement { }

    class CommandTree : ICommandTreeElement
    {
        private const char separator = ' ';
        private const char escapeCharacter = '\\';
        private const char stringQuote = '"';

        private const char subtreesOpen = '(';
        private const char subtreesClose = ')';
        private const char containerOpen = '{';
        private const char containerClose = '}';


        public string rootString;

        public List<CommandTree> subtrees = new List<CommandTree>();

        public static CommandTree ParseCommand(string command)
        {
            CommandTree result = new CommandTree();
            int currentIndex = 0;
            result.ReadFromString(command, ref currentIndex);

            return result;
        }

        private void ReadFromString(string commandString, ref int currentIndex, bool endAtSubtreesClose = false)
        {
            bool finishedReading = false;
            bool lockFirstCharacter = true;
            bool firstCharacter = true;
            bool inStringQuotes = false;
            bool inSubtrees = false;
            bool rootStringEnded = false;

            StringBuilder stringBuilder = new StringBuilder();

            currentIndex--;
            while (!finishedReading)
            {
                currentIndex++;

                if (lockFirstCharacter)
                {
                    lockFirstCharacter = false;
                }
                else
                {
                    firstCharacter = false;
                }


                if (currentIndex >= commandString.Length)
                {
                    if (inStringQuotes)
                    {
                        throw new FormatException($"Unexpected end of command: missing closing quote `{stringQuote}`.");
                    }

                    if (inSubtrees)
                    {
                        throw new FormatException($"Unexpected end of command: missing closing for parentheses `{subtreesClose}`.");
                    }

                    finishedReading = true;
                    continue;
                }

                if (!rootStringEnded)
                {
                    if (commandString[currentIndex] == stringQuote)
                    {
                        if (firstCharacter)
                        {
                            inStringQuotes = true;

                            continue;
                        }

                        if (inStringQuotes)
                        {
                            inStringQuotes = false;
                            rootStringEnded = true;

                            continue;
                        }

                        // else
                        throw new FormatException($"Unexpected quote `{stringQuote}` symbol at position {currentIndex + 1}.");
                    }

                    // Exit from quotes is handled above
                    if (inStringQuotes)
                    {
                        stringBuilder.Append(commandString[currentIndex]);
                        continue;
                    }

                    if (commandString[currentIndex] == subtreesOpen)
                    {
                        rootStringEnded = true;
                        inSubtrees = true;

                        continue;
                    }

                    if (commandString[currentIndex] == subtreesClose)
                    {
                        if (endAtSubtreesClose)
                        {
                            finishedReading = true;

                            continue;
                        }

                        throw new FormatException($"Unexpected parentheses closing `{subtreesClose}` at position {currentIndex + 1}.");
                    }

                    if (commandString[currentIndex] == separator)
                    {
                        if (!firstCharacter)
                        {
                            finishedReading = true;
                        }
                        else
                        {
                            lockFirstCharacter = true;
                        }
                        continue;
                    }

                    if (commandString[currentIndex] == escapeCharacter)
                    {
                        currentIndex++;

                        if (currentIndex >= commandString.Length)
                        {
                            throw new FormatException($"Unexpected escape character `{escapeCharacter}` at the end of command.");
                        }

                        // else
                        stringBuilder.Append(commandString[currentIndex]);
                        continue;
                    }

                    // else
                    stringBuilder.Append(commandString[currentIndex]);
                    continue;
                }
                else // if (rootStringEnded)
                {
                    if (!inSubtrees)
                    {
                        if (commandString[currentIndex] == stringQuote)
                        {
                            throw new FormatException($"Unexpected quote `{stringQuote}` symbol at position {currentIndex + 1}.");
                        }

                        if (commandString[currentIndex] == escapeCharacter)
                        {
                            throw new FormatException($"Unexpected escape character `{escapeCharacter}` at position {currentIndex + 1}.");
                        }

                        if (commandString[currentIndex] == subtreesOpen)
                        {
                            inSubtrees = true;
                            continue;
                        }

                        if (commandString[currentIndex] == subtreesClose)
                        {
                            if (endAtSubtreesClose)
                            {
                                finishedReading = true;
                                continue;
                            }

                            throw new FormatException($"Unexpected parentheses closing `{subtreesClose}` at position {currentIndex + 1}.");
                        }

                        if (commandString[currentIndex] == separator)
                        {
                            finishedReading = true;
                            continue;
                        }

                        // else
                        throw new FormatException($"Unexpected `{commandString[currentIndex]}` at position {currentIndex}.");
                    }
                    else
                    {
                        if (commandString[currentIndex] == subtreesClose)
                        {
                            finishedReading = true;
                            inSubtrees = false;
                            currentIndex++;

                            continue;
                        }

                        if (commandString[currentIndex] == separator)
                        {
                            continue;
                        }

                        // else
                        CommandTree newSubtree = new CommandTree();
                        subtrees.Add(newSubtree);
                        newSubtree.ReadFromString(commandString, ref currentIndex, true);

                        continue;
                    }
                }
            }

            rootString = stringBuilder.ToString();
            currentIndex--;
        }
    }
}
