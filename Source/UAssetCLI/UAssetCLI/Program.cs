using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Newtonsoft.Json;
using UAssetAPI;
using UAssetCLI.Operation;

namespace UAssetCLI
{
    class Program
    {
        public static UAsset asset;
        private static string toolDir;

        private const string relativeConfigFilePath = "Config.json";
        public static Config config = null;

        public static readonly Dictionary<string, IOperation> operations =
            new Dictionary<string, IOperation>()
            {
                { "Syntax", new Syntax() },

                { "OperationsList", new OperationsList() },

                { "LoadAsset", new LoadAsset() },
                { "SaveAsset", new SaveAsset() },

                { "SetDefaultUE4Version", new SetDefaultUE4Version() },

                { "Exit", new Exit() },

                { "AddName", new AddName() },
                { "RewriteName", new RewriteName() },

                { "AddImport", new AddImport() },

                { "SetValue", new SetValue() }
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
            toolDir = Assembly.GetExecutingAssembly().Location;
            toolDir = toolDir.Substring(0, toolDir.LastIndexOf('\\') + 1);

            LoadOrCreateConfig();

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


        private static void LoadOrCreateConfig()
        {
            if (File.Exists(toolDir + relativeConfigFilePath))
            {
                try
                {
                    config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(toolDir + relativeConfigFilePath));
                    return;
                }
                catch
                {
                }
            }

            config = new Config();
            SaveConfig();
        }

        public static void SaveConfig()
        {
            File.WriteAllText(toolDir + relativeConfigFilePath, JsonConvert.SerializeObject(config,
                settings: new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                }
                ));
        }
    }
}
