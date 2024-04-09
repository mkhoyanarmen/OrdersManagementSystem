using ProductApi;
using DiscountApi;
using Common.Hubs;
using OrderApi.Data;
using Common.Exceptions;
using OrderApi.Entities;
using Microsoft.AspNetCore.SignalR;

namespace OrderApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDBContext _dbContext;
        private readonly IHubContext<NotifyHub> _hubContext;
        private readonly Product.ProductClient _productClient;
        private readonly Discount.DiscountClient _discountClient;

        public OrderService(OrderDBContext dbContext, IHubContext<NotifyHub> hubContext, Discount.DiscountClient discountClient, Product.ProductClient productClient)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
            _productClient = productClient;
            _discountClient = discountClient;
        }

        //public async Task<Order> CreateOrderAsync(int productId)
        //{
        //    var productReply = await _productClient.GetProductAsync(new ProductRequest { Id = productId });
        //    if (productReply.Result == null)
        //    {
        //        throw new NotFoundException();
        //    }

        //    var discountReply = await _discountClient.GetDiscountAsync(new DiscountRequest { Id = productId });
        //    decimal productPrice = Convert.ToDecimal(productReply.Result.Price);
        //    decimal totalPrice = productPrice;

        //    if (discountReply.Result != null)
        //    {
        //        decimal discountFixedPrice = Convert.ToDecimal(discountReply.Result.FixedPrice);
        //        decimal discountPercentage = int.Parse(discountReply.Result.Percentage);

        //        if (discountFixedPrice != 0)
        //            totalPrice = productPrice - discountFixedPrice;
        //        else
        //            totalPrice = productPrice - (productPrice * discountPercentage / 100);
        //    }

        //    await _hubContext.Clients.Group($"product{productId}Group").SendAsync("ReceiveNotification", productId, productReply.Result.Name, totalPrice);

        //    Order order = new() { Price = totalPrice, ProductId = productId };
        //    _dbContext.Orders.Add(order);
        //    await _dbContext.SaveChangesAsync();

        //    return order;
        //}
    }
}
