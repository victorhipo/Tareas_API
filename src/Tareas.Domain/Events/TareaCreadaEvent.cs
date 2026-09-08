using MediatR;
using Tareas.Domain.Enum;

namespace Tareas.Domain.Events;

public record TareaCreadaEvent(
    Guid Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    TareaStatus Status,
    DateTime CreatedAt,
    DateTime OcurredOn
) : INotification;