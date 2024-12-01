using EnsureThat;
using Hospital.Db.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Db.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddDb(
        this IServiceCollection services,
        string connectionString)
    {
        EnsureArg.IsNotNull(services, nameof(services));
        EnsureArg.IsNotNullOrWhiteSpace(connectionString, nameof(connectionString));

        services.AddDbContext<HospitalContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<UserRepository>();
        services.AddScoped<RoleRepository>();
        services.AddScoped<TownRepository>();
        services.AddScoped<PolyclinicRepository>();
        services.AddScoped<DoctorRepository>();
        services.AddScoped<SpecializationRepository>();
    }
}