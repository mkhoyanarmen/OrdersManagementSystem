using DiscountApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace DiscountApi.Data
{
    public class DiscountDbContext : DbContext
    {
        public DbSet<DiscountEntity> Discounts { get; set; }
        public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}