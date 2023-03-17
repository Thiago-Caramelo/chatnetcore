using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IChatRepository
    {
        Task SendMessage(Message message);
        Task<List<Message>> GetMessages(string chatId);
    }
}
