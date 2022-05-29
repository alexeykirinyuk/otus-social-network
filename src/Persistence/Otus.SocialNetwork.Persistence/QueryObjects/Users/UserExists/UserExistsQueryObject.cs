using System.Data;
using Dapper;
using Otus.SocialNetwork.Persistence.Abstranctions.Users;

namespace Otus.SocialNetwork.Persistence.QueryObjects.Users.UserExists;

public sealed class UserExistsQueryObject : IUserExistsQueryObject
{
    private readonly IDbConnection _db;

    public UserExistsQueryObject(IDbConnection db)
    {
        _db = db;
    }

    public Task<bool> ExistsByUsernameAsync(string username, CancellationToken ct)
    {
        return _db.QuerySingleAsync<bool>(
            new(
                UserExistsSql.CHECK_USER_EXISTS_BY_USERNAME,
                new {username},
                cancellationToken: ct
            ));
    }
}