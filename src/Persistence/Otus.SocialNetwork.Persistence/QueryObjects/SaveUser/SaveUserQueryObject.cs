using System.Data;
using Dapper;
using Otus.SocialNetwork.Domain;
using Otus.SocialNetwork.Persistence.Abstranctions;
using Otus.SocialNetwork.Persistence.QueryObjects.SaveUser.Records;

namespace Otus.SocialNetwork.Persistence.QueryObjects.SaveUser;

public sealed class SaveUserQueryObject : ISaveUserQueryObject
{
    private readonly IDbConnection _db;

    public SaveUserQueryObject(IDbConnection db)
    {
        _db = db;
    }

    public async Task SaveAsync(User user, CancellationToken ct)
    {
        var cityId = await GetOrCreateCityIdAsync(user.City, ct);

        await SaveInterestsAsync(user, ct);

        await _db.ExecuteAsync(new(
            SaveUsersSql.INSERT_OR_UPDATE_USER,
            new
            {
                username = user.Username,
                firstName = user.FirstName,
                lastName = user.LastName,
                dateOfBirth = user.DateOfBirth,
                sex = user.Sex,
                cityId,
                passwordHash = user.PasswordHash,
                createdAt = user.CreatedAt
            }));
    }

    private async Task<long?> GetOrCreateCityIdAsync(string? cityName, CancellationToken ct)
    {
        if (cityName is null)
        {
            return null;
        }

        var cityId = await _db.QueryFirstOrDefaultAsync<long>(
            new(
                SaveUsersSql.GET_CITY_ID_BY_NAME,
                new
                {
                    name = cityName
                },
                cancellationToken: ct
            ));

        if (cityId is default(long))
        {
            return await _db.QuerySingleAsync<long>(
                new(
                    SaveUsersSql.INSERT_CITY,
                    new
                    {
                        name = cityName
                    },
                    cancellationToken: ct)
            );
        }

        return cityId;
    }

    private async Task SaveInterestsAsync(User user, CancellationToken ct)
    {
        var currentInterests = (await _db.QueryAsync<InterestRecord>(
                new(
                    SaveUsersSql.GET_USER_INTERESTS,
                    new {username = user.Username},
                    cancellationToken: ct)))
            .AsList();

        var interestsToRemove = currentInterests
            .Where(interest => !user.Interests.Contains(interest.Name))
            .Select(interest => interest.Id)
            .ToArray();

        if (interestsToRemove.Any())
        {
            await _db.ExecuteAsync(
                new(
                    SaveUsersSql.DELETE_USER_INTERESTS,
                    new {userId = user.Username, interestIds = interestsToRemove},
                    cancellationToken: ct));
        }

        var interestsToInsert = user.Interests
            .Where(interest => currentInterests.All(curInterest => curInterest.Name != interest))
            .ToArray();

        if (interestsToInsert.Any())
        {
            await InsertUserInterests(user, interestsToInsert, ct);
        }
    }

    private async Task InsertUserInterests(
        User user,
        string[] newUserInterestNames,
        CancellationToken ct)
    {
        var userInterestsToInsert = (await _db.QueryAsync<InterestRecord>(
                new(SaveUsersSql.GET_INTERESTS_BY_NAMES, new {names = newUserInterestNames},
                    cancellationToken: ct)))
            .ToList();

        var notFoundInterestEntities = newUserInterestNames
            .Where(name => userInterestsToInsert.All(interest => interest.Name != name))
            .ToArray();

        foreach (var notFoundInterest in notFoundInterestEntities)
        {
            var newInterestId = await _db.QuerySingleAsync<long>(
                new(SaveUsersSql.ADD_INTEREST, new {name = notFoundInterest}, cancellationToken: ct));

            userInterestsToInsert.Add(new InterestRecord
            {
                Id = newInterestId,
                Name = notFoundInterest
            });
        }

        if (userInterestsToInsert.Any())
        {
            var query = string.Format(
                SaveUsersSql.ADD_USER_INTERESTS,
                string.Join(",", userInterestsToInsert.Select((_, ind) => $"(@username, @interestId{ind})"))
            );

            var parameters = new DynamicParameters();
            parameters.Add("@username", user.Username);
            foreach (var (index, userInterest) in userInterestsToInsert.Select((userInterest, index) =>
                         (index, userInterest)))
            {
                parameters.Add($"@interestId{index}", userInterest.Id);
            }

            await _db.ExecuteAsync(
                new(query, parameters, cancellationToken: ct));
        }
    }
}