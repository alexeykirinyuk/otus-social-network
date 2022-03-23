namespace Otus.SocialNetwork.Persistence.QueryObjects.GetUsers.Records;

public sealed class FriendRecord
{
    public string UserUsername { get; set; } = null!;

    public string FriendUsername { get; set; } = null!;
}