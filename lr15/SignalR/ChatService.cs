using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace lr_fifteen.SignalR
{
    public class ChatService : BackgroundService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private bool flag = false;

        public ChatService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void Notification()
        {
            flag = true;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (flag)
                {
                    Log.Information("New message added!");
                    flag = false;
                }
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
