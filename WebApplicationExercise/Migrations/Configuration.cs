namespace WebApplicationExercise.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using DataLayer.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Core.MainDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Core.MainDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            var products = new List<Product>
            {
                new Product
                {
                    Name = "New product",
                    Price = 20,
                    Quantity = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Product
                {
                    Name = "Other product",
                    Price = 26,
                    Quantity = 3,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }
            };

            context.Products.AddRange(products);

            var order = new Order
            {
                Customer = "customer1",
                Total = 96,
                Products = products,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            context.Orders.AddOrUpdate(order);
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
