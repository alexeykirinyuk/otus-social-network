using MediatR;
using Otus.SocialNetwork.Libs.SharedKernel;

namespace Otus.SocialNetwork.Application.MediatR;

public sealed class MediatorEventBus : IEventBus
{
    private readonly IMediator _mediator;

    public MediatorEventBus(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task PublishAllAsync(IEnumerable<DomainEvent> domainEvents, CancellationToken ct)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent, ct);
        }
    }
}