using Microsoft.EntityFrameworkCore;
using OrdersManagementSystem.Entities;
using System.Reflection;

namespace OrdersManagementSystem.Data
{
    public class ProductDBContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
