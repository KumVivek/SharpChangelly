using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Changelly.ResponseModel
{
    class TransactionResponses
    {

        [JsonProperty("jsonrpc")]
        public string JsonRPC { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("result")]
        public List<SingleTransaction> Result { get; set; }
        [JsonProperty("error")]
        public Error Error { get; set; }
    }
    public class SingleTransaction
    {

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("payinConfirmations")]
        public string PayInConfirmations { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }


        [JsonProperty("currencyFrom")]
        public string CurrencyFrom { get; set; }


        [JsonProperty("currencyTo")]
        public string CurrencyTo { get; set; }


        [JsonProperty("payinAddress")]
        public string PayinAddress { get; set; }

        [JsonProperty("payinExtraId")]
        public string PayinExtraId { get; set; }

        [JsonProperty("payinHash")]
        public string PayinHash { get; set; }


        [JsonProperty("payoutAddress")]
        public string PayoutAddress { get; set; }

        [JsonProperty("payoutExtraId")]
        public string PayoutExtraId { get; set; }

        [JsonProperty("payoutHash")]
        public string PayoutHash { get; set; }
        [JsonProperty("amountFrom")]
        public string AmountFrom { get; set; }

        [JsonProperty("amountTo")]
        public string AmountTo { get; set; }

        [JsonProperty("networkFee")]
        public string NetworkFee { get; set; }

        [JsonProperty("apiExtraFee")]
        public string ApiExtraFee { get; set; }
        [JsonProperty("changellyFee")]
        public string ChangellyFee { get; set; }
        
        
       
        
    }
        
}
