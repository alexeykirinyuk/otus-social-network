using Otus.SocialNetwork.Domain;

namespace Otus.SocialNetwork.Persistence.Abstranctions.Posts;

public interface ISavePostQueryObject
{
    Task Save(Post post, CancellationToken ct);
}