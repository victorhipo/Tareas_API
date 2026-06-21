using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tareas.Application.Interaces;
using Tareas.Infrastructure.Persistence;

namespace Tareas.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TareasDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
        services.AddScoped<ITareaRepository, TareaRepository>();

        return services;
    }
    public static async Task UseInfrastructureAsync( this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TareasDBContext>();
        await db.Database.MigrateAsync();

    }
}