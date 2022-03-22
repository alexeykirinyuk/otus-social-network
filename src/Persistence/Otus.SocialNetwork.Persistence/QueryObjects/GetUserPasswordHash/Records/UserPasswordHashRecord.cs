namespace Otus.SocialNetwork.Persistence.QueryObjects.GetUserPasswordHash.Records;

public sealed class UserPasswordHashRecord
{
    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;
}