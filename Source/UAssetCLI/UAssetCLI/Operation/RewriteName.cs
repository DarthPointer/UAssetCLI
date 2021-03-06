using System.Collections.Generic;

using UAssetAPI;
using UAssetAPI.Extension;

namespace UAssetCLI.Operation
{
    class RewriteName : IOperation
    {
        bool IOperation.ProcessCommandTree(CommandTree commandTree, out List<Report> reports)
        {
            reports = new List<Report>();

            INameReference nameToRewrite = UAssetCLICommandTreeUtils.GenerateNameReference(commandTree.subtrees[0]);
            FString newName = UAssetCLICommandTreeUtils.GenerateFString(commandTree.subtrees[1]);

            Program.asset.RewriteNameReference(nameToRewrite, newName);

            return true;
        }
    }
}
