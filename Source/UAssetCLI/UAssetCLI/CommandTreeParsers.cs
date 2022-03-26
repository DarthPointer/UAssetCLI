using System;
using System.Collections.Generic;

using UAssetAPI;
using UAssetAPI.Extension;

namespace UAssetCLI
{
    static class CommandTreeParsers
    {
        public const string packageKeyword = "Package";
        public const string classKeyword = "Class";
        public const string outerKeyword = "Outer";
        public const string nameKeyword = "Name";
        public const string indexKeyword = "Index";

       
        public static readonly Dictionary<Type, Func<CommandTree, object>> objectFromTreeParsers =
            new Dictionary<Type, Func<CommandTree, object>>()
            {
                { typeof(bool), (x) => bool.Parse(x.rootString) },
                { typeof(int), (x) => int.Parse(x.rootString) },
                { typeof(float), (x) => float.Parse(x.rootString) },
                { typeof(byte), (x) => byte.Parse(x.rootString) },
                { typeof(AssetBoundFName), GenerateAssetBoundFName }
            };

        public static FString GenerateFString(CommandTree commandTree)
        {
            return new FString(commandTree.rootString);
        }

        public static INameReference GenerateNameReference(CommandTree commandTree)
        {
            if (commandTree.rootString == indexKeyword && commandTree.subtrees.Count >= 1)
            {
                return new IndexNameReference(int.Parse(commandTree.subtrees[0].rootString));
            }

            else
            {
                return new FStringNameReference(commandTree.rootString);
            }
        }

        public static NameData GenerateNameData(CommandTree commandTree)
        {
            INameReference nameReference = GenerateNameReference(commandTree.subtrees[0]);
            int number = 0;

            if (commandTree.subtrees.Count >= 2)
            {
                number = int.Parse(commandTree.subtrees[1].rootString);
            }

            return new NameData(nameReference, number);
        }

        public static IObjectReference GenerageObjectReference(CommandTree commandTree)
        {
            if (commandTree.rootString == indexKeyword)
            {
                return new IndexObjectReference(int.Parse(commandTree.subtrees[0].rootString));
            }
            else
            {
                return GenerateNameData(commandTree);
            }
        }

        public static AssetBoundFName GenerateAssetBoundFName(CommandTree commandTree)
        {
            NameData nameData = GenerateNameData(commandTree);

            return new AssetBoundFName(nameData.name.GetFString(Program.asset), Program.asset, nameData.number);
        }
    }
}
