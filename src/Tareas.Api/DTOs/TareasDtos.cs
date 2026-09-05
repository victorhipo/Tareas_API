using Tareas.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Tareas.Api.DTOs;

public record TareaResponse(
    Guid Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    TareaStatus Status,
    DateTime CreatedAt
);

public record CreateTareaRequest(
    string Title,
    string? Description,
    DateTime? DueDate
);
public record UpdateTareaRequest(
    string Title,
    string? Description,
    DateTime? DueDate,
    TareaStatus Status
);