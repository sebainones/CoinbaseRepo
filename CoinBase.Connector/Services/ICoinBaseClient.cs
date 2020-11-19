using System.Threading.Tasks;

namespace Coinbase.Connector.Services
{
    public interface ICoinBaseClient
    {
        string GetTimeStamp();
        void MakeAuthorizedRequestCall(string gEmethodT, string requestPath, string body);
        Task<T> MakeNormalRequestCallAsync<T>(string method, string requestPath, string body);
    }
}