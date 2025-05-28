using Azure;
using ChatAppWebDomain.Entities.MessageChat;
using ChatAppWebDomain.Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChatApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatAppController : ControllerBase
    {
        private readonly ILogger<ChatAppController> _logger;
        private readonly IMessageChatService _messageChatService;

        public ChatAppController(ILogger<ChatAppController> logger, IMessageChatService messageChatService)
        {
            _messageChatService = messageChatService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MessageChatViewModel messageChatViewModel)
        {
            var response = await _messageChatService.Add(messageChatViewModel);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] MessageChatViewModel messageChatViewModel)
        {
            var response = await _messageChatService.Edit(messageChatViewModel);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _messageChatService.GetById(id);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] MessageChatViewModel messageChatViewModel)
        {
            var response = await _messageChatService.Send(messageChatViewModel);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList(int pageSize, int pageNumber, string? conditions, string? sortOrderColumn, string? sortOrderDirection)
        {
            var response = await _messageChatService.GetList(pageSize, pageNumber, conditions, sortOrderColumn, sortOrderDirection);

            return Ok(response);
        }
    }
}
