using System.Collections.Generic;

namespace UAssetCLI.Operation
{
    class Syntax : IOperation
    {
        bool IOperation.ProcessCommandTree(CommandTree commandTree, out List<Report> reports)
        {
            reports = new List<Report>();
            
            Program.currentSyntax = Program.syntaxes[commandTree.subtrees[0].rootString];

            return true;
        }
    }
}
