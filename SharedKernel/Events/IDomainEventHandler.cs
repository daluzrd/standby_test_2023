using MediatR;

namespace SharedKernel.Events;

public interface IDomainEventHandler<in TInput> : INotificationHandler<TInput> where TInput : DomainEvent
{
    
}