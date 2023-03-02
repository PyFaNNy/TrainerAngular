using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trainer.Application.Behaviours;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application
{
    public static class DependenciesBootstrapper
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            services.ConfigureWritable<BaseUserErrorSettings>(configuration.GetSection("BaseUserErrorSettings"));
            services.ConfigureWritable<CSVErrorSettings>(configuration.GetSection("CSVErrorSettings"));
            services.ConfigureWritable<ExaminationErrorSettings>(configuration.GetSection("ExaminationErrorSettings"));
            services.ConfigureWritable<OTPCodesErrorSettings>(configuration.GetSection("OTPCodesErrorSettings"));
            services.ConfigureWritable<PatientErrorSettings>(configuration.GetSection("PatientErrorSettings"));
            services.ConfigureWritable<ResultsErrorSettings>(configuration.GetSection("ResultsErrorSettings"));

            return services;
        }
    }
}
