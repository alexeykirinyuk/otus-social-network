using System.Data;
using Otus.SocialNetwork.Domain;
using Otus.SocialNetwork.Persistence.Abstranctions.NewsFeed;

namespace Otus.SocialNetwork.Persistence.QueryObjects.NewsFeed.SaveNewsFeed;

public sealed class SaveNewsFeedQueryObject : ISaveNewsFeedQueryObject
{
    private readonly IDbConnection _db;

    public SaveNewsFeedQueryObject(IDbConnection db)
    {
        _db = db;
    }

    public Task Add(IReadOnlyList<NewsFeedItem> newsFeed, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}

public static class SaveNewsFeedSql
{
    public const string ADD = @"
INSERT INTO news_feed
VALUES()
";
}