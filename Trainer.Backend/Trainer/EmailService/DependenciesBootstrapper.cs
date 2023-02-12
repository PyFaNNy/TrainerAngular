using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trainer.Application.Interfaces;
using Trainer.Settings;

namespace Trainer.EmailService
{
    public static class DependenciesBootstrapper
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, Services.EmailService>();

            return services;
        }
    }
}
