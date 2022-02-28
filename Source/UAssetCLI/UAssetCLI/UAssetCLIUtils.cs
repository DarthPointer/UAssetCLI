using UAssetAPI;
using UAssetAPI.Extension;

namespace UAssetCLI
{
    static class UAssetCLICommandTreeUtils
    {
        public const string packageKeyword = "Package";
        public const string classKeyword = "Class";
        public const string outerKeyword = "Outer";
        public const string nameKeyword = "Name";
        public const string indexKeyword = "Index";

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
    }
}
