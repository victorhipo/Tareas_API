using Tareas.Application.DTOs;
using Tareas.Application.Interaces;
using Tareas.Domain.Entities;

namespace Tareas.Application.UseCases.Tareas.CreateTarea;

public class CreateTareaHandler
{
    private readonly ITareaRepository _repository;

    public CreateTareaHandler(ITareaRepository repository)
    {
        _repository = repository;
    }

    public async Task<TareaDto> HandleAsync( CreateTareaCommand command, CancellationToken ct)
    {
        var tarea = new Tarea
        {
            Id = Guid.NewGuid(),  
            Title = command.Title,
            Description = command.Description,
            DueDate = command.DueDate,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(tarea, ct);
        return new TareaDto(tarea.Id, tarea.Title, tarea.Description, tarea.DueDate, tarea.Status, tarea.CreatedAt);
    }

}