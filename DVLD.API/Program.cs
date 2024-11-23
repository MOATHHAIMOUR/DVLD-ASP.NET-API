using DVLD.API.Middlewares;
using DVLD.Application;
using DVLD.Infrastructure;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add Infrastructure layer Dependencies
builder.Services.InfrastructureDependencies();

// Add Application layer Dependencies
builder.Services.ApplicationDependencies();

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Use camelCase for JSON serialization
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        // Enable case-insensitive matching for deserialization
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseMiddleware<GlobalExeptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

