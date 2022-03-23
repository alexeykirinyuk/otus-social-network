namespace Otus.SocialNetwork.Application.Exceptions;

public sealed class UserNotFoundException : OtusApplicationException
{
    public UserNotFoundException(string username)
        : base($"User with username '{username}' not found.")
    {
    }
}