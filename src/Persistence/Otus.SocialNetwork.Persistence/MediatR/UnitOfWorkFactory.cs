using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Otus.SocialNetwork.Persistence.MediatR;

internal sealed class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IConfiguration _configuration;

    private MySqlConnection? _dbConnection;
    private MySqlTransaction? _dbTransaction;

    public UnitOfWorkFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task BeginTransactionAsync(
        IsolationLevel isolationLevel,
        CancellationToken ct)
    {
        var connectionString = _configuration.GetConnectionString("Default");
        _dbConnection = new MySqlConnection(connectionString);
        if (_dbConnection.State != ConnectionState.Open)
        {
            await _dbConnection.OpenAsync(ct);
        }

        _dbTransaction = await _dbConnection.BeginTransactionAsync(ct);
    }

    public IDbTransaction GetDefault()
    {
        if (_dbTransaction is null)
        {
            throw new InvalidOperationException("Transaction not initialized");
        }

        return _dbTransaction;
    }

    public async Task CommitTransactionAsync(CancellationToken ct)
    {
        if (_dbTransaction is null)
        {
            throw new InvalidOperationException("Transaction not initialized");
        }

        await _dbTransaction.CommitAsync(ct);
    }

    public async Task RollbackTransactionAsync(CancellationToken ct)
    {
        if (_dbTransaction is null)
        {
            throw new InvalidOperationException("Transaction not initialized");
        }

        await _dbTransaction.RollbackAsync(ct);
    }

    public async Task CloseAsync(CancellationToken ct)
    {
        if (_dbTransaction is not null)
        {
            await _dbTransaction.DisposeAsync();
        }

        if (_dbConnection is not null && _dbConnection.State != ConnectionState.Closed)
        {
            await _dbConnection.CloseAsync(ct);
        }

        _dbConnection = null;
        _dbTransaction = null;
    }
}