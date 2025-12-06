using FluentValidation;
using FluentValidation.AspNetCore;
using LeadQualifier.Api.Middlewares;
using LeadQualifier.Application.Interfaces;
using LeadQualifier.Application.Services;
using LeadQualifier.Application.Validators;
using LeadQualifier.Infrastructure;
using LeadQualifier.Api.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------------
// Infra + API services (DB, repositories, HttpClient for OpenAI, etc.)
// ----------------------------------------------------------
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);

// ----------------------------------------------------------
// CONTROLLERS
// ----------------------------------------------------------
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // Usa ProblemDetails para erros de validação
        options.InvalidModelStateResponseFactory = context =>
        {
            var problemDetailsFactory = context.HttpContext
                .RequestServices
                .GetRequiredService<ProblemDetailsFactory>();

            var problem = problemDetailsFactory.CreateValidationProblemDetails(
                context.HttpContext,
                context.ModelState,
                statusCode: StatusCodes.Status400BadRequest,
                title: "Validation Error",
                type: "https://httpstatuses.com/400"
            );

            return new BadRequestObjectResult(problem);
        };
    });

// ----------------------------------------------------------
// FLUENT VALIDATION
// ----------------------------------------------------------
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(CreateLeadDtoValidator).Assembly);

// ----------------------------------------------------------
// DEPENDENCY INJECTION (Application + Infra)
// ----------------------------------------------------------

// Domain/Infra registrations are handled by AddInfrastructure/AddApiServices

// ----------------------------------------------------------
// SWAGGER
// ----------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----------------------------------------------------------
// BUILD APP
// ----------------------------------------------------------
var app = builder.Build();

// ----------------------------------------------------------
// MIDDLEWARES
// ----------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ProblemDetails — captura erros não tratados e formata
app.UseMiddleware<ProblemDetailsMiddleware>();

app.UseHttpsRedirection();

// ----------------------------------------------------------
// ENDPOINTS
// ----------------------------------------------------------
app.MapControllers();

app.Run();
