using System.Data;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Persistence.MediatR;

public interface IUnitOfWorkFactory
{
    Task BeginTransactionAsync(
        DatabaseType databaseType,
        IsolationLevel isolationLevel,
        CancellationToken ct);

    IDbTransaction GetDefault();

    Task CommitTransactionAsync(CancellationToken ct);

    Task RollbackTransactionAsync(CancellationToken ct);

    Task CloseAsync(CancellationToken ct);
}