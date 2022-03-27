namespace Otus.SocialNetwork.Persistence;

public interface IMigrator
{
    Task MigrateAsync();
}