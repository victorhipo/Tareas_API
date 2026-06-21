using Tareas.Application.DTOs;
using Tareas.Application.Interaces;

namespace Tareas.Application.UseCases.Tareas.GetAllTareas;

public class GetAllTareasHandler
{
    private readonly ITareaRepository _repository;

    public GetAllTareasHandler(ITareaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TareaDto>> HandleAsync(CancellationToken ct)
    {
        var tareas = await _repository.GetAllAsync(ct);

        return tareas.Select(t => new TareaDto(t.Id, t.Title, t.Description, t.DueDate, t.Status, t.CreatedAt)).ToList();
    }
}