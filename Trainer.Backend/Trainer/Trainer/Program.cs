using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using App.Metrics.Formatters.Prometheus;
using FluentValidation.AspNetCore;
using IdentityServer4.AccessTokenValidation;
using Jdenticon.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;
using Trainer.Application;
using Trainer.CSVParserService;
using Trainer.EmailService;
using Trainer.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Host    
    .UseMetricsWebTracking(options =>
    {
        options.OAuth2TrackingEnabled = true;
    })
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

    options.AddSecurityRequirement( new OpenApiSecurityRequirement
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
        options.Authority = "https://localhost:10001";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });
    // .AddIdentityServerAuthentication(options =>
    // {
    //     options.ApiName = "TrainerAPI";
    //     options.Authority = "https://localhost:10001";
    //     options.RequireHttpsMetadata = false;
    //     
    // });

builder.Services.AddAuthorization();

builder.Services.AddControllers()    
    .AddFluentValidation()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.Converters.Add(new StringEnumConverter());

        options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffff";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseMetricServer();
// app.UseMiddleware<ResponseMetricMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(x => x.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
            
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trainer V1");
    c.DocumentTitle = "Title";
    c.RoutePrefix = "docs";
    c.DocExpansion(DocExpansion.List);
    c.OAuthClientId("client_id_swagger");
    c.OAuthScopeSeparator(" ");
    c.OAuthClientSecret("client_secret_swagger");
});
            
app.UseJdenticon();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();