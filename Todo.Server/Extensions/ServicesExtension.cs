using Todo.Server.Services;
using Todo.Server.Services.Interfaces;

namespace Todo.Server.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServicesExtension(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<ITaskService, TaskService>();

        return services;
    }
}
