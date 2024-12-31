using DVLD.API.Middlewares;
using DVLD.Application;
using DVLD.Application.Common.settings;
using DVLD.Application.settings;
using DVLD.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer((options) =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        //You can set this to true for production.
        //This setting determines whether to validate the "issuer" (iss) claim of the JWT. The "issuer" is the entity that issued the JWT. In this code, it's set to true, which means the issuer will be validated. In a production environment, you should typically set this to true to ensure that the token is issued by a trusted authority.
        ValidateAudience = true,
        //The "audience" (aud) claim in a JWT (JSON Web Token) represents the intended recipient of the token.
        //this setting determines whether to validate the "audience" (aud) claim of the JWT. The "audience" represents the intended recipient of the JWT. Again, it's set to true, but it's advisable to set it to true in production to verify that the token is meant for your application.

        ValidateLifetime = true,
        // This setting ensures that the token has not expired. It's set to true so that the token's expiration time is checked during validation.

        ValidateIssuerSigningKey = true,
        //This setting determines whether to validate the signing key used to sign the JWT. Setting it to true ensures that the token's signature is verified with the specified key.

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT")["SecretKey"]!)),
        //As we specified to validate Issuer and Audience, we must also specify the details of Audience and Issuer to validate the incoming token's issuer and audience against these details.
        ValidAudience = builder.Configuration.GetSection("JWT")["Audience"],
        ValidIssuer = builder.Configuration.GetSection("JWT")["Issuer"],
        ClockSkew = TimeSpan.Zero

    };
});

builder.Services.AddAuthorization();

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
        builder.WithOrigins("http://localhost:5174") // Replace with your frontend's origin
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // Important for including cookies or tokens
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

