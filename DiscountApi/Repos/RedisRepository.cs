using DiscountApi.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace DiscountApi.Repos
{
    public class RedisRepository
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async void SaveDiscount(DiscountEntity discount)
        {
            var database = _redis.GetDatabase();
            var key = $"Discount:{discount.Id}";

            var serializedDiscount = JsonSerializer.Serialize(discount);

            await database.StringSetAsync(key, serializedDiscount);
        }

        public async Task<DiscountEntity> GetDiscountAsync(int id)
        {
            try
            {
                var database = _redis.GetDatabase();
                var key = $"Discount:{id}";
                var serializedDiscount = await database.StringGetAsync(key);

                return await JsonSerializer.DeserializeAsync<DiscountEntity>(new MemoryStream(serializedDiscount));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
