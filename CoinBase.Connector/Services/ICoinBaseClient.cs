using System.Threading.Tasks;

namespace Coinbase.Connector.Services
{
    public interface ICoinBaseClient
    {
        string GetTimeStamp();
        Task<T> MakeNormalRequestCallAsync<T>(string requestPath, string body);
        Task<T> MakeAuthorizedRequestCall<T>(string requestPath, string body);
    }
}