using System;
using System.Collections.Generic;

using UAssetAPI;
using UAssetAPI.Extension;
using UAssetAPI.StringAccessible;

namespace UAssetCLI.Operation
{
    class SetValue : IOperation
    {
        bool IOperation.ProcessCommandTree(CommandTree commandTree, out List<Report> reports)
        {
            reports = new List<Report>();

            IObjectReference objectReference = CommandTreeParsers.GenerageObjectReference(commandTree.subtrees[0]);
            FObjectResource @object = objectReference.GetObject(Program.asset);

            StringAccessorSequence stringAccessorSequence = new StringAccessorSequence(commandTree.subtrees[1].rootString);
            IStringAccessible stringAccessible = stringAccessorSequence.Access(@object);

            stringAccessible.SetValue(CommandTreeParsers.objectFromTreeParsers[stringAccessible.ValueType](commandTree.subtrees[2]));

            return true;
        }
    }
}
