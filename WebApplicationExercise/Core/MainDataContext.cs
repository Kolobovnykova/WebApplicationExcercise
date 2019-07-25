using System.Data.Entity;
using WebApplicationExercise.DataLayer.Models;

namespace WebApplicationExercise.Core
{
    public class MainDataContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany<Product>(p => p.Products)
                .WithMany(c => c.Orders)
                .Map(op =>
                {
                    op.MapLeftKey("OrderId");
                    op.MapRightKey("ProductId");
                    op.ToTable("ProductOrders");
                });
        }
    }
}