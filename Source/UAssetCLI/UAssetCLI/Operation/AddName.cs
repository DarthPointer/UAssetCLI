using System.Collections.Generic;

using UAssetAPI;

namespace UAssetCLI.Operation
{
    class AddName : IOperation
    {
        bool IOperation.ProcessCommandTree(CommandTree commandTree, out List<Report> reports)
        {
            reports = new List<Report>();

            FString newName = CommandTreeParsers.GenerateFString(commandTree.subtrees[0]);
            Program.asset.AddNameReference(newName);

            return true;
        }
    }
}
