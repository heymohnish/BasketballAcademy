using BasketballAcademy.Extensions;
using BasketballAcademy.Services;
using BasketballAcademy.Services.Interfaces;
using Microsoft.OpenApi.Models;

namespace Basketbal.Academy
{
    internal class Program
    {
        static void Main(string[] args)
        {
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

            builder.Services.RegisterAuthendicationSettings();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<ITokenService, JWTService>();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                   {
                      new OpenApiSecurityScheme
                      {
                          Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                             }
                          },
                      new List<string>()
                     }
                 });
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.RegisterDatabaseSettings();
            builder.Services.RegisterRepositories();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

        }
    }
}