using LeadQualifier.Application.Interfaces;
using LeadQualifier.Infrastructure.Persistence;
using LeadQualifier.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace LeadQualifier.Infrastructure;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("DefaultConnection");


        services.AddDbContext<AppDbContext>(options =>
        {
            // Use PostgreSQL via Npgsql
            options.UseNpgsql(conn, npgsql => npgsql.EnableRetryOnFailure());
        });


        // Register repository implementation for the application interface
        services.AddScoped<ILeadRepository, LeadRepository>();


        return services;
    }
}