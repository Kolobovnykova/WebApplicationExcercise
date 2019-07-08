using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApplicationExercise.Models;
using System.Threading.Tasks;
using Ninject;
using WebApplicationExercise.DataLayer.Interfaces;

namespace WebApplicationExercise.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private readonly IOrderRepository _repository;

        [Inject]
        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getOrder")]
        public async Task<Order> GetOrder(Guid orderId)
        {
            var order = await _repository.Get(orderId);

            return order;
        }

        [HttpGet]
        [Route("getOrders")]
        public async Task<IEnumerable<Order>> GetOrders(DateTime? from = null, DateTime? to = null, string customerName = null)
        {
            var orders = await _repository.GetOrders(from, to, customerName);
            IEnumerable<Order> mockedOrders = new List<Order>
            {
                new Order
                {
                    CreatedDate = new DateTime(2019, 01, 31),
                    Customer = "Not Hidden Joe",
                    Id = Guid.NewGuid(),
                    Products = null
                }
            };

            return orders;
        }

        [HttpPost]
        [Route("saveOrder")]
        public void SaveOrder([FromBody]Order order)
        {
            _repository.Create(order);
            //_dataContext.Orders.Add(order);
            //_dataContext.SaveChanges();
        }
    }
}