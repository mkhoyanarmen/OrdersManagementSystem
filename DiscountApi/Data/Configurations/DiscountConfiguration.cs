using DiscountApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountApi.Data.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<DiscountEntity>
    {
        public void Configure(EntityTypeBuilder<DiscountEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(b => b.ProductId);
            builder.Property(e => e.FixedPrice)
                .HasPrecision(25, 10);
        }
    }
}
