using MediatR;
using Tareas.Application.Interaces;
using Tareas.Domain.Events;

namespace Tareas.Application.UseCases.Tareas.Commands.DeleteTarea;

public class DeleteTareaHandler : IRequestHandler<DeleteTareaCommand, bool>
{
    private readonly ITareaRepository _repository;
    private readonly IMediator _mediator;

    public DeleteTareaHandler(ITareaRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<bool> Handle( DeleteTareaCommand request, CancellationToken ct)
    {
        var tarea = await _repository.GetByIdAsync( request.Id, ct );

        if( tarea is null) return false;

        var eventoBorrada = new TareaBorradaEvent(tarea.Id, tarea.Title, DateTime.UtcNow);

        await _repository.DeleteAsync(tarea, ct);
        await _mediator.Publish(eventoBorrada, ct);
        
        return true;
    }
}