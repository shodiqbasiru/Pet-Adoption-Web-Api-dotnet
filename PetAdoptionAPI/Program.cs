using Microsoft.AspNetCore.Mvc;
using PetAdoptionAPI.Extensions;
using PetAdoptionAPI.filters;
using PetAdoptionAPI.Middlewares;
using PetAdoptionAPI.Services;

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
builder.Services.AddRepositories(builder.Configuration); // Inject the repositories
builder.Services.AddMiddlewares();
builder.Services.AddServices(); // Inject the services
// builder.Services.AddAuthorization();

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