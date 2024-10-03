using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Services;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterTaskManagementServices(this IServiceCollection services)
    {
        services.AddTransient<UsersManagementService>();
        services.AddTransient<ProjectsManagementService>();
        services.AddTransient<TaskManagementService>();
        services.AddTransient<CommentsManagementService>();

        return services;
    }
}
