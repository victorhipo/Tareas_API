namespace Tareas
{
    public class Tarea
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TareaStatus Status { get; set; } = TareaStatus.Pendiente;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

