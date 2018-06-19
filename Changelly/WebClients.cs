using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace Changelly
{
    public class WebClients
    {
        private readonly WebClient Client;
        public WebClients()
        {
            Client = new WebClient();
        }

        public static string ToHexString(byte[] array)
        {
            StringBuilder hex = new StringBuilder(array.Length * 2);
            foreach (byte b in array)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public (string result, bool Success, string Error) PostToWeb(string apiKey, string apiSecret, string apiUrl, string message)
        {
            try
            {
                HMACSHA512 hmac = new HMACSHA512(Encoding.UTF8.GetBytes(apiSecret));
                byte[] hashmessage = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                string sign = ToHexString(hashmessage);

                Client.Headers.Set("Content-Type", "application/json");
                Client.Headers.Add("api-key", apiKey);
                Client.Headers.Add("sign", sign);

                string result = Client.UploadString(apiUrl, message);
                if (!string.IsNullOrEmpty(result))
                {
                    return (result, true, "");
                }
                return ("", false, "");
            }
            catch(Exception Ex)
            {
                return ("", false, Ex.Message);
            }
        }
        public (string result, bool Success, string Error) GetFromWeb(string symbol)
        {
            try
            {
                string Url = "https://min-api.cryptocompare.com/";

                HttpClient httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(Url),
                };
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                

                var result = httpClient.GetAsync("data/price?fsym=" + symbol.ToUpper() + "&tsyms=USD").GetAwaiter().GetResult();
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var resultContent = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    dynamic val = JObject.Parse(resultContent);
                    return (val.USD.ToString(), true, "");
                }
                return ("", false, "Error from crypto compare.");
               
            }
            catch (Exception Ex)
            {
                return ("", false, Ex.Message);
            }
        }

        //public (string result, bool Success, string Error) HttpToWeb(string apiKey, string apiSecret, string apiUrl, string message)
        //{
        //    HttpClient httpClient = new HttpClient();

        //    HMACSHA512 hmac = new HMACSHA512(Encoding.UTF8.GetBytes(apiSecret));
        //    byte[] hashmessage = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
        //    string sign = ToHexString(hashmessage);


        //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    httpClient.DefaultRequestHeaders.Add("api-key", apiKey);
        //    httpClient.DefaultRequestHeaders.Add("sign", sign);

        //    var postContent = new FormUrlEncodedContent(new[]
        //    {
        //                new KeyValuePair<string, string>("to", _btcRecieveAddress),
        //                new KeyValuePair<string, string>("value", _valBTC.ToString()),
        //                new KeyValuePair<string, string>("account_name", _accountId)
        //    });

        //    var result = httpClient.PostAsync(apiUrl,postContent).GetAwaiter().GetResult();
        //    if (result.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        var resultContent = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //        dynamic val = JObject.Parse(resultContent);
        //        if (!string.IsNullOrEmpty(val.message.ToString()))
        //        {

        //        }
        //    }
        //}
    }
}
