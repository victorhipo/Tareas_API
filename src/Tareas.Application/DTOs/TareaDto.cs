using Tareas.Domain.Enum;

namespace Tareas.Application.DTOs;


public record TareaDto(
    Guid Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    TareaStatus Status,
    DateTime CreatedAt
);