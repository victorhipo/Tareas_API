using Tareas.Application.Interaces;
using MediatR;
using Tareas.Domain.Events;
using Tareas.Domain.Enum;
using Tareas.Domain.Entities;


namespace Tareas.Application.UseCases.Tareas.Commands.UpdateTarea;

public class UpdateTareaHandler : IRequestHandler<UpdateTareaCommand, bool>
{
    private readonly ITareaRepository _repository;
    private readonly IMediator _mediator;

    public UpdateTareaHandler(ITareaRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<bool> Handle(UpdateTareaCommand request, CancellationToken ct)
    {
        var tarea = await _repository.GetByIdAsync(request.Id, ct);

        if (tarea is null ) return false;

        var statusAnterior = tarea.Status;

        tarea.Title = request.Title;
        tarea.Description = request.Description;
        tarea.DueDate = request.DueDate;
        tarea.Status = request.Status;

        tarea.AddDomainEvent(new TareaActualizadaEvent(
            tarea.Id,
            tarea.Title,
            tarea.Description,
            tarea.DueDate,
            tarea.Status,
            DateTime.UtcNow
        ));

        if( statusAnterior != TareaStatus.Completada && tarea.Status == TareaStatus.Completada )
        {
            tarea.AddDomainEvent( new TareaCompletadaEvent(
                tarea.Id,
                tarea.Title,
                DateTime.UtcNow
            ));
        }

        if( statusAnterior == TareaStatus.Completada && tarea.Status != TareaStatus.Completada)
        {
            tarea.AddDomainEvent( new TareaReabiertaEvent(
                tarea.Id,
                tarea.Title,
                DateTime.UtcNow
            ));
        }

        await _repository.UpdateAsync(tarea, ct);

        foreach( var domainEvent in tarea.DomainEvents)
        await _mediator.Publish(domainEvent,ct);

        tarea.ClearDomainEvents();

        return true;
    }
}