namespace DiscountApi.Entities
{
    public class DiscountEntity
    {
        public int Id { get; set; }
        public decimal FixedPrice { get; set; }
        public int Percentage { get; set; }
        public int ProductId { get; set; }
    }
}
