﻿using System;
using System.Collections.Generic;

using UAssetAPI;
using UAssetCLI.Operation;

namespace UAssetCLI
{
    class Program
    {
        public static UAsset asset;

        static readonly Dictionary<string, IOperation> operations =
            new Dictionary<string, IOperation>()
            {
                { "Syntax", new Syntax() },

                { "LoadAsset", new LoadAsset() },
                { "SaveAsset", new SaveAsset() },
                { "Exit", new Exit() },

                { "AddName", new AddName() },
                { "RewriteName", new RewriteName() },

                { "AddImport", new AddImport() }
            };

        public static readonly Dictionary<string, SyntaxSettings> syntaxes =
            new Dictionary<string, SyntaxSettings>()
            {
                { "0.0.0.0", new SyntaxSettings() },

                { "latest", new SyntaxSettings() }
            };

        public static SyntaxSettings currentSyntax = syntaxes["latest"];

        static void Main(string[] args)
        {
            while (ProcessSTDINCommand(out List<Report> reports))
            {
                foreach (Report report in reports)
                {
                    Console.WriteLine(report);
                }
            }
        }

        static bool ProcessSTDINCommand(out List<Report> reports)
        {
            bool result = true;

            try
            {
                Console.WriteLine();
                Console.Write(">");
                result = ProcessCommand(Console.ReadLine(), out reports);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                reports = new List<Report>();
            }

            return result;
        }

        static bool ProcessCommand(string command, out List<Report> reports)
        {
            CommandTree commandTree = CommandTree.ParseCommand(command);

            if (operations.ContainsKey(commandTree.rootString))
            {
                return operations[commandTree.rootString].ProcessCommandTree(commandTree, out reports);
            }

            else
            {
                reports = new List<Report>();
                reports.Add(Report.Error($"Operation `{commandTree.rootString}` not found."));

                return true;
            }
        }
    }
}
