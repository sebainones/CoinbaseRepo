using Coinbase.Common.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Coinbase.Infrastructure
{
    public class CoinbaeJsonSerializer : ICoinbaseJsonSerializer<BuyPrice>
    {
        public CoinbaeJsonSerializer()
        {

        }
        public bool TrySerialize(BuyPrice serializable)
        {
            try
            {
                File.WriteAllText(@$"./{serializable.Base}.json", JsonConvert.SerializeObject(serializable));

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

      
    }
}
