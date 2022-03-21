using System.Data;
using Dapper;
using Otus.SocialNetwork.Domain;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Persistence;

public sealed class UserRepository : IUserRepository
{
    private readonly IDbConnection _db;

    public UserRepository(IDbConnection db)
    {
        _db = db;
    }

    public Task<bool> Exists(string username, CancellationToken ct)
    {
        return _db.QuerySingleAsync<bool>(
            new(
                UsersSql.CHECK_USER_EXISTS_BY_USERNAME,
                new { username },
                cancellationToken: ct
            ));
    }

    public Task Save(User user, CancellationToken ct)
    {
        
    }
}