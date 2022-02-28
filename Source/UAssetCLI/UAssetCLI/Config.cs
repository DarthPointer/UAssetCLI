using Newtonsoft.Json;
using UAssetAPI;

namespace UAssetCLI
{
    [JsonObject(memberSerialization: MemberSerialization.OptOut)]
    class Config
    {
        public UE4Version defaultUE4Version = UE4Version.UNKNOWN;
    }
}
