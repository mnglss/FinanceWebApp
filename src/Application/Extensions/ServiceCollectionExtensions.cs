using Application.Interfaces;
using Application.ScheduledJob;
using Application.Services;
using Coravel;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMovementService, MovementService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IHealthService, HeathService>();
            
            services.AddScheduler();
            services.AddTransient<HealthJob>();
            
            return services;
        }
    }
}