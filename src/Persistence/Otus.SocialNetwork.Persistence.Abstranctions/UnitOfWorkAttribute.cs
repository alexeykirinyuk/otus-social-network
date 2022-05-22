using System.Data;

namespace Otus.SocialNetwork.Persistence.Abstranctions;

public sealed class UnitOfWorkAttribute : Attribute
{
    public UnitOfWorkAttribute(IsolationLevel isolationLevel, DatabaseType databaseType = DatabaseType.Default)
    {
        IsolationLevel = isolationLevel;
        DatabaseType = databaseType;
    }

    public IsolationLevel IsolationLevel { get; }
    public DatabaseType DatabaseType { get; }
}