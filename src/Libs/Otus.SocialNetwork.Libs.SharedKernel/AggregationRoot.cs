namespace Otus.SocialNetwork.Libs.SharedKernel;

public abstract class AggregationRoot
{
    private readonly List<DomainEvent> _domainEvents;

    protected AggregationRoot()
    {
        _domainEvents = new();
    }

    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IReadOnlyList<DomainEvent> PopDomainEvents()
    {
        var events = new List<DomainEvent>(_domainEvents);
        _domainEvents.Clear();

        return events;
    }
}