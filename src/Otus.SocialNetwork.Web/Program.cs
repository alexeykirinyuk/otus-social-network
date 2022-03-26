using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Otus.SocialNetwork.Web;
using Otus.SocialNetwork.Web.API;
using Otus.SocialNetwork.Web.Utils;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseAddress = new Uri(builder.Configuration.GetValue<string>("api:baseUrl"));
builder.Services.AddScoped((_) => new HttpClient { BaseAddress = apiBaseAddress });

builder.Services.AddScoped<ISocialNetworkAdapter, SocialNetworkAdapter>();
builder.Services.AddScoped<ILocalStorage, LocalStorage>();

await builder.Build().RunAsync();