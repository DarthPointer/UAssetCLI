using System;
using System.Collections.Generic;

using UAssetAPI;
using UAssetAPI.Extension;
using UAssetAPI.StructTypes;

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

                { typeof(sbyte), (x) => sbyte.Parse(x.rootString) },
                { typeof(byte), (x) => byte.Parse(x.rootString) },
                { typeof(short), (x) => short.Parse(x.rootString) },
                { typeof(ushort), (x) => ushort.Parse(x.rootString) },
                { typeof(int), (x) => int.Parse(x.rootString) },
                { typeof(uint), (x) => uint.Parse(x.rootString) },
                { typeof(long), (x) => long.Parse(x.rootString) },
                { typeof(ulong), (x) => ulong.Parse(x.rootString) },

                { typeof(float), (x) => float.Parse(x.rootString) },
                { typeof(double), (x) => double.Parse(x.rootString) },

                { typeof(Guid), (x) => Guid.Parse(x.rootString) },

                { typeof(FVector), GenerateFVector },
                { typeof(System.Drawing.Color), (x) => GenerateColor(x) },
                { typeof(LinearColor), GenerateLinearColor },
                { typeof(FQuat), GenerateFQuat },
                { typeof(FRotator), GenerateFRotator },

                { typeof(DateTime), (x) => DateTime.Parse(x.rootString) },
                { typeof(TimeSpan), (x) => TimeSpan.Parse(x.rootString) },

                { typeof (FString), GenerateFString },          

                { typeof(FName), GenerateFName },
                { typeof(AssetBoundFName), GenerateAssetBoundFName },
                { typeof(FPackageIndex), GenerateFPackageIndex}
            };

        public static object GenerateObject(Type type, CommandTree commandTree)
        {
            if (type.IsEnum)
            {
                return GenerateEnum(type, commandTree);
            }

            else
            {
                return objectFromTreeParsers[type](commandTree);
            }
        }

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

        public static IObjectReference GenerateObjectReference(CommandTree commandTree)
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

        public static System.Drawing.Color GenerateColor(CommandTree commandTree)
        {
            return System.Drawing.Color.FromArgb(
                int.Parse(commandTree.subtrees[0].rootString),
                int.Parse(commandTree.subtrees[1].rootString),
                int.Parse(commandTree.subtrees[2].rootString),
                int.Parse(commandTree.subtrees[3].rootString));
        }

        public static FVector GenerateFVector(CommandTree commandTree)
        {
            return new FVector(
                float.Parse(commandTree.subtrees[0].rootString),
                float.Parse(commandTree.subtrees[1].rootString),
                float.Parse(commandTree.subtrees[2].rootString));
        }

        public static FName GenerateFName(CommandTree commandTree)
        {
            return GenerateNameData(commandTree).AsAssetFName(Program.asset);
        }

        public static FPackageIndex GenerateFPackageIndex(CommandTree commandTree)
        {
            return GenerateObjectReference(commandTree).GetObjectIndex(Program.asset);
        }

        public static LinearColor GenerateLinearColor(CommandTree commandTree)
        {
            return new LinearColor(
                float.Parse(commandTree.subtrees[0].rootString),
                float.Parse(commandTree.subtrees[1].rootString),
                float.Parse(commandTree.subtrees[2].rootString),
                float.Parse(commandTree.subtrees[3].rootString));
        }

        public static FQuat GenerateFQuat(CommandTree commandTree)
        {
            return new FQuat(
                float.Parse(commandTree.subtrees[0].rootString),
                float.Parse(commandTree.subtrees[1].rootString),
                float.Parse(commandTree.subtrees[2].rootString),
                float.Parse(commandTree.subtrees[3].rootString));
        }

        public static FRotator GenerateFRotator(CommandTree commandTree)
        {
            return new FRotator(
                float.Parse(commandTree.subtrees[0].rootString),
                float.Parse(commandTree.subtrees[1].rootString),
                float.Parse(commandTree.subtrees[2].rootString));
        }

        public static object GenerateEnum(Type enumType, CommandTree commandTree)
        {
            return Enum.Parse(enumType, commandTree.rootString);
        }
    }
}
