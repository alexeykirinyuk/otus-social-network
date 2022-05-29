namespace Otus.SocialNetwork.Persistence.Abstranctions.Users;

public interface IGetUserPasswordHashQueryObject
{
    Task<(string Hash, string Salt)?> GetPasswordHash(string username, CancellationToken ct);
}