using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace Otus.SocialNetwork.Persistence;

public sealed class DatabaseMigrator : IMigrator
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<DatabaseMigrator> _logger;

    public DatabaseMigrator(
        IConfiguration configuration,
        ILogger<DatabaseMigrator> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task MigrateAsync()
    {
        await using var cnx = new MySqlConnection(_configuration.GetConnectionString("Default"));

        var counter = 0;
        while (true)
        {
            try
            {
                await cnx.OpenAsync();
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when open connection.");
                await Task.Delay(2_000);

                counter++;
            }
        }

        _logger.LogInformation("Connected to database successfully");

        try
        {
            var evolve = new Evolve.Evolve(cnx, msg => _logger.LogInformation(msg))
            {
                Locations = new[] { "Migrations" },
                IsEraseDisabled = true,
            };

            evolve.Migrate();
        }
        catch
            (Exception ex)
        {
            _logger.LogCritical(ex, "Database migration failed.");
            throw;
        }
    }
}