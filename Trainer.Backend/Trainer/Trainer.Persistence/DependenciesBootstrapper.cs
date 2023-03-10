using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trainer.Application.Interfaces;

namespace Trainer.Persistence
{
    public static class DependenciesBootstrapper
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TrainerDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetValue<string>("MSSQL_URL"),
                    b => b.MigrationsAssembly(typeof(TrainerDbContext).Assembly.FullName));

                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });
            services.AddScoped<ITrainerDbContext>(provider => provider.GetService<TrainerDbContext>());

            return services;
        }
    }
}