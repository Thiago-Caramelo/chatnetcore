using Bot;
using Business.Interfaces;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository chatRepository;
        private readonly IBotService botService;

        public ChatService(IChatRepository chatRepository, IBotService botService)
        {
            this.chatRepository = chatRepository;
            this.botService = botService;
        }

        public async Task<List<Message>> GetMessages(string chatId)
        {
            var messages = await chatRepository.GetMessages(chatId);

            var quote = botService.GetStockQuote();

            if (!String.IsNullOrWhiteSpace(quote))
            {
                messages.Insert(0, new Message {
                    CreatedOn = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    Text = quote,
                    UserName = "BOT",
                });
            }

            return messages;
        }

        public Task SendMessage(Message message)
        {
            var text = message?.Text?.Trim() ?? string.Empty;
            if (text.Contains("/stock=", StringComparison.InvariantCultureIgnoreCase))
            {
                botService.SendStockCode(message.Text.Trim().ToLowerInvariant().Replace("/stock=", string.Empty));
                return Task.CompletedTask;
            }

            return chatRepository.SendMessage(message);
        }
    }
}
