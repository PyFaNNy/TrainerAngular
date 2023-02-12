using CSVParserService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trainer.Application.Interfaces;

namespace Trainer.CSVParserService
{
    public static class DependenciesBootstrapper
    {
        public static IServiceCollection AddCSVParserService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICsvParserService, CsvParserService>();

            return services;
        }
    }
}
