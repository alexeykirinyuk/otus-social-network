using System.Data;
using Dapper;
using Otus.SocialNetwork.Domain;
using Otus.SocialNetwork.Persistence.Abstranctions;
using Otus.SocialNetwork.Persistence.QueryObjects.GetUsers.Records;

namespace Otus.SocialNetwork.Persistence.QueryObjects.GetUsers;

internal sealed class GetUsersQueryObject : IGetUsersQueryObject
{
    private readonly IDbConnection _db;

    public GetUsersQueryObject(IDbConnection db)
    {
        _db = db;
    }

    private static (string Sql, object Parameters) BuildQuery(GetUsersFilters? filters)
    {
        var predicates = new List<string>();
        var parameters = new DynamicParameters();

        if (filters?.Username is not null)
        {
            predicates.Add("username = @username");
            parameters.Add("@username", filters.Username);
        }

        var predicate = string.Join("AND", predicates.Select(predicate => $"({predicate})"));

        var sql = string.Format(GetUsersSql.GET_USERS, predicate);

        return (sql, parameters);
    }

    public async Task<IReadOnlyList<User>> ToListAsync(GetUsersFilters? filters, CancellationToken ct)
    {
        var query = BuildQuery(filters);

        var users = await _db.QueryAsync<UserRecord>(
            new(query.Sql, query.Parameters, cancellationToken: ct));

        var userList = users.AsList();

        var userInterests = await GetUserInterestsAsync(userList, ct);

        var cities = await GetCitiesAsync(userList, ct);

        var friends = await GetFriendsAsync(userList, ct);

        return userList
            .Select(userRecord => new User(
                userRecord.Username,
                userRecord.FirstName,
                userRecord.LastName,
                userRecord.DateOfBirth,
                userRecord.Sex,
                userInterests[userRecord.Username].ToArray(),
                userRecord.CityId.HasValue ? cities[userRecord.CityId.Value] : null,
                friends[userRecord.Username].ToArray(),
                userRecord.PasswordHash,
                userRecord.PasswordSalt,
                userRecord.CreatedAt))
            .ToArray();
    }

    public async Task<User?> SingleOrDefaultAsync(GetUsersFilters? filters, CancellationToken ct)
    {
        var query = BuildQuery(filters);

        var userRecord = await _db.QuerySingleOrDefaultAsync<UserRecord>(
            new(query.Sql, query.Parameters, cancellationToken: ct));

        if (userRecord is null)
        {
            return null;
        }

        var userList = new[] { userRecord };

        var userInterests = await GetUserInterestsAsync(userList, ct);

        var cities = await GetCitiesAsync(userList, ct);

        var friends = await GetFriendsAsync(userList, ct);

        return new User(
            userRecord.Username,
            userRecord.FirstName,
            userRecord.LastName,
            userRecord.DateOfBirth,
            userRecord.Sex,
            userInterests[userRecord.Username].ToArray(),
            userRecord.CityId.HasValue ? cities[userRecord.CityId.Value] : null,
            friends[userRecord.Username].ToArray(),
            userRecord.PasswordHash,
            userRecord.PasswordSalt,
            userRecord.CreatedAt);
    }

    private async Task<IDictionary<long, string>> GetCitiesAsync(
        IReadOnlyList<UserRecord> users,
        CancellationToken ct)
    {
        var cityIds = users
            .Select(user => user.CityId)
            .Where(cityId => cityId.HasValue)
            .Distinct()
            .ToArray();

        if (!cityIds.Any())
        {
            return new Dictionary<long, string>();
        }

        var cities = await _db.QueryAsync<CityRecord>(
            new(GetUsersSql.GET_CITIES_BY_IDS, new { cityIds }, cancellationToken: ct));

        return cities.ToDictionary(city => city.Id, city => city.Name);
    }

    private async Task<ILookup<string, string>> GetUserInterestsAsync(
        IReadOnlyList<UserRecord> users,
        CancellationToken ct)
    {
        var usernames = users.Select(user => user.Username).ToArray();

        var userInterestRecords = await _db.QueryAsync<UserInterestRecord>(
            new(GetUsersSql.GET_USER_INTERESTS_BY_USERNAMES, new { usernames }, cancellationToken: ct));

        var userInterestRecordList = userInterestRecords.AsList();

        var interestIds = userInterestRecordList
            .Select(userInterest => userInterest.InterestId)
            .Distinct();

        var interests = await _db.QueryAsync<InterestRecord>(
            new(GetUsersSql.GET_INTERESTS_BY_IDS, new { interestIds }, cancellationToken: ct));

        var interestsDictionary = interests
            .ToDictionary(interest => interest.Id, interest => interest.Name);

        return userInterestRecordList.ToLookup(
            userInterest => userInterest.Username,
            userInterest => interestsDictionary[userInterest.InterestId]);
    }

    private async Task<ILookup<string, Friend>> GetFriendsAsync(
        IReadOnlyList<UserRecord> users,
        CancellationToken ct)
    {
        var usernames = users.Select(user => user.Username).ToArray();

        var friendRecords = await _db.QueryAsync<FriendRecord>(
            new(GetUsersSql.GET_FRIENDS_BY_USERNAMES, new { usernames }, cancellationToken: ct));

        return friendRecords
            .ToLookup(
                friend => friend.UserUsername,
                friend => new Friend(friend.FriendUsername));
    }
}