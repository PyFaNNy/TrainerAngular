using System.Reflection;
using App.Metrics.Formatters.Prometheus;
using FluentValidation.AspNetCore;
using Jdenticon.AspNetCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
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
builder.Services.AddControllers()    
    .AddFluentValidation()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.Converters.Add(new StringEnumConverter());

        options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffff";
    });
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
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
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
});
            
app.UseJdenticon();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();