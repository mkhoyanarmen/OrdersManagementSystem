using DiscountApi.Data;
using DiscountApi.Entities;
using DiscountApi.Repos;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace DiscountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redis;

        public DiscountController(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        [HttpPost]
        public async Task<DiscountEntity> AddDiscountAsync(int productId, decimal fixedPrice = 0, int percentage = 0)
        {
            var repository = new RedisRepository(_redis);
            DiscountEntity discount = new() {Id = productId, FixedPrice = fixedPrice, Percentage = percentage, ProductId = productId };
            repository.SaveDiscount(discount);
            discount = await repository.GetDiscountAsync(discount.Id);
            return discount;
        }
    }
}
