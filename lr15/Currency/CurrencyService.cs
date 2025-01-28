using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Net.WebSockets;

namespace lr_fifteen.Currency
{
    public class CurrencyService: BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        private const string _chacheKey = "Currency";
        private const string _token = "cur_live_1o11f5oF3LGtOMTMSOdfRLrHDSTZTQgaoTyg0OwX";
        private const string _url = "https://api.currencyapi.com/v3/latest";

        public CurrencyService(HttpClient httpClient, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
        }

        public async Task GetCurrencies()
        {
            string uah = "UAH";
            string usd = "USD";
            string eur = "EUR";
            string gbp = "GBP";

            var requestUrl = $"{_url}?apikey={_token}&base_currency={uah}&currencies={string.Join(",", usd, eur, gbp)}";

            try
            {
                if (!_memoryCache.TryGetValue(_chacheKey, out var data))
                {
                    var response = await _httpClient.GetStringAsync(requestUrl);

                    JObject json = JObject.Parse(response);

                    var usdValue = (decimal)json["data"]["USD"]["value"];
                    var eurValue = (decimal)json["data"]["EUR"]["value"];
                    var gbpValue = (decimal)json["data"]["GBP"]["value"];

                    usdValue = decimal.Round(1 / usdValue, 2);
                    eurValue = decimal.Round(1 / eurValue, 2);
                    gbpValue = decimal.Round(1 / gbpValue, 2);

                    var result = $"1 USD = {usdValue} UAH | 1 EUR = {eurValue} | 1 GBP = {gbpValue} UAH";
                    _memoryCache.Set(_chacheKey, result, TimeSpan.FromMinutes(1));
                    Log.Information($"Get from Api: {result}");
                }
                else
                {
                    Log.Information($"From Chache: {data}");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await GetCurrencies();
                await Task.Delay(30000, stoppingToken); 
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
