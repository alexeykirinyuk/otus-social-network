namespace Otus.SocialNetwork.Persistence.QueryObjects.Users.GetUserPasswordHash.Records;

public sealed class UserPasswordHashRecord
{
    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;
}