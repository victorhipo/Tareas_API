using Microsoft.AspNetCore.Mvc;
using Tareas.Api.DTOs;
using Tareas.Application.UseCases.Tareas.GetAllTareas;
using Tareas.Application.UseCases.Tareas.GetTareaById;
using Tareas.Application.UseCases.Tareas.CreateTarea;
using Tareas.Application.UseCases.Tareas.UpdateTarea;
using Tareas.Application.UseCases.Tareas.DeleteTarea;


namespace Tareas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareasController: ControllerBase
{
    private readonly GetAllTareasHandler _getAllHandler;
    private readonly GetTareaByIdHandler _getByIdHandler;
    private readonly CreateTareaHandler _createHandler;
    private readonly UpdateTareaHandler _updateHandler;
    private readonly DeleteTareaHandler _deleteHandler;

    public TareasController(GetAllTareasHandler getAllHandler, GetTareaByIdHandler getByIdHandler, CreateTareaHandler createHandler, UpdateTareaHandler updateHandler, DeleteTareaHandler deleteHandler)
    {
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
        _getAllHandler = getAllHandler;
        _getByIdHandler = getByIdHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TareaResponse>>> GetAll( CancellationToken ct)
    {
        var tareas = await _getAllHandler.HandleAsync(ct);
        var response = tareas.Select( t => new TareaResponse(t.Id, t.Title, t.Description, t.DueDate, t.Status, t.CreatedAt));

        return Ok(response);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TareaResponse>> GetById(Guid id, CancellationToken ct)
    {
        var tarea = await _getByIdHandler.HandleAsync(id, ct);
        
        if(tarea is null) return NotFound();

        return Ok(new TareaResponse(tarea.Id,tarea.Title, tarea.Description, tarea.DueDate, tarea.Status, tarea.CreatedAt));
    }

    [HttpPost]
    public async Task<ActionResult<TareaResponse>> Create([FromBody]CreateTareaRequest request, CancellationToken ct)
    {
        var command = new CreateTareaCommand(request.Title, request.Description, request.DueDate);
        
        var tarea = await _createHandler.HandleAsync(command, ct);

        var response = new TareaResponse(tarea.Id, tarea.Title, tarea.Description, tarea.DueDate, tarea.Status, tarea.CreatedAt);

        return CreatedAtAction(nameof(GetById), new{id = tarea.Id}, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTarea(Guid id, [FromBody]UpdateTareaRequest request, CancellationToken ct)
    {
        var command = new UpdateTareaCommand(id, request.Title, request.Description, request.DueDate, request.Status);

        var found = await _updateHandler.HandleAsync(command, ct);

        if(!found) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTarea(Guid id, CancellationToken ct)
    {
        var found = await _deleteHandler.HandleAsync(id, ct);

        if(!found) return NotFound();

        return NoContent();

    }
}