using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Abstractions.Repositories;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ITodoTaskRepository, InMemoryTodoTaskRepository>();
        return services;
    }
}
