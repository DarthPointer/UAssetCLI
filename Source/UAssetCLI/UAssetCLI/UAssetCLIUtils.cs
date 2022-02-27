using UAssetAPI;
using UAssetAPI.Extension;

namespace UAssetCLI
{
    static class UAssetCLICommandTreeUtils
    {
        const string indexKeyword = "index";

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
    }
}
