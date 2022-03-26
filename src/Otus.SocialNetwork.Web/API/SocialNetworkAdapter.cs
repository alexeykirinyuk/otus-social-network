using System.Net.Http.Json;
using System.Text.Json;
using Otus.SocialNetwork.Web.API.Models;
using Otus.SocialNetwork.Web.Utils;

namespace Otus.SocialNetwork.Web.API;

public sealed class SocialNetworkAdapter : ISocialNetworkAdapter
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorage _localStorage;

    public SocialNetworkAdapter(
        HttpClient httpClient,
        ILocalStorage localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public Task RegisterAsync(Register.Request request, CancellationToken ct)
    {
        return _httpClient.PostAsJsonAsync("/users/register", request, ct);
    }

    public async Task<Login.Response> LoginAsync(string username, string password, CancellationToken ct)
    {
        var request = new Login.Request(username, password);
        var responseMessage = await _httpClient.PostAsJsonAsync("/users/login", request, ct);

        return await responseMessage.Content.ReadFromJsonAsync<Login.Response>(cancellationToken: ct)
               ?? throw ResponseCantBeNullException();
    }

    public async Task<GetUsers.Response> GetUsersAsync(GetUsers.Request request, CancellationToken ct)
    {
        var jwt = await _localStorage.GetItemAsync<string>("jwt");

        var requestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "/users");
        requestMessage.Headers.Add("Authorization", $"Bearer {jwt}");

        var responseMessage = await _httpClient.SendAsync(requestMessage, ct);

        responseMessage.EnsureSuccessStatusCode();

        var result = await responseMessage.Content.ReadFromJsonAsync<GetUsers.Response>(cancellationToken: ct);
        return result ?? throw ResponseCantBeNullException();
    }

    public async Task FriendAsync(string friendUsername, CancellationToken ct)
    {
        var jwt = await _localStorage.GetItemAsync<string>("jwt");

        var requestMessage = new HttpRequestMessage(
            HttpMethod.Put,
            "/users/friend");
        requestMessage.Headers.Add("Authorization", $"Bearer {jwt}");
        requestMessage.Content = JsonContent.Create(new Friend.Request(friendUsername));

        var responseMessage = await _httpClient.SendAsync(requestMessage, ct);
        responseMessage.EnsureSuccessStatusCode();
    }

    private static InvalidOperationException ResponseCantBeNullException()
    {
        return new InvalidOperationException("Response can't be null");
    }
}