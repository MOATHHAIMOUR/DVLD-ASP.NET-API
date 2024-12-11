using DVLD.API.Middlewares;
using DVLD.Application;
using DVLD.Application.settings;
using DVLD.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

// Add Infrastructure layer Dependencies
builder.Services.InfrastructureDependencies();

// Add Application layer Dependencies
builder.Services.ApplicationDependencies();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configure JSON serializer options
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // Case-insensitive matching
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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    c.UseInlineDefinitionsForEnums();
});
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

