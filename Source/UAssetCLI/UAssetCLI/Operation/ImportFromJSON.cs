using System.Collections.Generic;
using System.IO;
using UAssetAPI;

namespace UAssetCLI.Operation
{
    class ImportFromJSON : IOperation
    {
        bool IOperation.ProcessCommandTree(CommandTree commandTree, out List<Report> reports)
        {
            reports = new List<Report>();

            Program.asset = UAsset.DeserializeJson(File.ReadAllText(commandTree.subtrees[0].rootString));

            return true;
        }
    }
}
