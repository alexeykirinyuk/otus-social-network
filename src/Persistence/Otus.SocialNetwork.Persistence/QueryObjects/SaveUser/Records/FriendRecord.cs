namespace Otus.SocialNetwork.Persistence.QueryObjects.SaveUser.Records;

internal sealed class FriendRecord
{
    public string UserUsername { get; set; } = null!;
    
    public string FriendUsername { get; set; } = null!;
}