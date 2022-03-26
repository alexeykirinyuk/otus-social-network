namespace Otus.SocialNetwork.Web.API.Models;

public sealed class Login
{
    public sealed record Request(string Username, string Password);
    public sealed record Response(string Token);
}