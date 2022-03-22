namespace Otus.SocialNetwork.Persistence.Abstranctions;

public interface IGetUserPasswordHashQueryObject
{
    Task<(string Hash, string Salt)?> GetPasswordHash(string username, CancellationToken ct);
}