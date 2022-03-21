using Otus.SocialNetwork.Domain;

namespace Otus.SocialNetwork.Persistence.Abstranctions;

public interface ISaveUserQueryObject
{
    Task SaveAsync(User user, CancellationToken ct);
}