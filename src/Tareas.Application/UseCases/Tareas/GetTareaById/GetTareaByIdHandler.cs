using Tareas.Application.DTOs;
using Tareas.Application.Interaces;

namespace Tareas.Application.UseCases.Tareas.GetTareaById;

public class GetTareaByIdHandler
{
    private readonly ITareaRepository _repository;

    public GetTareaByIdHandler(ITareaRepository repository)
    {
        _repository = repository;
    }

    public async Task<TareaDto?> HandleAsync(Guid id, CancellationToken ct)
    {
        var tarea = await _repository.GetByIdAsync(id, ct);

        if( tarea is null ) return null;

        return new TareaDto(tarea.Id, tarea.Title, tarea.Description, tarea.DueDate, tarea.Status, tarea.CreatedAt);
    }
}