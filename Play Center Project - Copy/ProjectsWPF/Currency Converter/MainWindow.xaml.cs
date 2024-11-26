using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;






namespace CurrencyConverter
{
    public class ExchangeRateData
    {
        public string disclaimer { get; set; }
        public string license { get; set; }
        public long timestamp { get; set; }
        public string @base { get; set; }
        public Dictionary<string, double> rates { get; set; }
    }

    public class CurrencyConverter
    {
        private const string ApiUrl = "https://openexchangerates.org/api/latest.json?app_id=" + "06649b8f11784cc5aa7f544f349e597f";

        public async Task<ExchangeRateData> GetExchangeRates()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(ApiUrl);
                var data = JsonSerializer.Deserialize<ExchangeRateData>(response);

                if (data != null)
                {
                    return data;
                }
                else
                {
                    throw new Exception("Failed to retrieve currency exchange rates.");
                }
            }
        }

        public double ConvertCurrency(double amount, double  rate)
        {
            return amount / rate;
        }
    }

    public sealed partial class MainWindow : Window
    {
        private CurrencyConverter currencyConverter;
        private ExchangeRateData exchangeRates;

        public MainWindow()
        {
            this.InitializeComponent();
            currencyConverter = new CurrencyConverter();
            LoadCurrencies();
        }

        private async void LoadCurrencies()
        {
            exchangeRates = await currencyConverter.GetExchangeRates();
            foreach (var currency in exchangeRates.rates.Keys)
            {
                CurrencyComboBox.Items.Add(currency);
            }
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(AmountTextBox.Text, out double amount))
            {
                string selectedCurrency = CurrencyComboBox.SelectedItem as string;
                string targetCurrency = (TargetCurrencyComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (!string.IsNullOrEmpty(selectedCurrency) && !string.IsNullOrEmpty(targetCurrency))
                {
                    if (exchangeRates.rates.TryGetValue(selectedCurrency, out double baseRate) &&
                        exchangeRates.rates.TryGetValue(targetCurrency, out double targetRate))
                    {
                        // Convert the amount to USD, then to the target currency
                        double amountInUSD = currencyConverter.ConvertCurrency(amount, baseRate);
                        double convertedAmount = amountInUSD * targetRate;

                        ResultTextBlock.Text = $"{amount} {selectedCurrency} = {convertedAmount:F2} {targetCurrency}";
                    }
                    else
                    {
                        ResultTextBlock.Text = "Conversion rate for the selected currency is not available.";
                    }
                }
            }
        }
    }
}



    
