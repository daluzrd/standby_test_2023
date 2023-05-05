using MediatR;

namespace SharedKernel.Events;

public class DomainEvent : INotification
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime DateOccurred { get; protected set; } = DateTime.Now;
}