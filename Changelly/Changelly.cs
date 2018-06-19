using Changelly.ResponseModel;
using Changelly.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Changelly
{
    public class Changelly
    {
        private  readonly string APIKEY ;
        private  readonly string APISECRET ;
        private  readonly string APIURL ;
        private WebClients WebClients;
        public Changelly(string apiKey, string apiSeceret, string apiUrl)
        {
            APIKEY = apiKey;
            APISECRET = apiSeceret;
            APIURL = apiUrl;
            WebClients = new WebClients();
        }

        public (List<Currency> currency,bool Success,string Error) GetCurrencies()
        {
            try
            {
                string message = @"{
		            ""jsonrpc"": ""2.0"",
		            ""id"": 1,
		            ""method"": ""getCurrencies"",
		            ""params"": []
			    }";

                List<Currency> currencies = new List<Currency>();
                var result = WebClients.PostToWeb(APIKEY, APISECRET, APIURL, message);
                var response = JsonConvert.DeserializeObject<Response>(result.result);
                if (response.Error == null)
                {
                    foreach(var x in response.Result as JArray)
                    {
                        Currency currency = new Currency() {
                            Name = x.ToString()
                        };
                        currencies.Add(currency);
                    }
                    Currency usdcurrency = new Currency() {
                        Name = "usd"
                    };
                    currencies.Add(usdcurrency);

                    return (currencies,true,"");
                }
                else
                {
                    return (null, false, response.Error.Message);
                }
            }
            catch(Exception Ex)
            {
                return (null, false, Ex.Message);
            }
        }
        public (IList<CurrencyFull> currency, bool Success, string Error) GetCurrenciesFull()
        {
            try
            {
                string message = @"{
		            ""jsonrpc"": ""2.0"",
		            ""id"": 1,
		            ""method"": ""getCurrenciesFull"",
		            ""params"": []
			    }";
                
                IList<CurrencyFull> currencyFulls;
                IList<string> invalidCurrencyInJson;
                var result = WebClients.PostToWeb(APIKEY, APISECRET, APIURL, message);
                var response = JsonConvert.DeserializeObject<Response>(result.result);
                if (response.Error == null)
                {
                    currencyFulls = ListConverter.DeserializeToList<CurrencyFull>(response.Result.ToString());
                    if (ListConverter.InvalidJsonElements != null)
                    {
                        if (ListConverter.InvalidJsonElements.Count != 0)
                        {
                            invalidCurrencyInJson = ListConverter.InvalidJsonElements;
                            return (null, false, "Invalid elements from changelly.");
                        }
                    }
                    return (currencyFulls, true, "");
                }
                else
                {
                    return (null, false, response.Error.Message);
                }
            }
            catch (Exception Ex)
            {
                return (null, false, Ex.Message);
            }
        }
        public (double amount, bool Success, string Error) GetExchangeAmount(string fromCurrency, string toCurrency,double amount)
        {
            try
            {
                string message = "{\"id\": \"test\",\"jsonrpc\": \"2.0\",\"method\": \"getExchangeAmount\",\"params\":{\"from\": \"" + fromCurrency + "\",\"to\": \"" + toCurrency + "\",\"amount\": \"" + amount + "\"}}";
                if (fromCurrency.ToUpper() != "USD" && toCurrency.ToUpper() != "USD")
                {

                    var result = WebClients.PostToWeb(APIKEY, APISECRET, APIURL, message);
                    if (result.Success)
                    {
                        var response = JsonConvert.DeserializeObject<ExchangeResponse>(result.result);
                        if (response.Error == null)
                        {
                            return (Convert.ToDouble(response.Result), true, "");
                        }
                        return (0, false, response.Error.Message);
                    }
                    return (0, false, result.Error);
                }
                else
                {
                    if(fromCurrency.ToUpper() == "USD" && toCurrency.ToUpper() == "USD")
                    {
                        return (0, false, "Invalid Inputs");
                    }

                    var usd = WebClients.GetFromWeb(fromCurrency.ToUpper() != "USD" ? fromCurrency : toCurrency);
                    if (usd.Success)
                    {
                        if(fromCurrency.ToUpper() != "USD" && toCurrency.ToUpper() == "USD")
                        {
                            return (Convert.ToDouble(Convert.ToDecimal(amount) * Convert.ToDecimal(usd.result)), true, "");
                        }
                        else
                        {
                            return (Convert.ToDouble(Convert.ToDecimal(amount) / Convert.ToDecimal(usd.result)), true, "");
                        }
                      
                    }
                    
                    return (0, false,"");
                }
            }
            catch (Exception Ex)
            {
                return (0,false,Ex.Message);
            }
        }
        public (double amount, bool Success, string Error) GetMinAmount(string fromCurrency, string toCurrency)
        {
            try
            {
                string message = "{\"id\": \"test\",\"jsonrpc\": \"2.0\",  \"method\": \"getMinAmount\",  \"params\": {  	\"from\": \"" + fromCurrency + "\",   	\"to\": \"" + toCurrency + "\"  }}";
                var result = WebClients.PostToWeb(APIKEY, APISECRET, APIURL, message);
                if (result.Success)
                {
                    var response = JsonConvert.DeserializeObject<ExchangeResponse>(result.result);
                    if (response.Error == null)
                    {
                        return (Convert.ToDouble(response.Result), true, "");
                    }
                    return (0, false, response.Error.Message);
                }
                return (0, false, result.Error);
            }
            catch (Exception Ex)
            {
                return (0, false, Ex.Message);
            }
        }
        public (CreateTransaction CreateTransaction,bool Success, string Error) CreateTransaction(string fromCurrency, string toCurrency,string address,double amount)
        {
            try
            {
                string message = "{  \"id\": \"test\",  \"jsonrpc\": \"2.0\",  \"method\": \"createTransaction\",  \"params\": {  	\"from\": \""+fromCurrency+"\", \"to\": \""+toCurrency+"\",   	\"address\": \""+address+"\", 	\"amount\": \""+amount+"\"  }}";
                var result = WebClients.PostToWeb(APIKEY, APISECRET, APIURL, message);
                if (result.Success)
                {
                    var response = JsonConvert.DeserializeObject<CreateTransaction>(result.result);
                    if (response.Error == null)
                    {
                        return (response, true, "");
                    }
                    return (null, false, response.Error.Message);
                }
                return (null, false, result.Error);
            }
            catch (Exception Ex)
            {
                return (null, false, Ex.Message);
            }
        }
        public (string status,bool Success, string Error) GetStatus(string transactionId)
        {
            try
            {
                string message = "{  \"id\": \"test\",  \"jsonrpc\": \"2.0\",  \"method\": \"getStatus\",  \"params\": {  	\"id\": \"" + transactionId + "\"  }}";
                var result = WebClients.PostToWeb(APIKEY, APISECRET, APIURL, message);
                if (result.Success)
                {
                    var response = JsonConvert.DeserializeObject<ExchangeResponse>(result.result);
                    if(response.Error != null)
                    {
                        return (response.Result.ToString(), true, "");
                    }
                    return ("", false, response.Error.Message);
                }
                return ("", false, result.Error);
            }
            catch (Exception Ex)
            {
                return ("", false, Ex.Message);
            }
        }

        public (List<SingleTransaction> SingleTransaction,bool Success, string Error ) GetTransactions()
        {
            try
            {
                string message = "{  \"id\": \"test\",  \"jsonrpc\": \"2.0\",  \"method\": \"getTransactions\",  \"params\": {  }    }";
                var result = WebClients.PostToWeb(APIKEY, APISECRET, APIURL, message);
                if (result.Success)
                {
                    var response = JsonConvert.DeserializeObject<TransactionResponses>(result.result);
                    if (response.Error != null)
                    {
                        return (response.Result, true, "");
                    }
                    return (null, false, response.Error.Message);
                }
                return (null, false, result.Error);
            }
            catch (Exception Ex)
            {
                return (null, false, Ex.Message);
            }
            
        }

    }
}
