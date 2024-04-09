using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using OrdersManagementSystem.Data;

namespace ProductApi
{
    public class ProductService : Product.ProductBase
    {
        private readonly ProductDBContext _dbProduct;

        public ProductService(ProductDBContext dbProduct)
        {
            _dbProduct = dbProduct;
        }

        public override async Task<NullableResponse> GetProduct(ProductRequest request, ServerCallContext context)
        {
            var product = await _dbProduct.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
            if (product == null)
            {
                return new NullableResponse
                {
                    Result = null,
                };
            }

            return new NullableResponse
            {
                Result = new ProductReply
                {
                    Name = product.Name,
                    Price = product.Price.ToString()
                }
            };
        }
    }
}
