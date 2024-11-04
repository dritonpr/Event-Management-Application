namespace EventManagement.Common.Dto
{
    public class UserFeedbackDto
    {
        public long EventId { get; set; }
        public long UserId { get; set; }
        public string? Message { get; set; }
    }
}
