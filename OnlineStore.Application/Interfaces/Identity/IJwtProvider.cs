namespace OnlineStore.Application.Interfaces.Identity
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(Guid userId);

        string GenerateRefreshToken(Guid userId);
    }
}
