using DiscountApi.Data;
using DiscountApi.Repos;
using Grpc.Core;
using StackExchange.Redis;

namespace DiscountApi
{
    public class DiscountService : Discount.DiscountBase
    {
        private readonly IConnectionMultiplexer _redis;

        public DiscountService(DiscountDbContext dbDiscount, IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public override async Task<NullableResponse> GetDiscount(DiscountRequest request, ServerCallContext context)
        {
            var repository = new RedisRepository(_redis);
            var discount = await repository.GetDiscountAsync(request.Id);

            if (discount == null)
            {
                return new NullableResponse
                {
                    Result = null,
                };
            }

            return new NullableResponse
            {
                Result = new DiscountReply
                {
                    FixedPrice = discount.FixedPrice.ToString(),
                    Percentage = discount.Percentage.ToString()
                }
            };
        }
    }
}
