namespace Otus.SocialNetwork.Exceptions;

public sealed class AuthenticationFailedException : Exception
{
    public AuthenticationFailedException(string username)
        : base($"Authentication failed for user '{username}'")
    {
    }
}