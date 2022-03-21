namespace Otus.SocialNetwork.Application.Exceptions;

public sealed class UsernameAlreadyExistsException : OtusApplicationException
{
    public UsernameAlreadyExistsException(string username)
        : base($"User with username '{username}' already exists")
    {
    }
}