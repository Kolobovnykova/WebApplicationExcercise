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
                Customer = "John",
                Products = new List<Product>()
            };
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product1",
                Price = 68.3d,
                Orders = new List<Order>()
            };

            order.Products.Add(product);

            return order;
        }
    }
}