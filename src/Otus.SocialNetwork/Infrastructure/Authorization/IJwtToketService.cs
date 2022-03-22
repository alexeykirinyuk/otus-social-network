namespace Otus.SocialNetwork.Infrastructure.Authorization;

public interface IJwtToketService
{
    string GenerateToken(string username);
}