using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Todo.Server.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
    {
        var openApi = new OpenApiInfo
        {
            Title = "ToDo App API",
            Version = "v1",
            Description = "To Do App API 2023",
            TermsOfService = new Uri("http://opensource.org/licenses/NIT"), //Url donde deberían ir nuestros terminos de uso
            Contact = new OpenApiContact
            {
                Name = "Ariel De avila",
                Email = "arieldeavila-1996@hotmail.com",
                Url = new Uri("http://sirtech.com.pe")
            },
            License = new OpenApiLicense
            {
                Name = "License",
                Url = new Uri("http://opensource.org/licenses")
            }
        };

        services.AddSwaggerGen(s =>
        {
            openApi.Version = "v1";

            s.SwaggerDoc("v1", openApi);

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authentication",
                Description = "Jwt Bearer Token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            s.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

            s.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {securityScheme, new string[]{ } }
            });


        });

        return services;
    }
}
