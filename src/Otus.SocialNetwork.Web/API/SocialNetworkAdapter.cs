using System.Net.Http.Json;
using Otus.SocialNetwork.Web.API.Models;
using Otus.SocialNetwork.Web.Storages;

namespace Otus.SocialNetwork.Web.API;

public sealed class SocialNetworkAdapter : ISocialNetworkAdapter
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorage _tokenStorage;

    public SocialNetworkAdapter(
        HttpClient httpClient,
        ITokenStorage tokenStorage)
    {
        _httpClient = httpClient;
        _tokenStorage = tokenStorage;
    }

    public Task RegisterAsync(Register.Request request, CancellationToken ct)
    {
        return _httpClient.PostAsJsonAsync("/api/users/register", request, ct);
    }

    public async Task<Login.Response> LoginAsync(string username, string password, CancellationToken ct)
    {
        var request = new Login.Request(username, password);
        var responseMessage = await _httpClient.PostAsJsonAsync("/api/users/login", request, ct);

        return await responseMessage.Content.ReadFromJsonAsync<Login.Response>(cancellationToken: ct)
               ?? throw ResponseCantBeNullException();
    }

    public async Task<bool> IsLoggedIn(CancellationToken ct)
    {
        try
        {
            var response = await MakeRequest(
                HttpMethod.Get,
                "/api/users/username",
                null,
                ct);
            response.EnsureSuccessStatusCode();
            var username = await response.Content.ReadAsStringAsync(ct);

            return !string.IsNullOrWhiteSpace(username);
        }
        catch
        {
            return false;
        }
    }

    public async Task<GetUsers.Response> GetUsersAsync(GetUsers.Request request, CancellationToken ct)
    {
        var response = await MakeRequest(
            HttpMethod.Get,
            "/api/users",
            null,
            ct);

        var result = await response.Content.ReadFromJsonAsync<GetUsers.Response>(cancellationToken: ct);
        return result ?? throw ResponseCantBeNullException();
    }

    public Task BeFriendsAsync(string friendUsername, CancellationToken ct)
    {
        return MakeRequest(HttpMethod.Put, "/api/users/friend", new BeFriends.Request(friendUsername), ct);
    }

    public Task StopBeingFriendsAsync(string friendUsername, CancellationToken ct)
    {
        return MakeRequest(HttpMethod.Delete, "/api/users/friend", new StopBeingFriends.Request(friendUsername), ct);
    }

    private async Task<HttpResponseMessage> MakeRequest(
        HttpMethod method,
        string path,
        object? body,
        CancellationToken ct)
    {
        var jwt = await _tokenStorage.Get(ct);

        var requestMessage = new HttpRequestMessage(
            method,
            path);
        requestMessage.Headers.Add("Authorization", $"Bearer {jwt}");

        if (body is not null)
        {
            requestMessage.Content = JsonContent.Create(body);
        }

        var responseMessage = await _httpClient.SendAsync(requestMessage, ct);
        responseMessage.EnsureSuccessStatusCode();

        return responseMessage;
    }

    private static InvalidOperationException ResponseCantBeNullException()
    {
        return new InvalidOperationException("Response can't be null");
    }
}