using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationExercise.DataLayer.Models;

namespace WebApplicationExercise.Core
{
    public class MainDataContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public override Task<int> SaveChangesAsync()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Entity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                ((Entity)entry.Entity).ModifiedDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    ((Entity)entry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChangesAsync();
        }
    }
}