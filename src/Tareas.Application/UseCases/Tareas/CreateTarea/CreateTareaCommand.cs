namespace Tareas.Application.UseCases.Tareas.CreateTarea;

public record CreateTareaCommand(string Title, string? Description, DateTime? DueDate);
