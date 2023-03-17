namespace Business.Models
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string UserName { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string ChatId { get; set; } = "general";
    }
}
