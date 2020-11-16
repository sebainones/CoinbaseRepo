using System;
using System.Net.Http;
using System.Net.Http.Headers;
using CoinBase.Connector.Utils;

namespace Coinbase.Connector.Services
{
    public class CoinBaseClient : ICoinBaseClient
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private const string ApiKeyField = "CB-ACCESS-KEY";
        private readonly string ApiKeyValue;
        private string MessageSignaturField = "CB-ACCESS-SIGN";
        private string MessageSignatureValue;

        private const string TimestampField = "CB-ACCESS-TIMESTAMP";
        private string TimeStamp => GetTimeStamp();

        //TODO: https://developers.coinbase.com/docs/wallet/api-key-authentication
        //https://developers.coinbase.com/api/v2?shell#oauth2-coinbase-connect
        public CoinBaseClient()
        {
            ApiKeyValue = AppSettings.HydratedAppSettings.ApiKey;

            Console.WriteLine($"Hello Unix epoch {TimeStamp}");
            Console.WriteLine(AppSettings.HydratedAppSettings.MediaTypeJson);
        }

        public string GetTimeStamp()
        {
            return ((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
        }

        public async void MakeRequestCall(string method, string requestPath, string body = "")
        {

            AddRequestHeaders(method, requestPath, body);
            httpClient.BaseAddress = new Uri(AppSettings.HydratedAppSettings.ApiEndpoint);

            var result = await httpClient.GetAsync(requestPath);

            Console.WriteLine(result);

        }
        private void AddRequestHeaders(string method, string requestPath, string body = "")
        {

            // All REST requests must contain the following headers:

            // CB-ACCESS-KEY API key as a string
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppSettings.HydratedAppSettings.MediaTypeJson));
            httpClient.DefaultRequestHeaders.Add(ApiKeyField, ApiKeyValue);

            var currentTiempStamp = TimeStamp;
            // CB-ACCESS-SIGN Message signature (see below)
            // timestamp + method + requestPath + body

            var compesedKey = currentTiempStamp + method.ToUpper() + requestPath + body;
            Hashomatic.HashHMAC(AppSettings.HydratedAppSettings.ApiKey, compesedKey);
            httpClient.DefaultRequestHeaders.Add(MessageSignaturField, MessageSignatureValue);

            // CB-ACCESS-TIMESTAMP Timestamp for your request
            httpClient.DefaultRequestHeaders.Add(TimestampField, currentTiempStamp);
        }

    }
}