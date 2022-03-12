using System.Collections.Generic;

using UAssetAPI.Extension;
using UAssetAPI.StringAccessible;

namespace UAssetCLI.Operation
{
    class SetValue : IOperation
    {
        bool IOperation.ProcessCommandTree(CommandTree commandTree, out List<Report> reports)
        {
            reports = new List<Report>();

            IObjectReference objectReference = UAssetCLICommandTreeUtils.GenerageObjectReference(commandTree.subtrees[0]);

            StringAccessorSequence stringAccessorSequence = new StringAccessorSequence(commandTree.subtrees[1].rootString);

            return true;
        }
    }
}
