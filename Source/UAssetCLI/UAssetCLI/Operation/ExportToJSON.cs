using System.Collections.Generic;
using System.IO;

namespace UAssetCLI.Operation
{
    class ExportToJSON : IOperation
    {
        bool IOperation.ProcessCommandTree(CommandTree commandTree, out List<Report> reports)
        {
            reports = new List<Report>();

            string assetFileName = Path.GetFileNameWithoutExtension(Program.asset.FilePath);
            File.WriteAllText(assetFileName + ".json", Program.asset.SerializeJson());

            return true;
        }
    }
}
