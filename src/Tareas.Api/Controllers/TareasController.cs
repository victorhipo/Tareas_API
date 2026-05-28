using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tareas.Api.DTOs;

namespace Tareas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareasController: ControllerBase
{
    private readonly TareasDBContext _db;
    public TareasController(TareasDBContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TareaResponse>>> GetAll( CancellationToken ct)
    {
        var tareas = await _db.Tareas.AsNoTracking().OrderByDescending(t => t.CreatedAt).Select(t => new TareaResponse(t.Id,t.Title, t.Description, t.DueDate, t.Status, t.CreatedAt)).ToListAsync(ct);
        return Ok(tareas);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TareaResponse>> GetTareaById(Guid id, CancellationToken ct)
    {
        var t = await _db.Tareas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id,ct);
        if(t is null) return NotFound();

        return Ok(new TareaResponse(t.Id,t.Title, t.Description, t.DueDate, t.Status, t.CreatedAt));
    }

    [HttpPost]
    public async Task<ActionResult<TareaResponse>> CreateTarea([FromBody]CreateTareaRequest request, CancellationToken ct)
    {
        var tarea = new Tarea
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            CreatedAt = DateTime.UtcNow
        };
        _db.Tareas.Add(tarea);
        await _db.SaveChangesAsync(ct);

        var response = new TareaResponse(tarea.Id, tarea.Title, tarea.Description, tarea.DueDate, tarea.Status, tarea.CreatedAt);
        return CreatedAtAction(nameof(GetTareaById), new {id = tarea.Id}, response);
    }
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TareaResponse>> UpdateTarea(Guid id, [FromBody]UpdateTareaRequest request, CancellationToken ct)
    {
        var tarea = await _db.Tareas.FirstOrDefaultAsync(x => x.Id == id,ct);
        if(tarea is null) return NotFound();

        tarea.Title = request.Title;
        tarea.Description = request.Description;
        tarea.DueDate = request.DueDate;
        tarea.Status = request.Status;

        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<TareaResponse>> DeleteTarea(Guid id, CancellationToken ct)
    {
        var tarea = await _db.Tareas.FirstOrDefaultAsync(x => x.Id == id,ct);
        if(tarea is null) return NotFound();

        _db.Remove(tarea);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}