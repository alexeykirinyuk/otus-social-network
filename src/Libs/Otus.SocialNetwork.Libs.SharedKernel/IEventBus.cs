namespace Otus.SocialNetwork.Libs.SharedKernel;

public interface IEventBus
{
    Task PublishAllAsync(IEnumerable<DomainEvent> domainEvents, CancellationToken ct);
}