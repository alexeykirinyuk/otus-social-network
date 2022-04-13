using System.Globalization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Otus.SocialNetwork.Application.Features.Users.RegisterUser;
using Otus.SocialNetwork.Infrastructure.Authorization;

namespace Otus.SocialNetwork.Controllers;

[ApiController]
[Route("api/test")]
public sealed class TestController : ControllerBase
{
    private readonly IServiceProvider _provider;

    public TestController(IServiceProvider provider)
    {
        _provider = provider;
    }

    [HttpPost("generate-data")]
    public void GenerateData()
    {
        Task.Run(async () =>
        {
            using var scope = _provider.CreateScope();

            await GenerateData(
                scope.ServiceProvider.GetRequiredService<IPasswordHashService>(),
                scope.ServiceProvider.GetRequiredService<IMediator>(),
                scope.ServiceProvider.GetRequiredService<ILogger<TestController>>()
            );
        });
    }

    private static async Task GenerateData(
        IPasswordHashService passwordHashService,
        IMediator mediator,
        ILogger<TestController> logger)
    {
        var (hash, salt) = passwordHashService.CreateHash("temp-123");

        using var httpClient = new HttpClient();

        var count = 0;
        while (count < 1_000_000)
        {
            var result =
                await httpClient.GetAsync(
                    "https://api.randomdatatools.ru/?count=100&params=FirstName,LastName,Login,DateOfBirth");
            var persons = await result.Content.ReadFromJsonAsync<Person[]>();

            if (persons is null)
            {
                continue;
            }

            foreach (var person in persons)
            {
                var usernamePostfix = new string(Guid.NewGuid().ToString().Take(5).ToArray());
                await mediator.Send(new RegisterUserCommand(
                    $"{person.Login}_{usernamePostfix}",
                    person.FirstName,
                    person.LastName,
                    DateTime.ParseExact(person.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    null,
                    Array.Empty<string>(),
                    "Москва",
                    hash,
                    salt
                ));
                count++;
            }
            
            logger.LogInformation("Generated {Count} users", count);
        }
    }

    private sealed class Person
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Login { get; set; } = null!;

        public string DateOfBirth { get; set; } = null!;
    }
}