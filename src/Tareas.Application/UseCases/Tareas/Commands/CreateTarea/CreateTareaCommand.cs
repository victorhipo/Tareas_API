using MediatR;
using Tareas.Application.DTOs;

namespace Tareas.Application.UseCases.Tareas.Commands.CreateTarea;

public record CreateTareaCommand(string Title, string? Description, DateTime? DueDate) : IRequest<TareaDto>;
