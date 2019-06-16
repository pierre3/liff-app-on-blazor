using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LiffAppOnBlazor
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class Profile
    {
        public string UserId { get; set; }

        public string DisplayName { get; set; }

        public string PictureUrl { get; set; }

        public string StatusMessage { get; set; }

        public Profile()
        { }
    }
}
