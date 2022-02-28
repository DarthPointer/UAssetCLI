using System;
using System.Collections.Generic;

using UAssetAPI;

namespace UAssetCLI.Operation
{
    class LoadAsset : IOperation
    {
        bool IOperation.ProcessCommandTree(CommandTree commandTree, out List<Report> reports)
        {
            reports = new List<Report>();

            string path = commandTree.subtrees[0].rootString;
            UE4Version version = Program.config.defaultUE4Version;

            if (commandTree.subtrees.Count >= 2)
            {
                version = (UE4Version)Enum.Parse(typeof(UE4Version), commandTree.subtrees[1].rootString);
            }

            UAsset loadedAsset = new UAsset(path, version);

            if (!loadedAsset.VerifyBinaryEquality())
            {
                reports.Add(Report.Warning("Binary equality verification failed. Make sure you use correct engine version argument."));
            }

            Program.asset = loadedAsset;
            

            return true;
        }
    }
}
