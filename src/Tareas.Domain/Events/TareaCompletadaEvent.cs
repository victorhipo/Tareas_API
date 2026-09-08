using MediatR;

namespace Tareas.Domain.Events;

public record TareaCompletadaEvent(
    Guid Id,
    string Title,
    DateTime OcurredOn
) : INotification;