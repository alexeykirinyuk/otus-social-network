using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Otus.SocialNetwork.Web;
using Otus.SocialNetwork.Web.API;
using Otus.SocialNetwork.Web.States;
using Otus.SocialNetwork.Web.Storages;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseUrl = builder.Configuration.GetValue<string>("api:baseUrl");
baseUrl = string.IsNullOrWhiteSpace(baseUrl) ? builder.HostEnvironment.BaseAddress : baseUrl;

builder.Services.AddScoped((_) => new HttpClient { BaseAddress = new Uri(baseUrl) });

builder.Services.AddSingleton<ISocialNetworkAdapter, SocialNetworkAdapter>();
builder.Services.AddSingleton<ILocalStorage, LocalStorage>();
builder.Services.AddSingleton<ITokenStorage, TokenStorage>();

builder.Services.AddSingleton<IAuthenticationState, AuthenticationState>();

await builder.Build().RunAsync();