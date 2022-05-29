using Otus.SocialNetwork.Domain;

namespace Otus.SocialNetwork.Persistence.Abstranctions.Users;

public sealed record PlainUserRecord(
    string Username,
    string? FirstName,
    string? LastName,
    DateTime? DateOfBirth,
    Sex? Sex,
    long? CityId,
    DateTime CreatedAt
);

public sealed record GetPlainUsersParams(
    string? FirstNamePrefix,
    string? LastNamePrefix,
    int? Limit,
    long? Offset
);

public interface IGetPlainUsersQueryObject
{
    Task<IReadOnlyList<PlainUserRecord>> GetListAsync(
        GetPlainUsersParams @params,
        CancellationToken ct);
}