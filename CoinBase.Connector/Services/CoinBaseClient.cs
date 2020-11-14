using System;

namespace Coinbase.Connector.Services
{
    public class CoinBaseClient : ICoinBaseClient
    {
        public Int32 TimeStamp => (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        //TODO: https://developers.coinbase.com/docs/wallet/api-key-authentication
        //https://developers.coinbase.com/api/v2?shell#oauth2-coinbase-connect
        public CoinBaseClient()
        {              

            Console.WriteLine($"Hello Unix epoch {unixTimestamp}");
        }

       public string GetTimeStamp()
        {
            return ((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
        }

        // Computes a keyed hash for a source file and creates a target file with the keyed hash
        // prepended to the contents of the source file.
        public static void SignFile(byte[] key, String sourceFile, String destFile)
        {
        // Initialize the keyed hash object.
        using (HMACSHA256 hmac = new HMACSHA256(key))
        {
            // Compute the hash of the input file.
            byte[] hashValue = hmac.ComputeHash(inStream);
            
        }
        return;
    } // end SignFile

    }
}