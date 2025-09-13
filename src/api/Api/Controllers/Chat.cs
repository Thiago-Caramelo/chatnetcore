using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
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

        [HttpGet(Name = "GetMessages")]
        public async Task<List<Message>> Get()
        {
            var results = await _chatService.GetMessages("general");
            return results;
        }

        [HttpPost(Name = "SendMessage")]
        public Task SendMessage([FromBody] Message message)
        {
            return _chatService.SendMessage(message);
        }

        [HttpPost(Name = "Feature1-1")]
        public bool Feature1([FromBody] Message message)
        {
            return true;//comment-1
        }
    }
}