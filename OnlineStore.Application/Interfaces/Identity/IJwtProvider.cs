namespace OnlineStore.Application.Interfaces.Identity
{
    public interface IJwtProvider
    {
        Task<string> GenerateAccessToken(string userId);

        string GenerateRefreshToken();
    }
}
