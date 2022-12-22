using App.Metrics.Formatters.Prometheus;
using Microsoft.EntityFrameworkCore;
using Trainer.Persistence;

namespace Trainer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<TrainerDbContext>();
                    if (dbContext.Database.IsSqlServer())
                    {
                        dbContext.Database.Migrate();
                    }
                    await DefaultInitializer.InitializeAsync(dbContext);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseMetricsWebTracking(options =>
                {
                    options.OAuth2TrackingEnabled = true;
                })
                .UseMetricsEndpoints(options =>
                {
                    options.EnvironmentInfoEndpointEnabled = true;
                    options.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                    options.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
                }).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}