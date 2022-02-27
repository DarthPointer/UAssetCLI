using System.Collections.Generic;

namespace UAssetCLI.Operation
{
    class SaveAsset : IOperation
    {
        bool IOperation.ProcessCommandTree(CommandTree commandTree, out List<Report> reports)
        {
            reports = new List<Report>();

            if (commandTree.subtrees.Count >= 1)
            {
                Program.asset.FilePath = commandTree.subtrees[0].rootString;
            }

            Program.asset.Write(Program.asset.FilePath);
            
            return true;
        }
    }
}
