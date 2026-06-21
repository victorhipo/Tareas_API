using Microsoft.Extensions.DependencyInjection;
using Tareas.Application.UseCases.Tareas.CreateTarea;
using Tareas.Application.UseCases.Tareas.DeleteTarea;
using Tareas.Application.UseCases.Tareas.UpdateTarea;
using Tareas.Application.UseCases.Tareas.GetAllTareas;
using Tareas.Application.UseCases.Tareas.GetTareaById;

namespace Tareas.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication( this IServiceCollection services)
    {
        services.AddScoped<GetAllTareasHandler>();
        services.AddScoped<GetTareaByIdHandler>();
        services.AddScoped<CreateTareaHandler>();
        services.AddScoped<UpdateTareaHandler>();
        services.AddScoped<DeleteTareaHandler>();

        return services;
    }
}