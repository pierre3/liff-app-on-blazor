using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiffSdk.Blazor.Data
{
    [JsonObject( NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class LiffData
    {
        public string Language { get; set; }
        public LiffContext Context { get; set; }
        public LiffData(){}
    }
}
