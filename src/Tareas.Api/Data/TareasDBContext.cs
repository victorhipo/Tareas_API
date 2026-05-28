using Microsoft.EntityFrameworkCore;


namespace Tareas
{
    public class TareasDBContext: DbContext
    {
        public TareasDBContext(DbContextOptions<TareasDBContext> options): base(options)
        {}

        public DbSet<Tarea> Tareas => Set<Tarea>();
    }
}