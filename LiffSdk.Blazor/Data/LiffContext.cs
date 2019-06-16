using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiffSdk.Blazor.Data
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class LiffContext
    {
        public ContextType Type { get; set; }
        public ViewType ViewType { get; set; }
        public string UserId { get; set; }
        public string UtouId { get; set; }
        public string RoomId { get; set; }
        public string GroupId { get; set; }
        public LiffContext(){}
    }
}
