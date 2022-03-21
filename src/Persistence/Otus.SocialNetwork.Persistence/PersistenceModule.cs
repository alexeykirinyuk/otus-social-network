using System.Data;

namespace Otus.SocialNetwork.Persistence;

public static class PersistenceModule
{
}

public sealed class UnitOfWorkFactory : IDisposable
{
    private IDbConnection? _db;
    private IDbTransaction? _dbTransaction;

    public IDbTransaction BeginTransaction()
    {
        _db = new SqlCon
    }
    
    
    public IDbConnection GetDefault()
    {
        if (_db is null)
        {
            // TODO: Initialize connection
            _db = null!;
        }

        return _db;
    }

    public void Dispose()
    {
        _db?.Dispose();
        _dbTransaction?.Dispose();
    }
}