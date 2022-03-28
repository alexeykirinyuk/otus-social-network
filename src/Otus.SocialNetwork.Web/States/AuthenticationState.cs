using Microsoft.AspNetCore.Components;
using Otus.SocialNetwork.Web.API;
using Otus.SocialNetwork.Web.Storages;

namespace Otus.SocialNetwork.Web.States;

public sealed class AuthenticationState : IAuthenticationState
{
    private readonly ITokenStorage _tokenStorage;
    private readonly ISocialNetworkAdapter _socialNetworkAdapter;
    private readonly NavigationManager _navigationManager;

    public async Task<bool> IsLoggedIn(CancellationToken ct)
    {
        var token = await _tokenStorage.Get(ct);
        return !string.IsNullOrWhiteSpace(token);
    }

    public event Action<bool>? IsLoggedInChanged;

    public AuthenticationState(
        ITokenStorage tokenStorage,
        ISocialNetworkAdapter socialNetworkAdapter,
        NavigationManager navigationManager)
    {
        _tokenStorage = tokenStorage;
        _socialNetworkAdapter = socialNetworkAdapter;
        _navigationManager = navigationManager;
    }

    public async Task LogIn(string username, string password, CancellationToken ct)
    {
        var loginResponse = await _socialNetworkAdapter.LoginAsync(username, password, ct);
        await _tokenStorage.Save(loginResponse.Token, ct);
        OnIsLoggedInChanged(true);
        _navigationManager.NavigateTo("/users");
    }

    public async Task LogOut(CancellationToken ct)
    {
        await _tokenStorage.Remove(ct);
        OnIsLoggedInChanged(false);
        _navigationManager.NavigateTo("/users/login");
    }

    public async Task<bool> RedirectIfNotAuthorized(CancellationToken ct)
    {
        if (!await IsLoggedIn(ct))
        {
            _navigationManager.NavigateTo("/users/login");
            return true;
        }

        return false;
    }

    private void OnIsLoggedInChanged(bool isLoggedIn)
    {
        IsLoggedInChanged?.Invoke(isLoggedIn);
    }
}