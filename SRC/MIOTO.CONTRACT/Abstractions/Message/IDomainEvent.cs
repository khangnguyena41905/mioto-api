using MediatR;

namespace MIOTO.CONTRACT.Abstractions.Message;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}