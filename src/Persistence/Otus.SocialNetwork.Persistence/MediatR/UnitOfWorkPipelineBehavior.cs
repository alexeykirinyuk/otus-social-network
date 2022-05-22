using System.Data;
using System.Reflection;
using MediatR;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Persistence.MediatR;

public sealed class UnitOfWorkPipelineBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public UnitOfWorkPipelineBehavior(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<TResult> Handle(
        TRequest request,
        CancellationToken ct,
        RequestHandlerDelegate<TResult> next)
    {
        var unitOfWorkAttribute = (UnitOfWorkAttribute?)typeof(TRequest)
            .GetCustomAttribute(typeof(UnitOfWorkAttribute));

        if (unitOfWorkAttribute is null)
        {
            return await next();
        }

        await _unitOfWorkFactory.BeginTransactionAsync(
            unitOfWorkAttribute.DatabaseType,
            unitOfWorkAttribute.IsolationLevel,
            ct);

        try
        {
            var result = await next();
            await _unitOfWorkFactory.CommitTransactionAsync(ct);

            return result;
        }
        catch
        {
            await _unitOfWorkFactory.RollbackTransactionAsync(ct);
            throw;
        }
        finally
        {
            await _unitOfWorkFactory.CloseAsync(ct);
        }
    }
}