using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Models;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ChatDbContext _context;
        private readonly IMapper _mapper;

        public ChatRepository(ChatDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public Task<List<Message>> GetMessages(string chatId)
        {
            return _context.ChatMessage
                .ProjectTo<Message>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public Task SendMessage(Message message)
        {
            var chatMessage = _mapper.Map<ChatMessage>(message);
            
            _context.ChatMessage.Add(chatMessage);

            return _context.SaveChangesAsync();
        }
    }
}
