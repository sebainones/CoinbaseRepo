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

        public ICommand LoadedCommand { get; set; }

        private readonly IBusiness business;
        private readonly ILogger logger;

        public MainWindowViewModel(IBusiness business, ILogger<MainWindowViewModel> logger)
        {
            this.business = business ?? throw new ArgumentNullException(nameof(business));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            LoadedCommand = new DelegateCommand(LoadBasePricesBasedOnEurosAsync);
        }

        private async void LoadBasePricesBasedOnEurosAsync()
        {
            try
            {
                Btc = await GetEuroExchangeRateFromAsync(Currencies.BTC);

                Usd = await GetEuroExchangeRateFromAsync(Currencies.USD);

                Eur = await GetEuroExchangeRateFromAsync(Currencies.EUR);

                Eth = await GetEuroExchangeRateFromAsync(Currencies.ETH);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message, null);
            }
        }

        public async Task<string> GetEuroExchangeRateFromAsync(string currency)
        {
            var exchangeRate = await business.GetExchangeRatesAsync(currency);
            return $"{decimal.Round(exchangeRate.Rates[Currencies.EUR], 2)} €";
        }
    }
}
