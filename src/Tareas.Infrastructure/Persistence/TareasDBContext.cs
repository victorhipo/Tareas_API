using Tareas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tareas.Infrastructure.Persistence
{
    public class TareasDBContext: DbContext
    {
        public TareasDBContext(DbContextOptions<TareasDBContext> options): base(options)
        {}

        public DbSet<Tarea> Tareas => Set<Tarea>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title).IsRequired().HasMaxLength(200);
                entity.Property(t => t.Description).HasMaxLength(2000);
                entity.Property(t => t.Status).HasConversion<int>();
                entity.HasIndex(t => t.Status);

                entity.Ignore(t => t.DomainEvents);
            });
        }
    }
}