namespace Otus.SocialNetwork.Application.Exceptions;

public class OtusApplicationException : Exception
{
    public OtusApplicationException(string message)
        : base(message)
    {
    }
}