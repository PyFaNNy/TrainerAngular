using Microsoft.AspNetCore.Mvc;
using Trainer.Domain.Entities;
using Trainer.IdentityServer;
using Trainer.IdentityServer.Infrastructure;
using Trainer.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Configuration.GetClients())
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityServer();

app.MapControllers();

app.Run();