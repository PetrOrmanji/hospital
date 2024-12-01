using System.Text;
using EnsureThat;
using Hospital.Options;
using Hospital.Domain.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Hospital.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSwagger(
        this IServiceCollection services)
    {
        EnsureArg.IsNotNull(services, nameof(services));

        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Hospital API",
                Description = "An ASP.NET Core Web API for managing hospital items",
                Contact = new OpenApiContact
                {
                    Name = "Petr",
                    Email = "petrormanji68@mail.ru"
                }
            });
            options.AddSecurityDefinition(
                "Bearer", 
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header, 
                    Description = "Пожалуйста, вставьте JWT токен в поле в формате: bearer {jwtToken}", 
                    Name = "Authorization", 
                    Type = SecuritySchemeType.ApiKey
                });
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme,
                            }
                        }, 
                        new List<string>()
                    }
                });

        });
    }

    public static void AddApiAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        EnsureArg.IsNotNull(services, nameof(services));
        EnsureArg.IsNotNull(configuration, nameof(configuration));

        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.ClientSecret))
                };
            });

        services.AddAuthorization();
    }

    public static void AddMapperProfiles(
        this IServiceCollection services)
    {
        EnsureArg.IsNotNull(services, nameof(services));

        services.AddAutoMapper(
            typeof(TownProfile),
            typeof(PolyclinicProfile),
            typeof(SpecializationProfile),
            typeof(RoleProfile));
    }
}