using System.Collections.Generic;

using UAssetAPI.Extension;

namespace UAssetCLI.Operation
{
    class AddImport : IOperation
    {
        bool IOperation.ProcessCommandTree(CommandTree commandTree, out List<Report> reports)
        {
            reports = new List<Report>();

            NameData package = null;
            NameData @class = null;
            IObjectReference outer = null;
            NameData name = null;

            foreach (CommandTree argumentTree in commandTree.subtrees)
            {
                if (argumentTree.rootString == UAssetCLICommandTreeUtils.packageKeyword)
                {
                    package = UAssetCLICommandTreeUtils.GenerateNameData(argumentTree);
                    continue;
                }

                if (argumentTree.rootString == UAssetCLICommandTreeUtils.classKeyword)
                {
                    @class = UAssetCLICommandTreeUtils.GenerateNameData(argumentTree);
                    continue;
                }

                if (argumentTree.rootString == UAssetCLICommandTreeUtils.outerKeyword)
                {
                    outer = UAssetCLICommandTreeUtils.GenerageObjectReference(argumentTree.subtrees[0]);
                    continue;
                }

                if (argumentTree.rootString == UAssetCLICommandTreeUtils.nameKeyword)
                {
                    name = UAssetCLICommandTreeUtils.GenerateNameData(argumentTree);
                    continue;
                }

                if (package == null)
                {
                    package = UAssetCLICommandTreeUtils.GenerateNameData(argumentTree);
                    continue;
                }

                if (@class == null)
                {
                    @class = UAssetCLICommandTreeUtils.GenerateNameData(argumentTree);
                    continue;
                }

                if (name == null)
                {
                    name = UAssetCLICommandTreeUtils.GenerateNameData(argumentTree);
                    continue;
                }

                // Outer is optional thus last positional and defaults to FPackageIndex(0) if omitted
                if (outer == null)
                {
                    outer = UAssetCLICommandTreeUtils.GenerageObjectReference(argumentTree);
                    continue;
                }
            }

            if (outer == null)
            {
                outer = new IndexObjectReference(0);
            }

            Program.asset.AddImport(package, @class, outer, name);

            return true;
        }
    }
}
