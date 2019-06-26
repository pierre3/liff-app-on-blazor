using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LiffSdk.Blazor.Data
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class LiffData
    {
        public string Language { get; set; }
        public LiffContext Context { get; set; }

        public LiffData(){}
    }
}
