namespace Otus.SocialNetwork.Application.Libs;

public interface IUseCase<in TRequest>
{
    Task Handle(TRequest request, CancellationToken ct);
}

public interface IUseCase<in TRequest, TResult>
{
    Task<TResult> Handle(TRequest request, CancellationToken ct);
}