using BasketballAcademy.Model.Core;
using BasketballAcademy.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BasketballAcademy.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDatabaseSettings(this IServiceCollection services)
        {
            services.AddTransient(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return configuration.GetConnectionString("connect");
            });
            return services;
        }

        public static IServiceCollection RegisterAuthendicationSettings(this IServiceCollection services)
        {
            services.AddTransient(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return configuration.GetSection("JWT").Get<JWTSettings>();
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 var jwtSettings = services.BuildServiceProvider().GetService<IConfiguration>().GetSection("JWT");
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtSettings["Issuer"],
                     ValidAudience = jwtSettings["Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
                 };
             });
            return services;
        }


        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<AdminRepository>();
            services.AddTransient<UserRepository>();
            services.AddTransient<AdmissionRepository>();
            services.AddTransient<CoachRepository>();
            services.AddTransient<CredentialsRepository>();
            services.AddTransient<EventRepository>();
            return services;
        }
    }
}
