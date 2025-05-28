using ChatAppWeb.Models;
using ChatAppWebDomain.Entities.MessageChat;
using ChatAppWebDomain.Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatAppWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMessageChatService _messageChatService;

        public HomeController(ILogger<HomeController> logger, IMessageChatService messageChatService)
        {
            _logger = logger;
            _messageChatService = messageChatService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(MessageChatViewModel messageChat)
        {
            _messageChatService.Send(messageChat);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
