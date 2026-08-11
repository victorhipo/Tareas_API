using MediatR;
using Tareas.Application.DTOs;

namespace Tareas.Application.UseCases.Tareas.Queries.GetAllTareas;

public record GetAllTareasQuery() : IRequest<IReadOnlyList<TareaDto>>;