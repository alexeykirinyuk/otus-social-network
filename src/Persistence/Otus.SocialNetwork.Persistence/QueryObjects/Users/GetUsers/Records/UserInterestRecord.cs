namespace Otus.SocialNetwork.Persistence.QueryObjects.Users.GetUsers.Records;

internal sealed class UserInterestRecord
{
    public string Username { get; set; } = null!;

    public long InterestId { get; set; }
}