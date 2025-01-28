using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace lr_fifteen.SignalR
{
    public class ChatHub: Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Receive", message);
            Log.Information("New message added!");
        }
    }
}
