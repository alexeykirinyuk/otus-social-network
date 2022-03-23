namespace Otus.SocialNetwork.Domain;

public sealed class Friend
{
    public string Username { get; }

    public Friend(string username)
    {
        Username = username;
    }

    public static Friend Create(User user)
    {
        return new Friend(user.Username);
    }
}