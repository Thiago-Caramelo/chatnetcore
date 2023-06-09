﻿using AutoMapper;
using Business.Models;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Profiles
{
    public class ChatMessageProfile : Profile
    {
        public ChatMessageProfile()
        {
            CreateMap<Message, ChatMessage>();
            CreateMap<ChatMessage, Message>();
        }
    }
}
