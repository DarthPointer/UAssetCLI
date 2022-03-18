using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAssetCLI.Operation
{
    class OperationsList : IOperation
    {
        bool IOperation.ProcessCommandTree(CommandTree commandTree, out List<Report> reports)
        {
            reports = new List<Report>();

            foreach (string operationName in Program.operations.Keys)
            {
                Console.WriteLine(operationName);
            }

            return true;
        }
    }
}
