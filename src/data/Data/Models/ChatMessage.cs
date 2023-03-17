using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class ChatMessage
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserName { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string ChatId { get; set; } = null!;
    }
}
