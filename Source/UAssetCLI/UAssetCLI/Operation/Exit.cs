using System.Collections.Generic;


namespace UAssetCLI.Operation
{
    class Exit : IOperation
    {
        bool IOperation.ProcessCommandTree(CommandTree commandTree, out List<Report> reports)
        {
            reports = new List<Report>();

            return false;
        }
    }
}
