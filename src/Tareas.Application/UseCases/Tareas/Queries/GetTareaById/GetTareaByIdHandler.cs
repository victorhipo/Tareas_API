using Tareas.Application.DTOs;
using Tareas.Application.Interaces;
using MediatR;

namespace Tareas.Application.UseCases.Tareas.Queries.GetTareaById;

public class GetTareaByIdHandler : IRequestHandler<GetTareaByIdQuery, TareaDto?>
{
    private readonly ITareaRepository _repository;

    public GetTareaByIdHandler(ITareaRepository repository)
    {
        _repository = repository;
    }

    public async Task<TareaDto?> Handle(GetTareaByIdQuery request, CancellationToken ct)
    {
        var tarea = await _repository.GetByIdAsync(request.Id, ct);

        if( tarea is null ) return null;

        return new TareaDto(tarea.Id, tarea.Title, tarea.Description, tarea.DueDate, tarea.Status, tarea.CreatedAt);
    }
}