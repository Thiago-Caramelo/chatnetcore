using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string UserName { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string ChatId { get; set; } = null!;
    }
}
