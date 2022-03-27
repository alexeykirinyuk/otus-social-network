namespace Otus.SocialNetwork.Web.API.Models;

public sealed class BeFriends
{
    public sealed record Request(string FriendUsername);
}

public sealed class StopBeingFriends
{
    public sealed record Request(string FriendUsername);
}