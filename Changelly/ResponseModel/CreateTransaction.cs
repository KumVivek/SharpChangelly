using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Changelly.ResponseModel
{
    public class CreateTransaction
    {
        [JsonProperty("jsonrpc")]
        public string JsonRPC { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("result")]
        public TransactionResponse Result { get; set; }
        [JsonProperty("error")]
        public Error Error { get; set; }
    }
    public class TransactionResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("apiExtraFee")]
        public string ApiExtraFee { get; set; }
        [JsonProperty("changellyFee")]
        public string ChangellyFee { get; set; }
        [JsonProperty("payinExtraId")]
        public string PayinExtraId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("currencyFrom")]
        public string CurrencyFrom { get; set; }
        [JsonProperty("currencyTo")]
        public string CurrencyTo { get; set; }
        [JsonProperty("amountTo")]
        public double AmountTo { get; set; }
        [JsonProperty("payinAddress")]
        public string PayinAddress { get; set; }
        [JsonProperty("payoutAddress")]
        public string PayoutAddress { get; set; }
        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }
    }
        

}
