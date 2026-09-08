using MediatR;
using Microsoft.Extensions.Logging;
using Tareas.Domain.Events;

namespace Tareas.Application.EventHandlers.Tareas.TareaCreada;

public class LogTareaCompletadaHandler : INotificationHandler<TareaCompletadaEvent>
{
    private readonly ILogger<LogTareaCompletadaHandler> _logger;

    public LogTareaCompletadaHandler(ILogger<LogTareaCompletadaHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TareaCompletadaEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "[EVENTO] 🎉 Tarea completada: '{Title}' (Id: {Id}) a las {OccurredOn}",
            notification.Title,
            notification.Id,
            notification.OcurredOn
        );

        return Task.CompletedTask;
    }
}