using Tareas.Application.Interaces;
using MediatR;


namespace Tareas.Application.UseCases.Tareas.Commands.UpdateTarea;

public class UpdateTareaHandler : IRequestHandler<UpdateTareaCommand, bool>
{
    private readonly ITareaRepository _repository;

    public UpdateTareaHandler(ITareaRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateTareaCommand request, CancellationToken ct)
    {
        var tarea = await _repository.GetByIdAsync(request.Id, ct);

        if (tarea is null ) return false;

        tarea.Title = request.Title;
        tarea.Description = request.Description;
        tarea.DueDate = request.DueDate;
        tarea.Status = request.Status;

        await _repository.UpdateAsync(tarea, ct);

        return true;
    }
}