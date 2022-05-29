using Otus.SocialNetwork.Domain;

namespace Otus.SocialNetwork.Persistence.Abstranctions.Users;

public interface ISaveUserQueryObject
{
    Task SaveAsync(User user, CancellationToken ct);
}