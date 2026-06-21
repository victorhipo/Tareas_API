using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Tareas.Application.Interaces;
using Tareas.Domain.Entities;

namespace Tareas.Infrastructure.Persistence;

public class TareaRepository: ITareaRepository
{
    private readonly TareasDBContext _db;

    public TareaRepository(TareasDBContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Tarea tarea, CancellationToken ct)
    {
        _db.Tareas.Add(tarea);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Tarea tarea, CancellationToken ct)
    {
        _db.Remove(tarea);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<Tarea>> GetAllAsync(CancellationToken ct)
    {
        return await _db.Tareas
            .AsNoTracking()
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task<Tarea?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _db.Tareas.FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task UpdateAsync(Tarea tarea, CancellationToken ct)
    {
        _db.Tareas.Update(tarea);
        await _db.SaveChangesAsync(ct);
    }
}