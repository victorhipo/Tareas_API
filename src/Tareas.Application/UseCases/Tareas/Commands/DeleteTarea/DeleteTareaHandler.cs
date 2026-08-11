using MediatR;
using Tareas.Application.Interaces;

namespace Tareas.Application.UseCases.Tareas.Commands.DeleteTarea;

public class DeleteTareaHandler : IRequestHandler<DeleteTareaCommand, bool>
{
    private readonly ITareaRepository _repository;

    public DeleteTareaHandler(ITareaRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle( DeleteTareaCommand request, CancellationToken ct)
    {
        var tarea = await _repository.GetByIdAsync( request.Id, ct );

        if( tarea is null) return false;

        await _repository.DeleteAsync(tarea, ct);
        return true;
    }
}