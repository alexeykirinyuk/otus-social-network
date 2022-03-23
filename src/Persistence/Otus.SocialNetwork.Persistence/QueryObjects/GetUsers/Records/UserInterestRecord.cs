namespace Otus.SocialNetwork.Persistence.QueryObjects.GetUsers.Records;

internal sealed class UserInterestRecord
{
    public string Username { get; set; } = null!;

    public long InterestId { get; set; }
}