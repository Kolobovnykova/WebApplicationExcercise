using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;
using WebApplicationExercise.DataLayer.Interfaces;
using WebApplicationExercise.DataLayer.Models;
using WebApplicationExercise.Exceptions;

namespace WebApplicationExercise.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private readonly IOrderRepository _repository;

        [InjectionConstructor]
        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getOrder")]
        public async Task<IHttpActionResult> GetOrder([FromUri]Guid orderId)
        {
            try
            {
                var order = await _repository.Get(orderId);
                return Ok(order);
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("getOrders")]
        public async Task<IEnumerable<Order>> GetOrders(
            [FromUri]DateTime? from = null,
            [FromUri]DateTime? to = null,
            [FromUri]string customerName = null)
        {
            var orders = await _repository.GetOrders(from, to, customerName);

            return orders;
        }

        [HttpPost]
        [Route("saveOrder")]
        public async Task<IHttpActionResult> SaveOrder([FromBody]Order order)
        {
            try
            {
                await _repository.Create(order);
                return Ok();
            }
            catch (ArgumentNullException e)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("deleteOrder")]
        public async Task<IHttpActionResult> DeleteOrder([FromUri]Guid orderId)
        {
            try
            {
                var order = await _repository.Delete(orderId);
                return Ok(order);
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
        }
    }
}