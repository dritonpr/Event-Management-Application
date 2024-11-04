namespace EventManagement.Core
{
    public class UserFeedback
    {
        public long Id { get; set; }
        public long EventId { get; set; }
        public Event? Event { get; set; }
        public long UserId { get; set; }
        public User? User { get; set; }
        public string? Message { get; set; }
    }
}
