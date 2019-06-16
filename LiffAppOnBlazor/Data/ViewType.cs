using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
namespace LiffAppOnBlazor
{
    [JsonConverter(typeof(StringEnumConverter), new object[] { typeof(CamelCaseNamingStrategy) })]
    public enum ViewType
    {
        Compact,
        Tall,
        Full
    }
}
