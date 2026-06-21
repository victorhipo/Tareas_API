using Tareas.Application.Interaces;

namespace Tareas.Application.UseCases.Tareas.DeleteTarea;

public class DeleteTareaHandler
{
    private readonly ITareaRepository _repository;

    public DeleteTareaHandler(ITareaRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> HandleAsync( Guid id, CancellationToken ct)
    {
        var tarea = await _repository.GetByIdAsync( id, ct );

        if( tarea is null) return false;

        await _repository.DeleteAsync(tarea, ct);
        return true;
    }
}