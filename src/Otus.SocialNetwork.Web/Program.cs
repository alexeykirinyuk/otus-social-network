using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Otus.SocialNetwork.Web;
using Otus.SocialNetwork.Web.API;
using Otus.SocialNetwork.Web.Utils;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseUrl = builder.Configuration.GetValue<string>("api:baseUrl");
baseUrl = string.IsNullOrWhiteSpace(baseUrl) ? builder.HostEnvironment.BaseAddress : baseUrl;

builder.Services.AddScoped((_) => new HttpClient { BaseAddress = new Uri(baseUrl) });

builder.Services.AddScoped<ISocialNetworkAdapter, SocialNetworkAdapter>();
builder.Services.AddScoped<ILocalStorage, LocalStorage>();

await builder.Build().RunAsync();