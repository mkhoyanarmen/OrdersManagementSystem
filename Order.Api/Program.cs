using Common.Hubs;
using Common.Middlewares;
using DiscountApi;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using OrderApi.Services;
using ProductApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrderService, OrderService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OrderDBContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddSignalR();

builder.Services.AddGrpcClient<Discount.DiscountClient>(options =>
{
    options.Address = new Uri("https://localhost:7080");
});
builder.Services.AddGrpcClient<Product.ProductClient>(options =>
{
    options.Address = new Uri("https://localhost:7271");
});

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

app.MapHub<NotifyHub>("/notify");

app.Run();
