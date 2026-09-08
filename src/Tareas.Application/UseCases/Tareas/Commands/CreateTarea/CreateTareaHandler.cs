using Tareas.Application.DTOs;
using Tareas.Application.Interaces;
using Tareas.Domain.Entities;
using MediatR;
using Tareas.Domain.Events;

namespace Tareas.Application.UseCases.Tareas.Commands.CreateTarea;

public class CreateTareaHandler : IRequestHandler<CreateTareaCommand, TareaDto>
{
    private readonly ITareaRepository _repository;
    private readonly IMediator _mediator;
    public CreateTareaHandler(ITareaRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<TareaDto> Handle( CreateTareaCommand request, CancellationToken ct)
    {
        var tarea = new Tarea
        {
            Id = Guid.NewGuid(),  
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            CreatedAt = DateTime.UtcNow
        };

        tarea.AddDomainEvent(new TareaCreadaEvent(
            tarea.Id,
            tarea.Title,
            tarea.Description,
            tarea.DueDate,
            tarea.Status,
            tarea.CreatedAt,
            DateTime.UtcNow
        ));

        await _repository.AddAsync(tarea, ct);

        foreach(var domainEvent in tarea.DomainEvents)
        await _mediator.Publish(domainEvent, ct);

        tarea.ClearDomainEvents();

        return new TareaDto(tarea.Id, tarea.Title, tarea.Description, tarea.DueDate, tarea.Status, tarea.CreatedAt);
    }

}