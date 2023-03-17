using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Chat
    {
        public Chat()
        {
            ChatMessage = new HashSet<ChatMessage>();
        }

        public string Id { get; set; } = null!;

        public virtual ICollection<ChatMessage> ChatMessage { get; set; }
    }
}
