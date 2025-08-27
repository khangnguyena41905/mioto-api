using MIOTO.CONTRACT.Abstractions.Message;
using MIOTO.DOMAIN.Abstractions.Entities;

namespace MIOTO.DOMAIN.Abstractions.Aggregate;

public class AggregateRoot<T> : Entity<T>
{
    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    public IReadOnlyCollection<IDomainEvent> GetDomainEvents => _domainEvents.ToList();
    public void ClearDomainEvents() => _domainEvents.Clear();
    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);    
}