namespace OnlineStore.MVC.Models
{
    public class IdentityResponse
    {
        public required string AccessToken { get; set; }

        public required string RefreshToken { get; set; }
    }
}
