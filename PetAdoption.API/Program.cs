using Microsoft.AspNetCore.Mvc;
using PetAdoption.API.Extensions;
using PetAdoption.API.filters;
using PetAdoption.API.Middlewares;
using PetAdoption.Application;
using PetAdoption.Application.Interfaces;
using PetAdoption.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services.AddControllers(config =>
{
    config.Filters.Add<ValidateEntityFilter>();
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHostedService<AdminInitializer>();
builder.Services.AddJwtSwaggerAuthentication();
builder.Services.AddJwtAuthentication(builder.Configuration); // Inject the JwtAuthentication
builder.Services.AddInfrastructure(builder.Configuration); // Inject the infrastructure
builder.Services.AddMiddlewares();
builder.Services.AddApplication(); // Inject the application

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

await app.RunAsync();