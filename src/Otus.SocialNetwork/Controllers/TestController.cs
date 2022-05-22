using System.Data;
using System.Diagnostics;
using System.Globalization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Otus.SocialNetwork.Application.Features.Users.RegisterUser;
using Otus.SocialNetwork.Infrastructure.Authorization;
using Otus.SocialNetwork.Persistence.Abstranctions;
using Otus.SocialNetwork.Persistence.MediatR;

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
                scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory>(),
                scope.ServiceProvider,
                scope.ServiceProvider.GetRequiredService<ILogger<TestController>>(),
                CancellationToken.None
            );
        });
    }

    private static async Task GenerateData(
        IPasswordHashService passwordHashService,
        IUnitOfWorkFactory factory,
        IServiceProvider services,
        ILogger<TestController> logger,
        CancellationToken ct)
    {
        var (hash, salt) = passwordHashService.CreateHash("temp-123");

        using var httpClient = new HttpClient();

        var count = await GetCurrentCount(factory, services, ct);
        while (count < 1_000_000)
        {
            var persons = await Get100Users(httpClient, logger, ct);
            var records = persons
                .SelectMany(person =>
                {
                    return Enumerable.Range(0, 30)
                        .Select(_ =>
                        {
                            var guidPostfix = Guid.NewGuid().ToString();

                            return new UserRecord(
                                $"{person.Login}_{guidPostfix}",
                                person.FirstName,
                                person.LastName,
                                DateTime.ParseExact(person.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                                1,
                                hash,
                                salt,
                                DateTime.UtcNow);
                        });
                })
                .ToArray();

            await Save(services, factory, records, logger, ct);

            count += records.Length;

            logger.LogInformation("Generated {Count} users", count);
        }
    }

    private static async Task<long> GetCurrentCount(IUnitOfWorkFactory factory, IServiceProvider services,
        CancellationToken ct)
    {
        await factory.BeginTransactionAsync(
            DatabaseType.Default,
            IsolationLevel.ReadCommitted,
            ct);
        try
        {
            var count = await services
                .GetRequiredService<IGetUsersQueryObject>()
                .CountAsync(ct);
            await factory.CommitTransactionAsync(ct);

            return count;
        }
        catch
        {
            await factory.RollbackTransactionAsync(ct);
            throw;
        }
        finally
        {
            await factory.CloseAsync(ct);
        }
    }

    private static async Task Save(
        IServiceProvider services,
        IUnitOfWorkFactory factory,
        IReadOnlyList<UserRecord> records,
        ILogger logger,
        CancellationToken ct)
    {
        var watch = Stopwatch.StartNew();

        await factory.BeginTransactionAsync(
            DatabaseType.Default,
            IsolationLevel.ReadCommitted,
            ct);
        try
        {
            await services
                .GetRequiredService<ISaveUserBulkQueryObject>()
                .Save(
                    records,
                    ct);
            await factory.CommitTransactionAsync(ct);

            watch.Stop();
            logger.LogInformation($"Save 1000 users: {watch.Elapsed.TotalSeconds}");
        }
        catch
        {
            await factory.RollbackTransactionAsync(ct);
            throw;
        }
        finally
        {
            await factory.CloseAsync(ct);
        }
    }

    private static async Task<IReadOnlyList<Person>> Get100Users(
        HttpClient httpClient,
        ILogger logger,
        CancellationToken ct)
    {
        var watch = Stopwatch.StartNew();

        var result = await httpClient.GetAsync(
            "https://api.randomdatatools.ru?count=100&params=FirstName,LastName,Login,DateOfBirth",
            ct);
        var array = await result.Content.ReadFromJsonAsync<Person[]>(cancellationToken: ct);

        watch.Stop();
        logger.LogInformation($"Get 100 users: {watch.Elapsed.TotalSeconds}");

        return array ?? Array.Empty<Person>();
    }

    private sealed class Person
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Login { get; set; } = null!;

        public string DateOfBirth { get; set; } = null!;
    }
}