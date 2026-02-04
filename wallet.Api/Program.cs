using Microsoft.EntityFrameworkCore;
using wallet.Api.Interfaces;
using wallet.Api.Services;
using Wallet.Api.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Registrar o serviço
builder.Services.AddScoped<IBalanceCacheService, BalanceCacheService>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

// Add custom services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("WalletDb")); // Using InMemory for now to ensure build/run

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wallet API v1");
    });
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapGet("/", () => "API Wallet rodando");

app.Run();

