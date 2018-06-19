using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Changelly.ResponseModel
{
    public class CurrencyFull
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        [JsonProperty("enabled")]
        public bool Enable { get; set; }
        [JsonProperty("payinConfirmations")]
        public int PayInConfirmations { get; set; }
        [JsonProperty("image")]
        public string ImageLink { get; set; }
    }
}
