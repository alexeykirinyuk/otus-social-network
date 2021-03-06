using Otus.SocialNetwork.Web.API.Models;

namespace Otus.SocialNetwork.Web.API;

public interface ISocialNetworkAdapter
{
    Task RegisterAsync(Register.Request request, CancellationToken ct);

    Task<Login.Response> LoginAsync(string username, string password, CancellationToken ct);

    Task<GetUsers.Response> GetUsersAsync(GetUsers.Request request, CancellationToken ct);

    Task BeFriendsAsync(string friendUsername, CancellationToken ct);

    Task StopBeingFriendsAsync(string friendUsername, CancellationToken ct);

    Task<bool> IsLoggedIn(CancellationToken ct);
}