using MediatR;

namespace Tareas.Domain.Events;

public record TareaReabiertaEvent(
    Guid Id,
    string Title,
    DateTime OcurredOn
): INotification;