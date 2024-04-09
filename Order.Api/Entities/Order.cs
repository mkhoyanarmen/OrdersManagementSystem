using Microsoft.EntityFrameworkCore;

namespace OrderApi.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
    }
}
