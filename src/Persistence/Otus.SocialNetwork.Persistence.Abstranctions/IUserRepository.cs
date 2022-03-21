using Otus.SocialNetwork.Domain;

namespace Otus.SocialNetwork.Persistence.Abstranctions;

public interface IUserRepository
{
    Task<bool> Exists(string username, CancellationToken ct);

    Task Save(User user, CancellationToken ct);
}