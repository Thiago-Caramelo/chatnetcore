using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatNetCore.Controllers
{
    //[Authorize]
    //[Route("api/[controller]")]
    //[ApiController]
    public class ChatController : Controller
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IChatService _chatService;

        public ChatController(ILogger<ChatController> logger, IChatService chatService)
        {
            _logger = logger;
            _chatService = chatService;
        }

        [HttpGet("api/chat/{id}")]
        public Task<List<Message>> Get([FromRoute]string id)
        {
            return _chatService.GetMessages(id);
        }

        [HttpPost]
        public Task SendMessage([FromBody]Message message)
        {
            return _chatService.SendMessage(message);
        }
    }
}