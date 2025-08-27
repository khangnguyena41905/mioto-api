using MediatR;

namespace MIOTO.CONTRACT.Abstractions.Message;

public interface IDomainHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}