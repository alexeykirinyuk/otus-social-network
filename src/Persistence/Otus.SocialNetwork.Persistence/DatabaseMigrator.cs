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
    
    public void Migrate()
    {
        try
        {
            using var cnx = new MySqlConnection(_configuration.GetConnectionString("Default"));
            var evolve = new Evolve.Evolve(cnx, msg => _logger.LogInformation(msg))
            {
                Locations = new[] { "Migrations" },
                IsEraseDisabled = true,
            };

            evolve.Migrate();
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Database migration failed.");
            throw;
        }
    }
}