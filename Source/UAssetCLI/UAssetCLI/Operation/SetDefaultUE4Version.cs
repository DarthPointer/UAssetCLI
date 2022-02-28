using System;
using System.Collections.Generic;

using UAssetAPI;

namespace UAssetCLI.Operation
{
    class SetDefaultUE4Version : IOperation
    {
        bool IOperation.ProcessCommandTree(CommandTree commandTree, out List<Report> reports)
        {
            reports = new List<Report>();

            Program.config.defaultUE4Version = (UE4Version)Enum.Parse(typeof(UE4Version), commandTree.subtrees[0].rootString);
            Program.SaveConfig();

            return true;
        }
    }
}
