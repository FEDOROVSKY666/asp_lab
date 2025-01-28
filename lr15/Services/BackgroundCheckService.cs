
using Serilog;

namespace lr_fifteen.Services
{
    public class BackgroundCheckService: BackgroundService
    {
        private readonly HttpClient _client;
        private string _url = "https://www.google.com/";

        public BackgroundCheckService()
        {
            _client = new HttpClient();
        }

        public async Task CheckUrlAvailable(String url)
        {
            try
            {
                _url = url;
                var response = await _client.GetAsync(_url);
                Log.Information($"Url - {_url} | Status code {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Log.Error($"Error {ex}");
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Log.Information("URL availability check stopped.");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information("Starting URL availability check...");
                await CheckUrlAvailable(_url);
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
