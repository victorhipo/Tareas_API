using MediatR;
using Microsoft.Extensions.Logging;
using Tareas.Domain.Events;

namespace Tareas.Application.EventHandlers.Tareas.TareaCreada;

public class LogTareaCreadaHandler : INotificationHandler<TareaCreadaEvent>
{
    private readonly ILogger<LogTareaCreadaHandler> _logger;

    public LogTareaCreadaHandler(ILogger<LogTareaCreadaHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TareaCreadaEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "[EVENTO] Nueva tarea creada: '{Title}' (Id: {Id}) a las {OcurredOn}",
            notification.Title,
            notification.Id,
            notification.OcurredOn
        );

        return Task.CompletedTask;
    }

}