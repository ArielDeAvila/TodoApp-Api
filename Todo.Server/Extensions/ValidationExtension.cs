using FluentValidation;
using Todo.Server.DTO;
using Todo.Server.Validations;

namespace Todo.Server.Extensions;

public static class ValidationExtension
{   
    public static IServiceCollection AddValidationExtension(this IServiceCollection services)
    {
        services.AddScoped<IValidator<UserRequestDto>, UserRegisterValidation>();
        services.AddScoped<IValidator<LoginRequestDto>, LoginValidation>();
        services.AddScoped<IValidator<TaskRequestDto>, TaskRequestValidation>();

        return services;
    }
}
