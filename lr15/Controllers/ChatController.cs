using lr_fifteen.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;

namespace lr_fifteen.Controllers
{
    public class ChatController: Controller
    {
        private readonly ChatService _service;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubContext, ChatService serivce)
        {
            _hubContext = hubContext;
            _service = serivce;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  Index(string message)
        {
            await _hubContext.Clients.All.SendAsync("Receive", message);
            _service.Notification();

            return View();
        }
    }
}
