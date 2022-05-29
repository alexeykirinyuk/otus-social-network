using MediatR;

namespace Otus.SocialNetwork.Libs.SharedKernel;

public abstract record DomainEvent : INotification
{
    public Guid EventId { get; }
    
    public DateTime OccurredAt { get; }
    
    protected DomainEvent()
    {
        EventId = Guid.NewGuid();
        OccurredAt = DateTime.UtcNow;
    }
}