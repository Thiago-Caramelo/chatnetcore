﻿using Business.Interfaces;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class Chat : IChatService
    {
        private readonly IChatRepository chatRepository;
        
        public Chat(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        public Task<List<Message>> GetMessages(string chatId)
        {
            return chatRepository.GetMessages(chatId);
        }

        public Task SendMessage(Message message)
        {
            return chatRepository.SendMessage(message);
        }
    }
}
