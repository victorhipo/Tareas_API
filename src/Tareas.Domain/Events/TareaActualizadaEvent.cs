using MediatR;
using Tareas.Domain.Enum;

namespace Tareas.Domain.Events;

public record TareaActualizadaEvent(
    Guid Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    TareaStatus Status,
    DateTime OcurredOn
) : INotification;