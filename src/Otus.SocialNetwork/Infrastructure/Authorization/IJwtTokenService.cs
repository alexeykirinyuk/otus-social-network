namespace Otus.SocialNetwork.Infrastructure.Authorization;

public interface IJwtTokenService
{
    string GenerateToken(string username);

    Task<string?> GetUsername(string? token);
}