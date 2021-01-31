using Coinbase.Common.Models;
using CoinBase.Business;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Coinbase.ClientApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel, IMainWindowViewModel
    {
        private string eur;
        public string Eur
        {
            get { return eur; }
            set
            {
                eur = value;
                OnPropertyRaised(nameof(Eur));
            }
        }

        private string usd;
        public string Usd
        {
            get { return usd; }
            set
            {
                usd = value;
                OnPropertyRaised(nameof(Usd));
            }
        }

        private string ltc;
        public string Ltc
        {
            get { return ltc; }
            set
            {
                ltc = value;
                OnPropertyRaised(nameof(Ltc));
            }
        }

        private string btc;
        public string Btc
        {
            get { return btc; }
            set
            {
                btc = value;
                OnPropertyRaised(nameof(Btc));
            }
        }

        private string eth;
        public string Eth
        {
            get { return eth; }
            set
            {
                eth = value;
                OnPropertyRaised(nameof(Eth));
            }
        }


        private string btcEur;
        public string BtcEur
        {
            get { return btcEur; }
            set
            {
                btcEur = value;
                OnPropertyRaised(nameof(BtcEur));
            }
        }

        private string btcUsd;
        public string BtcUsd
        {
            get { return btcUsd; }
            set
            {
                btcUsd = value;
                OnPropertyRaised(nameof(BtcUsd));
            }
        }

        private string ethEur;
        public string EthEur
        {
            get { return ethEur; }
            set
            {
                ethEur = value;
                OnPropertyRaised(nameof(EthEur));
            }
        }
        private string ltcEur;
        public string LtcEur
        {
            get { return ltcEur; }
            set
            {
                ltcEur = value;
                OnPropertyRaised(nameof(LtcEur));
            }
        }


        public ICommand LoadedPricesBasedOnEurCommand { get; set; }

        public ICommand PairsGroupLoadedCommand { get; set; }


        private readonly IBusiness business;
        private readonly ILogger logger;

        public MainWindowViewModel(IBusiness business, ILogger<MainWindowViewModel> logger)
        {
            this.business = business ?? throw new ArgumentNullException(nameof(business));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            LoadedPricesBasedOnEurCommand = new DelegateCommand(LoadPricesBasedOnEurosAsync);
            PairsGroupLoadedCommand = new DelegateCommand(CryptoFiatPairsGroupLoadedAsync);
        }

        private async void CryptoFiatPairsGroupLoadedAsync()
        {
            try
            {
                BuyPrice buyPriceResult = await business.GetBuyPriceAsync(CryptoFiatPairs.BTCEUR);
                BtcEur = buyPriceResult.Amount;


                logger.LogInformation(BtcEur);

                buyPriceResult = await business.GetBuyPriceAsync(CryptoFiatPairs.BTCUSD);
                BtcUsd = buyPriceResult.Amount;

                buyPriceResult = await business.GetBuyPriceAsync(CryptoFiatPairs.ETHEUR);
                EthEur = buyPriceResult.Amount;

                buyPriceResult = await business.GetBuyPriceAsync(CryptoFiatPairs.LTCEUR);
                LtcEur = buyPriceResult.Amount;
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message, null);
            }

        }

       

        private async void LoadPricesBasedOnEurosAsync()
        {
            //TODO: make indepent callS as each of them could throw an exception
            try
            {
                Btc = await GetEuroExchangeRateFromAsync(Currencies.BTC);

                Usd = await GetEuroExchangeRateFromAsync(Currencies.USD);

                Eur = await GetEuroExchangeRateFromAsync(Currencies.EUR);

                Eth = await GetEuroExchangeRateFromAsync(Currencies.ETH);

                Ltc = await GetEuroExchangeRateFromAsync(Currencies.LTC);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message, null);
            }
        }


       
        private async Task<string> GetEuroExchangeRateFromAsync(string currency)
        {
            var exchangeRate = await business.GetExchangeRatesAsync(currency);
            return $"{decimal.Round(exchangeRate.Rates[Currencies.EUR], 2)} €";
        }
    }
}
