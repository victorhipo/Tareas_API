using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tareas.Api.DTOs;
using Tareas.Application.UseCases.Tareas.Queries.GetAllTareas;
using Tareas.Application.UseCases.Tareas.Queries.GetTareaById;
using Tareas.Application.UseCases.Tareas.Commands.CreateTarea;
using Tareas.Application.UseCases.Tareas.Commands.UpdateTarea;
using Tareas.Application.UseCases.Tareas.Commands.DeleteTarea;


namespace Tareas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareasController: ControllerBase
{
    private readonly IMediator _mediator;

    public TareasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TareaResponse>>> GetAll( CancellationToken ct)
    {
        var tareas = await _mediator.Send(new GetAllTareasQuery(), ct);
        var response = tareas.Select( t => new TareaResponse(t.Id, t.Title, t.Description, t.DueDate, t.Status, t.CreatedAt));

        return Ok(response);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TareaResponse>> GetById(Guid id, CancellationToken ct)
    {
        var tarea = await _mediator.Send( new GetTareaByIdQuery(id), ct);
        
        if(tarea is null) return NotFound();

        return Ok(new TareaResponse(tarea.Id,tarea.Title, tarea.Description, tarea.DueDate, tarea.Status, tarea.CreatedAt));
    }

    [HttpPost]
    public async Task<ActionResult<TareaResponse>> Create([FromBody]CreateTareaRequest request, CancellationToken ct)
    {
        var command = new CreateTareaCommand(request.Title, request.Description, request.DueDate);
        
        var tarea = await _mediator.Send(command, ct);

        var response = new TareaResponse(tarea.Id, tarea.Title, tarea.Description, tarea.DueDate, tarea.Status, tarea.CreatedAt);

        return CreatedAtAction(nameof(GetById), new{id = tarea.Id}, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTarea(Guid id, [FromBody]UpdateTareaRequest request, CancellationToken ct)
    {
        var command = new UpdateTareaCommand(id, request.Title, request.Description, request.DueDate, request.Status);

        var found = await _mediator.Send(command, ct);

        if(!found) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTarea(Guid id, CancellationToken ct)
    {
        var command = new DeleteTareaCommand(id);
        
        var found = await _mediator.Send(command, ct);

        if(!found) return NotFound();

        return NoContent();

    }
}