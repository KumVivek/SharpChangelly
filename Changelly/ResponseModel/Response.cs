using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Changelly.ResponseModel
{
    public class Response
    {
        [JsonProperty("jsonrpc")]
        public string JsonRPC { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("result")]
        public object Result { get; set; }
        [JsonProperty("error")]
        public Error Error { get; set; }
    }
    public class Error
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; } 
    }
}
