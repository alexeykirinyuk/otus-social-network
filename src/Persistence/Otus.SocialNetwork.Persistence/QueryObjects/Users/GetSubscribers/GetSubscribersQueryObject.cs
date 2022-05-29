using System.Data;
using Dapper;
using Otus.SocialNetwork.Persistence.Abstranctions.Users;

namespace Otus.SocialNetwork.Persistence.QueryObjects.Users.GetSubscribers;

public sealed class GetSubscribersQueryObject : IGetSubscribersQueryObject
{
    private readonly IDbConnection _db;

    public GetSubscribersQueryObject(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<string>> GetUsernames(string username, CancellationToken ct)
    {
        var subscribers = await _db.QueryAsync<string>(
            new(
                GetSubscribersSql.GET_SUBSCRIBER_USERNAMES,
                new { username },
                cancellationToken: ct));

        return subscribers.AsList();
    }
}