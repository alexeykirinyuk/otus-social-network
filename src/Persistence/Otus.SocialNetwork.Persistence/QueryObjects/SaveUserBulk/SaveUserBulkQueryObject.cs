using System.Data;
using Dapper;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Persistence.QueryObjects.SaveUserBulk;

public sealed class SaveUserBulkQueryObject : ISaveUserBulkQueryObject
{
    private readonly IDbConnection _db;

    public SaveUserBulkQueryObject(IDbConnection db)
    {
        _db = db;
    }

    public async Task Save(IReadOnlyList<UserRecord> users, CancellationToken ct)
    {
        if (!users.Any())
        {
            return;
        }
        
        var query = string.Format(
            SaveUserBulkSql.INSERT_USERS_BULK,
            string.Join(",", users.Select((_, index) =>
                $"(@username{index}, @firstName{index}, @lastName{index}, @dateOfBirth{index}, 1, @cityId{index}, " +
                $"@passwordHash{index}, @passwordSalt{index}, @createdAt{index})"))
        );

        var parameters = new DynamicParameters();

        for (var index = 0; index < users.Count; index++)
        {
            var user = users[index];
            parameters.Add($"@username{index}", user.Username);
            parameters.Add($"@firstName{index}", user.FirstName);
            parameters.Add($"@lastName{index}", user.LastName);
            parameters.Add($"@dateOfBirth{index}", user.DateOfBirth);
            parameters.Add($"@cityId{index}", user.CityId);
            parameters.Add($"@passwordHash{index}", user.PasswordHash);
            parameters.Add($"@passwordSalt{index}", user.PasswordSalt);
            parameters.Add($"@createdAt{index}", user.CreatedAt);
        }

        await _db.ExecuteAsync(
            new(query, parameters, cancellationToken: ct));
    }
}