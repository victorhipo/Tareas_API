using Tareas.Application.Interaces;


namespace Tareas.Application.UseCases.Tareas.UpdateTarea;

public class UpdateTareaHandler
{
    private readonly ITareaRepository _repository;

    public UpdateTareaHandler(ITareaRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> HandleAsync(UpdateTareaCommand command, CancellationToken ct)
    {
        var tarea = await _repository.GetByIdAsync(command.Id, ct);

        if (tarea is null ) return false;

        tarea.Title = command.Title;
        tarea.Description = command.Description;
        tarea.DueDate = command.DueDate;
        tarea.Status = command.Status;

        await _repository.UpdateAsync(tarea, ct);

        return true;
    }
}