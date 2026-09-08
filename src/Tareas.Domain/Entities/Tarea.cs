using MediatR;
using Tareas.Domain.Enum;

namespace Tareas.Domain.Entities
{
    public class Tarea
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TareaStatus Status { get; set; } = TareaStatus.Pendiente;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        private readonly List<INotification> _domainEvents = new();

        public IReadOnlyList<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(INotification eventItem) => _domainEvents.Add(eventItem);

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}

