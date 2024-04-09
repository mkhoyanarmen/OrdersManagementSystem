using Microsoft.AspNetCore.Mvc;
using OrdersManagementSystem.Data;
using OrdersManagementSystem.Entities;

namespace OrdersManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDBContext _db;

        public ProductController(ProductDBContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<ProductEntity> AddProduct(string name, decimal price)
        {
            ProductEntity product = new ProductEntity { Name = name, Price = price };
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }
    }
}
