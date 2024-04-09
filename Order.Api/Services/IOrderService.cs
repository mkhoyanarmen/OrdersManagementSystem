using Microsoft.AspNetCore.Mvc;
using OrderApi.Entities;

namespace OrderApi.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(int productId);
    }
}
