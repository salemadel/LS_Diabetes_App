using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LS_Diabetes_App.Models
{
    public class DrugsJsonModel
    {
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("__EMPTY_19", NullValueHandling = NullValueHandling.Ignore)]
        public Empty19? Empty19 { get; set; }

        [JsonProperty("__EMPTY_18", NullValueHandling = NullValueHandling.Ignore)]
        public string Empty18 { get; set; }
    }

    public partial struct Empty19
    {
        public long? Integer;
        public string String;

        public static implicit operator Empty19(long Integer) => new Empty19 { Integer = Integer };
        public static implicit operator Empty19(string String) => new Empty19 { String = String };
    }
}
