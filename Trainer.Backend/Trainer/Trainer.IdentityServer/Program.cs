using IdentityServer4.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Trainer.IdentityServer;
using Trainer.IdentityServer.Infrastructure;
using Trainer.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        options => options.WithOrigins(
                builder.Configuration.GetValue<string>("FRONTEND_URL"),
                builder.Configuration.GetValue<string>("BACKEND_URL"))
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Configuration.GetClients(builder.Configuration))
    .AddInMemoryApiResources(Configuration.GetApiResources())
    .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
    .AddInMemoryApiScopes(Configuration.GetApiScopes())
    .AddResourceOwnerValidator<UserValidator>()
    .AddProfileService<ProfileService>()
    .AddDeveloperSigningCredential();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "identityServer v1"));

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityServer();

app.MapControllers();

app.Run();