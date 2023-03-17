using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatNetCore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IChatService _chatService;

        public ChatController(ILogger<ChatController> logger, IChatService chatService)
        {
            _logger = logger;
            _chatService = chatService;
        }

        [HttpGet("{id}/messages")]
        public Task<List<Message>> GetMessages(string id)
        {
            return _chatService.GetMessages(id);
        }

        [HttpPost("{id}/messages")]
        public Task SendMessage(Message message)
        {
            return _chatService.SendMessage(message);
        }
    }
}