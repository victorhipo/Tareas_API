using Tareas.Application.DTOs;
using Tareas.Application.Interaces;
using Tareas.Domain.Entities;
using MediatR;

namespace Tareas.Application.UseCases.Tareas.Commands.CreateTarea;

public class CreateTareaHandler : IRequestHandler<CreateTareaCommand, TareaDto>
{
    private readonly ITareaRepository _repository;

    public CreateTareaHandler(ITareaRepository repository)
    {
        _repository = repository;
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

        await _repository.AddAsync(tarea, ct);
        return new TareaDto(tarea.Id, tarea.Title, tarea.Description, tarea.DueDate, tarea.Status, tarea.CreatedAt);
    }

}