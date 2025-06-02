using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjetoRabbitMQ.Infrastructure;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Infrastructure.Repositories;
using ProjetoRabbitMQ.Models.Manga.Commands;
using ProjetoRabbitMQ.Models.Manga.Validation;
using ProjetoRabbitMQ.Models.MangaOrder.Commands;
using ProjetoRabbitMQ.Models.MangaOrder.Validation;
using ProjetoRabbitMQ.Models.User.Commands;
using ProjetoRabbitMQ.Models.User.Validation;
using ProjetoRabbitMQ.Services;
using ProjetoRabbitMQ.Services.Interfaces;
using Scrypt;
using Serilog;
using System.Security.Claims;
using System.Text;

namespace ProjetoRabbitMQ.Extensions
{
    public static class AppExtensions
    {
        public static void AddRabbitMQService(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.UsingRabbitMq((context, config) =>
                {
                    config.Host(new Uri(configuration["RABBITMQ_URI"]!), host =>
                    {
                        host.Username(configuration["RABBITMQ_USERNAME"]!);
                        host.Password(configuration["RABBITMQ_PASSWORD"]!);
                    });

                    config.ConfigureEndpoints(context);
                });
            });
        }

        public static void AddDatabaseContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<MySqlContext>(opt =>
            {
                var connectionString = configuration["MY_SQL_CONNECTION_STRING"];
                opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
        }

        public static void AddJwtConfiguration(this IServiceCollection services, ConfigurationManager configuration)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT_KEY"]!)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        RoleClaimType = ClaimTypes.Role
                    };
                });
        }

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Please insert the JWT in the format: Bearer {your token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void AddMediatRConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
        }

        public static void AddDependencyInjectionForServices(this IServiceCollection services)
        {
            services.AddScoped<ScryptEncoder>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IPasswordHasher, ScryptPasswordHasher>();

            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
            services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();

            services.AddScoped<IValidator<CreateMangaCommand>, CreateMangaCommandValidator>();
            services.AddScoped<IValidator<UpdateMangaCommand>, UpdateMangaCommandValidator>();

            services.AddScoped<IValidator<RequestMangaOrderCommand>, RequestMangaOrderCommandValidator>();
        }

        public static void AddSerilogConfiguration(this ConfigureHostBuilder config)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File("Logs/app.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            config.UseSerilog();
        }

        public static void EnsureDatabaseCreated(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MySqlContext>();
            dbContext.Database.EnsureCreated();
        }
    }
}
