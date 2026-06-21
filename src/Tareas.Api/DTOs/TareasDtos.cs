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
    [Required, StringLength(200, MinimumLength = 1)] string Title,
    [StringLength(2000)] string? Description,
    DateTime? DueDate
);
public record UpdateTareaRequest(
    [Required, StringLength(200, MinimumLength = 1)] string Title,
    [StringLength(2000)] string? Description,
    DateTime? DueDate,
    [EnumDataType(typeof(TareaStatus))] TareaStatus Status
);