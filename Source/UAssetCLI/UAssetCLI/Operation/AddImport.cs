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
                if (argumentTree.rootString == CommandTreeParsers.packageKeyword)
                {
                    package = CommandTreeParsers.GenerateNameData(argumentTree);
                    continue;
                }

                if (argumentTree.rootString == CommandTreeParsers.classKeyword)
                {
                    @class = CommandTreeParsers.GenerateNameData(argumentTree);
                    continue;
                }

                if (argumentTree.rootString == CommandTreeParsers.outerKeyword)
                {
                    outer = CommandTreeParsers.GenerateObjectReference(argumentTree.subtrees[0]);
                    continue;
                }

                if (argumentTree.rootString == CommandTreeParsers.nameKeyword)
                {
                    name = CommandTreeParsers.GenerateNameData(argumentTree);
                    continue;
                }

                if (package == null)
                {
                    package = CommandTreeParsers.GenerateNameData(argumentTree);
                    continue;
                }

                if (@class == null)
                {
                    @class = CommandTreeParsers.GenerateNameData(argumentTree);
                    continue;
                }

                if (name == null)
                {
                    name = CommandTreeParsers.GenerateNameData(argumentTree);
                    continue;
                }

                // Outer is optional thus last positional and defaults to FPackageIndex(0) if omitted
                if (outer == null)
                {
                    outer = CommandTreeParsers.GenerateObjectReference(argumentTree);
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
