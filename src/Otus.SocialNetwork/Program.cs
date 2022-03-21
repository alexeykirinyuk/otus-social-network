using Microsoft.AspNetCore;
using Otus.SocialNetwork;

var app = WebHost.CreateDefaultBuilder(args)
    .UseStartup<Startup>()
    .Build();

await app.RunAsync();