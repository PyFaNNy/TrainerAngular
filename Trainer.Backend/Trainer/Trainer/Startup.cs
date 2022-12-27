using System.Reflection;
using FluentValidation.AspNetCore;
using Jdenticon.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Trainer.Application;
using Trainer.Chart;
using Trainer.CSVParserService;
using Trainer.EmailService;
using Trainer.Infrastructure.Filters;
using Trainer.Persistence;

namespace Trainer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {   
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());

                    options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffff";
                });;
            
            services.AddApplication(Configuration);
            services.AddPersistence(Configuration);
            services.AddEmailService(Configuration);
            services.AddCSVParserService(Configuration);
            services.AddSignalR();
            
            // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //     .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            //     {
            //         options.LoginPath = "/Account/login";
            //         options.AccessDeniedPath = "/Account/logout";
            //     });
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Trainer API",
                    Description = "An ASP.NET Core Web API for my Project",
                    Contact = new OpenApiContact
                    {
                        Name = "GitHub Account",
                        Url = new Uri("https://github.com/PyFaNNy")
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors(x => x.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trainer V1");
                c.RoutePrefix = string.Empty;
            });
            
            app.UseJdenticon();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                // endpoints.MapHub<ChartHub>("/chart");
            });
            
            app.UseWelcomePage();
        }
    }
}
