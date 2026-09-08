using MediatR;

namespace Tareas.Domain.Events;

public record TareaBorradaEvent(
    Guid Id,
    string Title,
    DateTime OcurredOn
) : INotification;