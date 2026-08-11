using MediatR;
using Tareas.Application.DTOs;

namespace Tareas.Application.UseCases.Tareas.Queries.GetTareaById;

public record GetTareaByIdQuery(Guid Id) : IRequest<TareaDto?>;