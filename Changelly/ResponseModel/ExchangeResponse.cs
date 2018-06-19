using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Changelly.ResponseModel
{
    class ExchangeResponse
    {
        [JsonProperty("jsonrpc")]
        public string JsonRPC { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("result")]
        public object Result { get; set; }
        [JsonProperty("error")]
        public Error Error { get; set; }
    }
  
}
