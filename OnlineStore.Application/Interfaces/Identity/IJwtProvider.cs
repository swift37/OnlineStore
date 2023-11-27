namespace OnlineStore.Application.Interfaces.Identity
{
    public interface IJwtProvider
    {
        Task<string> GenerateAccessToken(Guid userId);

        string GenerateRefreshToken();
    }
}
