using System.Security.Cryptography;
using System.Text;

namespace CoinBase.Connector.Utils
{
    public static class Hashomatic
    {
        public static byte[] HashHMAC(byte[] key, byte[] message)
        {
            byte[] hashValue = null;

            using (var hash = new HMACSHA256(key))
            {
                hashValue = hash.ComputeHash(message);
            }
            return hashValue;
        }

        public static byte[] HashHMAC(string key, string message)
        {
            byte[] hashValue = null;

            byte[] keyBytes = Encoding.ASCII.GetBytes(key);

            byte[] messageBytes = Encoding.ASCII.GetBytes(message);

            using (var hash = new HMACSHA256(keyBytes))
            {
                hashValue = hash.ComputeHash(messageBytes);
            }
            return hashValue;
        }

    }
}