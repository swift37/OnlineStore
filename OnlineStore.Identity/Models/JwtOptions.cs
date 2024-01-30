namespace OnlineStore.Identity.Models
{
    public class JwtOptions
    {
        public string SecretKey { get; init; }

        public string Issuer { get; init; }

        public string Audience { get; init; }

        public double AccessTokenExpiryInMinutes { get; init; }

        public double RefreshTokenExpiryInDays { get; init; }

        public double ShortRefreshTokenExpiryInHours { get; init; }
    }
}
