using Tareas.Domain.Enum;
using MediatR;

namespace Tareas.Application.UseCases.Tareas.Commands.UpdateTarea;

public record UpdateTareaCommand(
    Guid Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    TareaStatus Status
) : IRequest<bool>;