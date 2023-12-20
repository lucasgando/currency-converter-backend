using currency_converter.Data;
using currency_converter.Data.Entities;
using currency_converter.Data.Interfaces;
using Newtonsoft.Json;

namespace currency_converter.Services
{
    public class UpdateExchangeService
    {
        private readonly IConfiguration _configuration;
        private readonly ConverterContext _context;
        private readonly HttpClient _httpClient;
        public UpdateExchangeService(IConfiguration config, ConverterContext context)
        {
            _configuration = config;
            _context = context;
            _httpClient = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false });
            _httpClient.BaseAddress = new Uri("http://api.exchangeratesapi.io/v1/");
        }
        public bool GetExchangeRates()
        {
            var response = _httpClient.GetAsync($"latest?access_key={_configuration["ExchangeRatesApiAccessKey"]}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            ExchangeRatesResponse? result = JsonConvert.DeserializeObject<ExchangeRatesResponse>(json);
            if (result is null || !result.Success) return false;
            float dolarRate = result.Rates.First(c => c.Key == "USD").Value;
            foreach (KeyValuePair<string, float> kvp in result.Rates)
            {
                if (_context.Currencies.Any(c => c.Symbol == kvp.Key)) continue;
                _context.Add(new Currency()
                {
                    Name = "",
                    Symbol = kvp.Key,
                    ConversionIndex = 1 / (kvp.Value / dolarRate)
                });
            }
            _context.SaveChanges();
            return true;
        }
        public bool GetExchangeRates(List<string> currencies)
        {
            var response = _httpClient.GetAsync($"latest?access_key={_configuration["ExchangeRatesApiAccessKey"]}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            ExchangeRatesResponse? result = JsonConvert.DeserializeObject<ExchangeRatesResponse>(json);
            if (result is null || !result.Success) return false;
            float dolarRate = result.Rates.First(c => c.Key == "USD").Value;
            IEnumerable<KeyValuePair<string, float>> rates = result.Rates.Where(r => currencies.Contains(r.Key));
            foreach (KeyValuePair<string, float> kvp in rates)
            {
                if (_context.Currencies.Any(c => c.Symbol == kvp.Key)) continue;
                _context.Add(new Currency()
                {
                    Name = "",
                    Symbol = kvp.Key,
                    ConversionIndex = 1 / (kvp.Value / dolarRate)
                });
            }
            _context.SaveChanges();
            UpdateExchangeRates();
            return true;
        }
        public bool UpdateExchangeRates()
        {
            var response = _httpClient.GetAsync($"latest?access_key={_configuration["ExchangeRatesApiAccessKey"]}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            ExchangeRatesResponse? result = JsonConvert.DeserializeObject<ExchangeRatesResponse>(json);
            if (result is null || !result.Success) return false;
            float dolarRate = result.Rates.First(c => c.Key == "USD").Value;
            foreach (KeyValuePair<string, float> kvp in result.Rates)
            {
                Currency? currency = _context.Currencies.FirstOrDefault(c => c.Symbol == kvp.Key);
                if (currency is null) continue;
                currency.ConversionIndex = 1 / (kvp.Value / dolarRate);
                _context.Currencies.Update(currency);
            }
            _context.SaveChanges();
            return true;
        }
    }
}
