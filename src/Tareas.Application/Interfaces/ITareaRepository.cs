using Tareas.Domain.Entities;

namespace Tareas.Application.Interaces;

public interface ITareaRepository
{
    Task<IReadOnlyList<Tarea>> GetAllAsync(CancellationToken ct);
    Task<Tarea?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Tarea tarea, CancellationToken ct);
    Task UpdateAsync(Tarea tarea, CancellationToken ct);
    Task DeleteAsync(Tarea tarea, CancellationToken ct);
}