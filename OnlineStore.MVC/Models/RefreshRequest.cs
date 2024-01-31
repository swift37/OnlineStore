namespace OnlineStore.MVC.Models
{
    public class RefreshRequest
    {
        public required Guid UserId { get; set; }

        public required string RefreshToken { get; set; }
    }
}
