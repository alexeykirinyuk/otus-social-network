using System.Data;

namespace Otus.SocialNetwork.Persistence.MediatR;

public interface IUnitOfWorkFactory
{
    Task BeginTransactionAsync(
        IsolationLevel isolationLevel,
        CancellationToken ct);

    IDbTransaction GetDefault();

    Task CommitTransactionAsync(CancellationToken ct);

    Task RollbackTransactionAsync(CancellationToken ct);

    Task CloseAsync(CancellationToken ct);
}