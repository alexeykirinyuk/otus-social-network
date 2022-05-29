using System.Data;
using Dapper;
using Otus.SocialNetwork.Persistence.Abstranctions.Users;
using Otus.SocialNetwork.Persistence.QueryObjects.Users.GetUserPasswordHash.Records;

namespace Otus.SocialNetwork.Persistence.QueryObjects.Users.GetUserPasswordHash;

public sealed class GetUserPasswordHashQueryObject : IGetUserPasswordHashQueryObject
{
    private readonly IDbConnection _db;

    public GetUserPasswordHashQueryObject(IDbConnection db)
    {
        _db = db;
    }

    public async Task<(string Hash, string Salt)?> GetPasswordHash(string username, CancellationToken ct)
    {
        var result = await _db.QueryFirstOrDefaultAsync<UserPasswordHashRecord>(
            new(GetUserPasswordHashSql.GET_USER_PASSWORD_HASH_BY_USERNAME, new { username }, cancellationToken: ct));

        if (result == null)
        {
            return null;
        }
        
        return (result.PasswordHash, result.PasswordSalt);
    }
}