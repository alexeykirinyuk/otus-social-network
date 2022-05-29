namespace Otus.SocialNetwork.Persistence.Abstranctions.Users;

public sealed record UserRecord(
    string Username,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    long CityId,
    string PasswordHash,
    string PasswordSalt,
    DateTime CreatedAt
);

public interface ISaveUserBulkQueryObject
{
    Task Save(IReadOnlyList<UserRecord> users, CancellationToken ct);
}