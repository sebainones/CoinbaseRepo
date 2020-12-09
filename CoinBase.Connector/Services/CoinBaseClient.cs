using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoinBase.Connector.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Coinbase.Connector.Services
{
    public class CoinBaseClient : ICoinBaseClient
    {
        private static readonly HttpClient httpClient = new HttpClient() { BaseAddress = new Uri(AppSettings.HydratedAppSettings.ApiEndpoint) };
        private readonly ILogger logger;

        private const string ApiKeyField = "CB-ACCESS-KEY";
        private string MessageSignaturField = "CB-ACCESS-SIGN";
        private const string TimestampField = "CB-ACCESS-TIMESTAMP";

        private string TimeStamp => GetTimeStamp();

        //TODO: https://developers.coinbase.com/docs/wallet/api-key-authentication
        //https://developers.coinbase.com/api/v2?shell#oauth2-coinbase-connect
        public CoinBaseClient(ILogger<CoinBaseClient> logger)
        {
            Console.WriteLine($"Hello Unix epoch {TimeStamp}");

            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public string GetTimeStamp()
        {
            return ((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
        }
        public async Task<T> MakeNormalRequestCallAsync<T>(string requestPath, string body = "")
        {
            AddBasicRequestHeaders();
            return await PerformRequest<T>(requestPath);
        }
        public async Task<T> MakeAuthorizedRequestCall<T>(string requestPath, string body)
        {
            AddApiKeyRequestHeaders(RestMethods.GET, requestPath, body);
            return await PerformRequest<T>(requestPath);
        }
        private async Task<T> PerformRequest<T>(string requestPath)
        {
            try
            {
                var result = await httpClient.GetAsync(requestPath);
                result.EnsureSuccessStatusCode();
                string content = await result.Content.ReadAsStringAsync();
                T typedResult = JsonConvert.DeserializeObject<T>(content);
                return typedResult;
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message, null);
            }
            return default;
        }
        private void AddApiKeyRequestHeaders(string method, string requestPath, string body = "")
        {
            //https://developers.coinbase.com/api/v2#api-key
            // All REST requests must contain the following headers:

            // CB-ACCESS-KEY API key as a string
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppSettings.HydratedAppSettings.MediaTypeJson));
            httpClient.DefaultRequestHeaders.Add(ApiKeyField, AppSettings.HydratedAppSettings.ApiKey);

            var currentTiempStamp = TimeStamp;

            // CB-ACCESS-SIGN Message signature (see below)
            // timestamp + method + requestPath + body
            var composedKey = currentTiempStamp + method.ToUpper() + requestPath + body;
            byte[] hashedSignature = Hashomatic.HashHMAC(AppSettings.HydratedAppSettings.ApiSecret, composedKey);

            string MessageSignatureValue = System.Text.Encoding.ASCII.GetString(hashedSignature);
            httpClient.DefaultRequestHeaders.Add(MessageSignaturField, MessageSignatureValue);

            // CB-ACCESS-TIMESTAMP Timestamp for your request
            httpClient.DefaultRequestHeaders.Add(TimestampField, currentTiempStamp);
        }
        private void AddBasicRequestHeaders()
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppSettings.HydratedAppSettings.MediaTypeJson));
        }
    }
}