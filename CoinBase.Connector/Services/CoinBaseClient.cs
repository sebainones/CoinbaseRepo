using System;
using System.Net.Http;
using System.Net.Http.Headers;
using CoinBase.Connector.Utils;

namespace Coinbase.Connector.Services {
    public class CoinBaseClient : ICoinBaseClient {
        private static readonly HttpClient httpClient = new HttpClient ();
        private readonly AppSettings appSettings;

        private const string ApiKeyField = "CB-ACCESS-KEY";
        private readonly string ApiKeyValue;
        private string MessageSignaturField = "CB-ACCESS-SIGN";
        private string MessageSignatureValue;

        private const string TimestampField = "CB-ACCESS-TIMESTAMP";
        private string TimeStamp => GetTimeStamp ();

        //TODO: https://developers.coinbase.com/docs/wallet/api-key-authentication
        //https://developers.coinbase.com/api/v2?shell#oauth2-coinbase-connect
        public CoinBaseClient (AppSettings appSettings) {
            this.appSettings = appSettings;
            ApiKeyValue = appSettings.ApiKey;

            Console.WriteLine ($"Hello Unix epoch {TimeStamp}");
            Console.WriteLine (appSettings.MediaTypeJson);

        }

        public string GetTimeStamp () {
            return ((Int32) (DateTime.UtcNow.Subtract (new DateTime (1970, 1, 1))).TotalSeconds).ToString ();
        }

        public void CreateRequestHeader (string method, string requestPath, string body = "") {

            //All REST requests must contain the following headers:
            // CB-ACCESS-KEY API key as a string
            // CB-ACCESS-SIGN Message signature (see below)
            // CB-ACCESS-TIMESTAMP Timestamp for your request

            httpClient.DefaultRequestHeaders.Accept.Clear ();
            httpClient.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue (appSettings.MediaTypeJson));
            httpClient.DefaultRequestHeaders.Add (ApiKeyField, ApiKeyValue);

            // timestamp + method + requestPath + body
            var compesedKey = TimeStamp + method.ToUpper () + requestPath + body;

            Hashomatic.HashHMAC (appSettings.ApiKey, compesedKey);
            httpClient.DefaultRequestHeaders.Add (MessageSignaturField, MessageSignatureValue);

        }

    }
}