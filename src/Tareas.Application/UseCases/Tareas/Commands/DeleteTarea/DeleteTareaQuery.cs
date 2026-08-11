using MediatR;

namespace Tareas.Application.UseCases.Tareas.Commands.DeleteTarea;

public record DeleteTareaCommand(Guid Id) : IRequest<bool>;