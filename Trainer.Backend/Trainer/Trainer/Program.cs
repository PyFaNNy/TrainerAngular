using System.Reflection;
using App.Metrics.Formatters.Prometheus;
using FluentValidation.AspNetCore;
using IdentityServer4.AccessTokenValidation;
using Jdenticon.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;
using Trainer;
using Trainer.Application;
using Trainer.Chart;
using Trainer.CSVParserService;
using Trainer.EmailService;
using Trainer.Middlewares;
using Trainer.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Host
    .UseMetricsWebTracking(options => { options.OAuth2TrackingEnabled = true; })
    .UseMetricsEndpoints(options =>
    {
        options.EnvironmentInfoEndpointEnabled = true;
        options.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
        options.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
    });

// Add services to the container.

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddEmailService(builder.Configuration);
builder.Services.AddCSVParserService(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
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

    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("https://localhost:10001/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"TrainerAPI", "TrainerAPI"}
                }
            }
        }
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.RequireHttpsMetadata = true;
        options.Authority = builder.Configuration.GetValue<string>("IDENTITY_URL");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("TrainerClientApp",
        new CorsPolicyBuilder()
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .Build());
});

builder.Services.AddAuthorization();
builder.Services.AddSignalR();
builder.Services.AddControllers()
    .AddFluentValidation()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.Converters.Add(new StringEnumConverter());

        options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffff";
    });

var app = builder.Build();

var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<TrainerDbContext>();
if (dbContext.Database.IsSqlServer())
{
    dbContext.Database.Migrate();
}
await DefaultInitializer.InitializeAsync(dbContext);

app.UseSwagger();
app.UseSwaggerUI(c =>
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "trainer v1"));

// app.UseMetricServer();
// app.UseMiddleware<ResponseMetricMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseStaticFiles();
app.UseCors("TrainerClientApp");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trainer V1");
    c.DocumentTitle = "Title";
    c.RoutePrefix = "docs";
    c.DocExpansion(DocExpansion.List);
    c.OAuthScopeSeparator(" ");
});

app.UseJdenticon();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChartHub>("/chart");
app.Run();