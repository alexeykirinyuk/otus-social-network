using Otus.SocialNetwork.Domain;

namespace Otus.SocialNetwork.Persistence.Abstranctions.NewsFeed;

public interface ISaveNewsFeedQueryObject
{
    Task Add(IReadOnlyList<NewsFeedItem> newsFeed, CancellationToken ct);
}