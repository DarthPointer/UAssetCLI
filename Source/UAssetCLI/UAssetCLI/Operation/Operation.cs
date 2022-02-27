using System.Collections.Generic;

namespace UAssetCLI.Operation
{
    /// <summary>
    /// Interface for all object classes that implement any UAssetCLI-callable command.
    /// </summary>
    interface IOperation
    {
        /// <summary>
        /// Place to define the behaviour of operation when it is called.
        /// </summary>
        /// <param name="commandTree">Stores the relevant operation call expression tree to read arguments from it.</param>
        /// <returns>Whether the execution loop of the app should proceed to the next command. Returning <c>false</c> is supposed to stop/return the execution.</returns>
        bool ProcessCommandTree(CommandTree commandTree, out List<Report> reports);
    }
}
