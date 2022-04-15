using System.Data;
using Dapper;
using Otus.SocialNetwork.Persistence.Abstranctions;
using Otus.SocialNetwork.Persistence.QueryObjects.GetUsers;
using UserRecord = Otus.SocialNetwork.Persistence.QueryObjects.GetUsers.Records.UserRecord;

namespace Otus.SocialNetwork.Persistence.QueryObjects.GetPlainUsers;

internal sealed class GetPlainUsersQueryObject : IGetPlainUsersQueryObject
{
    private readonly IDbConnection _db;

    public GetPlainUsersQueryObject(IDbConnection db)
    {
        _db = db;
    }

    private static (string Sql, object Parameters) BuildQuery(GetPlainUsersParams @params)
    {
        var predicates = new List<string>();
        var parameters = new DynamicParameters();

        if (@params?.LastNamePrefix is not null)
        {
            predicates.Add("last_name LIKE @lastName");
            parameters.Add("@lastName", $"{@params.LastNamePrefix}%");
        }

        if (@params?.FirstNamePrefix is not null)
        {
            predicates.Add("first_name LIKE @firstName");
            parameters.Add("@firstName", $"{@params.FirstNamePrefix}%");
        }

        var wherePredicate = predicates.Any()
            ? "WHERE " + string.Join(" AND ", predicates.Select(predicate => $"({predicate})"))
            : string.Empty;

        var limitOffsetList = new List<string>();

        if (@params?.Limit is not null)
        {
            limitOffsetList.Add("LIMIT @limit");
            parameters.Add("@limit", @params.Limit.Value);
        }

        if (@params?.Offset is not null)
        {
            limitOffsetList.Add("OFFSET @offset");
            parameters.Add("@offset", @params.Offset.Value);
        }

        var limitOffset = string.Join(" ", limitOffsetList);

        var sql = string.Format(GetUsersSql.GET_USERS, wherePredicate, limitOffset);

        return (sql, parameters);
    }

    public async Task<IReadOnlyList<PlainUserRecord>> GetListAsync(
        GetPlainUsersParams @params,
        CancellationToken ct)
    {
        var query = BuildQuery(@params);

        var users = await _db.QueryAsync<UserRecord>(
            new(query.Sql, query.Parameters, cancellationToken: ct));

        var userList = users.AsList();

        return userList
            .Select(userRecord => new PlainUserRecord(
                userRecord.Username,
                userRecord.FirstName,
                userRecord.LastName,
                userRecord.DateOfBirth,
                userRecord.Sex,
                userRecord.CityId,
                userRecord.CreatedAt))
            .ToArray();
    }
}