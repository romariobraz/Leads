using Microsoft.OpenApi.Models;

namespace LeadQualifier.Api.Config;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Lead Qualification API",
                Version = "v1",
                Description = "API responsável pela qualificação automatizada de leads.",
                Contact = new OpenApiContact
                {
                    Name = "LeadQualifier Team",
                    Url = new Uri("https://github.com/your-org")
                }
            });

            c.EnableAnnotations();
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lead Qualification API v1");
            c.RoutePrefix = "docs";
        });

        return app;
    }
}
