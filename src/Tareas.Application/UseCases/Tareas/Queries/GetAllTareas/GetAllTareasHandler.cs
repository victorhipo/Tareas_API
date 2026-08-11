using Tareas.Application.DTOs;
using Tareas.Application.Interaces;
using MediatR;


namespace Tareas.Application.UseCases.Tareas.Queries.GetAllTareas;

public class GetAllTareasHandler : IRequestHandler<GetAllTareasQuery, IReadOnlyList<TareaDto>>
{
    private readonly ITareaRepository _repository;

    public GetAllTareasHandler(ITareaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TareaDto>> Handle(GetAllTareasQuery request, CancellationToken ct)
    {
        var tareas = await _repository.GetAllAsync(ct);

        return tareas.Select(t => new TareaDto(t.Id, t.Title, t.Description, t.DueDate, t.Status, t.CreatedAt)).ToList();
    }
}