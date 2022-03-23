using Otus.SocialNetwork.Domain;

namespace Otus.SocialNetwork.Persistence.QueryObjects.GetUsers.Records;

internal sealed class UserRecord
{
    public string Username { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Sex? Sex { get; set; }
    public long? CityId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
}