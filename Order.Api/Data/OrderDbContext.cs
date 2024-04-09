using Microsoft.EntityFrameworkCore;
using OrderApi.Entities;
using System.Reflection;
using System.Reflection.Metadata;

namespace OrderApi.Data
{
    public class OrderDBContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public OrderDBContext(DbContextOptions<OrderDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
