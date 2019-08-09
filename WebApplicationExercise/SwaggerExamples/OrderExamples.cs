using System;
using System.Collections.Generic;
using Swashbuckle.Examples;
using WebApplicationExercise.DataLayer.Models;

namespace WebApplicationExercise.SwaggerExamples
{
    public class OrderExamples : IExamplesProvider
    {
        public object GetExamples()
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CreatedDate = new DateTime(2016, 4, 13),
                ModifiedDate = new DateTime(2016, 4, 14),
                Customer = "John",
                Total = 68.3d,
                Products = new List<Product>()
            };
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product1",
                Price = 68.3d,
                Quantity = 1,
                CreatedDate = new DateTime(2016, 4, 13),
                ModifiedDate = new DateTime(2016, 4, 14),
            };

            order.Products.Add(product);

            return order;
        }
    }
}