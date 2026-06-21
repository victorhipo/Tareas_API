using Tareas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tareas.Infrastructure.Persistence
{
    public class TareasDBContext: DbContext
    {
        public TareasDBContext(DbContextOptions<TareasDBContext> options): base(options)
        {}

        public DbSet<Tarea> Tareas => Set<Tarea>();
    }
}