using Tareas.Domain.Enum;

namespace Tareas.Application.UseCases.Tareas.UpdateTarea;

public record UpdateTareaCommand(
    Guid Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    TareaStatus Status
);