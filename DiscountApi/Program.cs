using Common.Middlewares;
using DiscountApi;
using DiscountApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using ProductApi;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DiscountDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var redisConfiguration = builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(redisConfiguration);
});

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandling();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<DiscountService>();

app.Run();
